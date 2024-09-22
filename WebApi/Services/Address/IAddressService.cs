using WebApi.DTO.Address;

namespace WebApi.Services.Address;

public interface IAddressService
{
    public Task<AddressDto> GetAddressByIdAsync(int id);
    public Task<AddressDto> CreateAddressByIdAsync(AddressCreateDto addressDto);
    public Task<AddressDto> UpdateAddressAsync(int id, AddressCreateDto addressDto);
    public Task<bool> DeleteAddressAsync(int id);
}