using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApi.Services.Address;

public interface IAddressRepository
{
    public IEnumerable<Models.Address> GetAddressesAsync();
    public Task<Models.Address> GetAddressByIdAsync(int id);
    public Task<IEnumerable<Models.Address>> GetAddressByUserIdAsync(int userId);
    public Task<Models.Address> CreateAddressAsync(Models.Address address);
    public Task<Models.Address> UpdateAddressAsync(Models.Address address);
    public Task<bool> DeleteAddressAsync(int id);
}