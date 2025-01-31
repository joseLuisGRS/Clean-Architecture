using ApplicationLayer_UC;
using ApplicationLayer_UC.UseCase;
using EnterpriseLayer;
using FluentValidation;
using FluentValidation.AspNetCore;
using FrameworksDrivers_API.Middlewares;
using FrameworksDrivers_API.Validators;
using FrameworksDrivers_ExternalService;
using InterfaceAdapters_Adapters;
using InterfaceAdapters_Adapters.Dtos;
using InterfaceAdapters_Data;
using InterfaceAdapters_Mappers;
using InterfaceAdapters_Mappers.Dtos.Requests;
using InterfaceAdapters_Models;
using InterfaceAdapters_Presenters;
using InterfaceAdapters_Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Validadores
builder.Services.AddValidatorsFromAssemblyContaining<BeerValidator>(); // para inyeccion de validador
builder.Services.AddFluentValidationAutoValidation(); //para validacion automatica
builder.Services.AddFluentValidationClientsideAdapters(); //para validacion en formularios web

//dependencias internas
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IRepository<Beer>, Repository>();
builder.Services.AddScoped<IRepository<Sale>, SaleRepository>();
builder.Services.AddScoped<IRepositorySerch<SaleModel, Sale>, SaleRepository>();
builder.Services.AddScoped<IRepository<Inventory>, InventoryRepository>();

builder.Services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
builder.Services.AddScoped<IPresenter<Beer, BeerDetailViewModel>, BeerDetailPresenter>();
builder.Services.AddScoped<IPresenter<Inventory, InventoryViewModel>, InventoryPresenter>();

builder.Services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();
builder.Services.AddScoped<IMapper<SaleRequestDTO, Sale>, SaleMapper>();

builder.Services.AddScoped<IExternalService<PostServiceDTO>, PostService>();
builder.Services.AddScoped<IExternalServiceAdapter<Post>, PostExternalServiceAdapter>();

builder.Services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerDetailViewModel>>();
builder.Services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();
builder.Services.AddScoped<GetPostUseCase>();
builder.Services.AddScoped<GenerateSaleUseCase<SaleRequestDTO>>();
builder.Services.AddScoped<GetSaleUseCase>();
builder.Services.AddScoped<GetSaleSearchUseCase<SaleModel>>();
builder.Services.AddScoped<GetInventoryUseCase<Inventory, InventoryViewModel>>();


builder.Services.AddHttpClient<IExternalService<PostServiceDTO>, PostService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Middlewares
app.UseMiddleware<ExceptionMiddleware>();

app.MapGet("/beer", async (GetBeerUseCase<Beer, BeerViewModel> beerUseCase) =>
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("beers")
.WithOpenApi();

app.MapPost("/beer", async (BeerRequestDTO beerRequest, AddBeerUseCase<BeerRequestDTO> beerUseCase, IValidator<BeerRequestDTO> validator) =>
{
    var result = await validator.ValidateAsync(beerRequest);

    if (!result.IsValid)
    {
        return Results.ValidationProblem(result.ToDictionary());
    }

    await beerUseCase.ExecuteAsync(beerRequest);
    return Results.Created();
})
.WithName("addBeer")
.WithOpenApi();

app.MapGet("/beerDetail", async (GetBeerUseCase<Beer, BeerDetailViewModel> beerUseCase) =>
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("beerDetail")
.WithOpenApi();

app.MapGet("/posts", async (GetPostUseCase postUseCase) =>
{
    return await postUseCase.ExecuteAsync();
})
.WithName("posts")
.WithOpenApi();

app.MapPost("/sale", async (SaleRequestDTO saleRequest , GenerateSaleUseCase<SaleRequestDTO> saleUseCase) =>
{
    await saleUseCase.ExecuteAsync(saleRequest);
    return Results.Created();
})
.WithName("generateSale")
.WithOpenApi();

app.MapGet("/sale", async (GetSaleUseCase getSaleUseCase) =>
{
    return await getSaleUseCase.ExecuteAsync();
})
.WithName("getSales")
.WithOpenApi();

app.MapGet("/salesearch/{total}", async (GetSaleSearchUseCase<SaleModel> saleUseCase, int total) =>
{
    return await saleUseCase.ExecuteAsync(s => s.Total > total);
})
.WithName("getSalesSearch")
.WithOpenApi();

app.MapGet("inventory", async (GetInventoryUseCase<Inventory, InventoryViewModel> inventoryUseCase) =>
{
    return await inventoryUseCase.ExecuteAsync();
})
.WithName("inventories")
.WithOpenApi();

app.Run();
