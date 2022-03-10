namespace Cars.Services;

public interface ICarService
{
    Task<Brand> AddBrand(Brand newBrand);
    Task<Car> AddCar(Car newCar);
    Task<Brand> GetBrand(string id);
    Task<List<Brand>> GetBrands();
    Task<Car> GetCar(string id);
    Task<List<Car>> GetCars();
    Task<List<Car>> GetCarsByBrandId(string brandId);
    Task SetupDummyData();
    Task<Brand> UpdateBrand(Brand brand);
    Task<Car> UpdateCar(Car car);
}

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IBrandRepository _brandRepository;

    public CarService(ICarRepository carRepository, IBrandRepository brandRepository)
    {
        _carRepository = carRepository;
        _brandRepository = brandRepository;
    }

    public async Task<Brand> AddBrand(Brand newBrand)
    {
        return await _brandRepository.AddBrand(newBrand);
    }

    public async Task<Car> AddCar(Car newCar)
    {
        return await _carRepository.AddCar(newCar);
    }

    public async Task<List<Brand>> GetBrands() => await _brandRepository.GetAllBrands();

    public async Task<List<Car>> GetCars() => await _carRepository.GetAllCars();

    public async Task<Brand> GetBrand(string id) => await _brandRepository.GetBrand(id);

    public async Task<Car> GetCar(string id) => await _carRepository.GetCar(id);

    public async Task<List<Car>> GetCarsByBrandId(string brandId) => await _carRepository.GetCarsByBrandId(brandId);

    public async Task<Brand> UpdateBrand(Brand brand) => await _brandRepository.UpdateBrand(brand);
    public async Task<Car> UpdateCar(Car car) => await _carRepository.UpdateCar(car);
    public async Task SetupDummyData()
    {
        if (!(await _brandRepository.GetAllBrands()).Any())
        {

            var brands = new List<Brand>(){
            new Brand()
            {
            Country = "Germany" , Name = "Volkswagen"
            },
            new Brand()
            {
           Country = "Germany" , Name = "BMW"
            },
            new Brand()
            {
         Country = "Germany" , Name = "Audi"
            },
            new Brand()
            {
              Country = "USA" , Name = "Tesla"
            }
        };

            foreach (var brand in brands)
                await _brandRepository.AddBrand(brand);
        }

        if (!(await _carRepository.GetAllCars()).Any())
        {
            var brands = await _brandRepository.GetAllBrands();
            var cars = new List<Car>()
        {
            new Car(){

                Name = "ID.3",
                Brand = brands[0],
            },
            new Car(){

                Name = "ID.4",
                Brand = brands[0],
            },
            new Car(){

                Name = "IX3",
                Brand = brands[1],
            },
            new Car(){

                Name = "E-Tron",
                Brand = brands[2],
            },
            new Car(){

                Name = "Model Y",
                Brand = brands[3],
            }
        };
            foreach (var car in cars)
                await _carRepository.AddCar(car);
        }
    }


}