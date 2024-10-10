using WebApi.DTO;
using WebApi.DTO.Address;

namespace WebApi.Services.Address;

public interface IAddressService
{
    public Task<AddressDto> GetAddressByIdAsync(int id);
    public Task<AddressDto> CreateAddressByIdAsync(AddressCreateDto addressDto);
    public Task<AuthResponseDto> UpdateAddressAsync(int userId, AddressDto addressDto);
    public Task<AuthResponseDto> DeleteAddressAsync(int userId, int id);
}