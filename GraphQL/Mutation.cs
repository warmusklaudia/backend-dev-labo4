namespace Cars.GraphQL.Mutations;

public interface IMutation
{
    Task<AddBrandPayload> AddBrand([Service(ServiceKind.Default)] ICarService carService, AddBrandInput input);
    //Task<UpdateBrandPayload> UpdateBrand([Service(ServiceKind.Default)] ICarService carService, UpdateBrandInput input);
}

public class Mutation : IMutation
{

    public async Task<AddBrandPayload> AddBrand([Service] ICarService carService, AddBrandInput input)
    {
        var newBrand = new Brand()
        {
            Name = input.name,
            Country = input.country
        };
        var created = await carService.AddBrand(newBrand);
        return new AddBrandPayload(created);
    }

    // public async Task<UpdateBrandPayload> UpdateBrand([Service] ICarService carService, UpdateBrandInput input)
    // {
    //     var updateBrand = new Brand()
    //     {
    //         Name = input.name,
    //         Country = input.country
    //     };
    //     var updated = await carService.UpdateBrand(updateBrand);
    //     return new UpdateBrandPayload(updated);
    // }
}