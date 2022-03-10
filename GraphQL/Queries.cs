namespace Cars.GraphQL.Queries;

public class Queries
{
    public async Task<List<Car>> GetCars([Service] ICarService carService) => await carService.GetCars();
    public async Task<List<Brand>> GetBrands([Service] ICarService carService) => await carService.GetBrands();
    public async Task<List<Car>> GetCarsByBrand([Service] ICarService carService, string brandId) => await carService.GetCarsByBrandId(brandId);
}