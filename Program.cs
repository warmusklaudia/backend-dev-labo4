var builder = WebApplication.CreateBuilder(args);

//inlezen database config info
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ICarRepository, CarRepository>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ICarService, CarService>();

// builder.Services
//     .AddGraphQLServer()
//     .AddQueryType<Queries>()
//     .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
//     .AddMutationType<Mutation>();

var app = builder.Build();
// app.MapGraphQL();

app.MapGet("/helloworld", () => "Hello World");

app.MapGet("/setup", (ICarService CarService) => CarService.SetupDummyData());

app.MapPost("/brands", async (ICarService carService, Brand brand) =>
{
    var result = await carService.AddBrand(brand);
    return Results.Created("", result);
});

app.MapPut("/brands", async (ICarService carService, Brand brand) =>
{
    try
    {
        var result = await carService.UpdateBrand(brand);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        throw;
    }

});

app.MapPut("/cars", async (ICarService carService, Car car) =>
{
    try
    {
        var result = await carService.UpdateCar(car);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        throw;
    }

});

app.MapPost("/cars", async (ICarService carService, Car car) =>
{
    var result = await carService.AddCar(car);
    return Results.Created("", result);
});

app.MapGet("/brands", async (ICarService carService) =>
{
    try
    {
        var result = await carService.GetBrands();
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        throw;
    }

});

app.MapGet("/cars", async (ICarService carService) =>
{
    var result = await carService.GetCars();
    return Results.Ok(result);
});

app.MapGet("/cars/{id}", async (ICarService carService, string id) =>
{
    try
    {
        var result = await carService.GetCar(id);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        throw;
    }

});

app.MapGet("/brands/{id}", async (ICarService carService, string id) =>
{
    try
    {
        var result = await carService.GetBrand(id);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        throw;
    }

});

//GraphQL.SchemaExtensions.RegisterTypeMapping<Brand,BrandType>();
app.Run("http://0.0.0.0:3000");
