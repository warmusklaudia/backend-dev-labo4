namespace Cars.Repositories;

public interface ICarRepository
{
    Task<Car> AddCar(Car newCar);
    Task<List<Car>> GetAllCars();
    Task<Car> GetCar(string id);
    Task<List<Car>> GetCarsByBrandId(string brandId);
    Task<Car> UpdateCar(Car car);
}

public class CarRepository : ICarRepository
{
    private readonly IMongoContext _context;
    public CarRepository(IMongoContext context)
    {
        _context = context;
    }
    public async Task<Car> AddCar(Car newCar)
    {
        await _context.CarsCollection.InsertOneAsync(newCar);
        return newCar;
    }

    public async Task<List<Car>> GetAllCars() => await _context.CarsCollection.Find(_ => true).ToListAsync();

    public async Task<Car> GetCar(string id) => await _context.CarsCollection.Find<Car>(c => c.Id == id).FirstOrDefaultAsync();

    public async Task<List<Car>> GetCarsByBrandId(string brandId) => await _context.CarsCollection.Find(c => c.Brand.Id == brandId).ToListAsync();

    public async Task<Car> UpdateCar(Car car)
    {
        try
        {
            var filter = Builders<Car>.Filter.Eq("Id", car.Id);
            var update = Builders<Car>.Update.Set("Name", car.Name);
            await _context.CarsCollection.UpdateOneAsync(filter, update);
            return await GetCar(car.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
