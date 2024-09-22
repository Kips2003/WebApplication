namespace WebApi.Services.Address;

public interface IAddressRepository
{
    public Task<Models.Address> GetAddressByIdAsync(int id);
    public Task<Models.Address> CreateAddressAsync(Models.Address address);
    public Task<Models.Address> UpdateAddressAsync(Models.Address address);
    public Task<bool> DeleteAddressAsync(int id);
}