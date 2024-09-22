using WebApi.DTO.Address;
using WebApi.DTO.Product;

namespace WebApi.Services.Address;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    
    public async Task<AddressDto> GetAddressByIdAsync(int id)
    {
        var address = await _addressRepository.GetAddressByIdAsync(id);
        if (address is null)
            return null;

        return new AddressDto
        {
            Id = address.Id,
            UserId = address.UserId,
            Street = address.Street,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }

    public async Task<AddressDto> CreateAddressByIdAsync(AddressCreateDto addressDto)
    {
        var address = new Models.Address
        {
            UserId = addressDto.UserId,
            State = addressDto.State,
            City = addressDto.City,
            PostalCode = addressDto.PostalCode,
            Country = addressDto.Country,
            Street = addressDto.Street
        };

        address = await _addressRepository.CreateAddressAsync(address);

        return new AddressDto
        {
            Id = address.Id,
            UserId = address.UserId,
            State = address.State,
            City = address.City,
            PostalCode = address.PostalCode,
            Country = address.Country,
            Street = address.Street
        };
    }

    public async Task<AddressDto> UpdateAddressAsync(int id, AddressCreateDto addressDto)
    {
        var address = await _addressRepository.GetAddressByIdAsync(id);
        if (address is null)
            return null;

        address.UserId = addressDto.UserId;
        address.State = addressDto.State;
        address.City = addressDto.City;
        address.Street = addressDto.Street;
        address.PostalCode = addressDto.PostalCode;
        address.Country = addressDto.Country;

        address = await _addressRepository.UpdateAddressAsync(address);

        return new AddressDto
        {
            UserId = address.UserId,
            State = address.State,
            City = address.City,
            Street = address.Street,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }

    public async Task<bool> DeleteAddressAsync(int id)
    {
        return await _addressRepository.DeleteAddressAsync(id);
    }
}