namespace Cars.Repositories;

public interface IBrandRepository
{
    Task<Brand> AddBrand(Brand newBrand);
    Task DeleteBrand(string id);
    Task<Brand> GetBrand(string id);
    Task<List<Brand>> GetAllBrands();
    Task<Brand> UpdateBrand(Brand brand);
}

public class BrandRepository : IBrandRepository
{
    private readonly IMongoContext _context;
    public BrandRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Brand>> GetAllBrands() => await _context.BrandsCollection.Find(_ => true).ToListAsync();

    public async Task<Brand> AddBrand(Brand newBrand)
    {
        await _context.BrandsCollection.InsertOneAsync(newBrand);
        return newBrand;
    }

    public async Task<Brand> GetBrand(string id) => await _context.BrandsCollection.Find<Brand>(b => b.Id == id).FirstOrDefaultAsync();

    public async Task DeleteBrand(string id)
    {
        try
        {
            var filter = Builders<Brand>.Filter.Eq("Id", id);
            var result = await _context.BrandsCollection.DeleteOneAsync(filter);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    public async Task<Brand> UpdateBrand(Brand brand)
    {
        try
        {
            var filter = Builders<Brand>.Filter.Eq("Id", brand.Id);
            var update = Builders<Brand>.Update.Set("Name", brand.Name);
            update = Builders<Brand>.Update.Set("Country", brand.Country);
            await _context.BrandsCollection.UpdateOneAsync(filter, update);
            return await GetBrand(brand.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

}