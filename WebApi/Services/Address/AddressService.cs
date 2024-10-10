using WebApi.DTO;
using WebApi.DTO.Address;
using WebApi.DTO.Product;
using WebApi.Services.Authentication;

namespace WebApi.Services.Address;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IUserRepository _userRepository;

    public AddressService(IAddressRepository addressRepository, IUserRepository userRepository)
    {
        _addressRepository = addressRepository;
        _userRepository = userRepository;
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
    
    public async Task<AuthResponseDto> UpdateAddressAsync(int userId, AddressDto addressDto)
    {
        var address = await _addressRepository.GetAddressByIdAsync(addressDto.Id);
        if (address is null)
            return new AuthResponseDto{Success = false, Message = "This address does not exist"};
        
        var user = await _userRepository.getUserById(userId);
        if (user is null)
            return new AuthResponseDto{Success = false, Message = "User does now exist"};

        if (addressDto.UserId != userId && user.PrivilageId != 1 && user.PrivilageId != 2)
        {
            return new AuthResponseDto { Success = false, Message = "You do not have access to change this address" };
        }

        address.State = addressDto.State;
        address.City = addressDto.City;
        address.Street = addressDto.Street;
        address.PostalCode = addressDto.PostalCode;
        address.Country = addressDto.Country;

        await _addressRepository.UpdateAddressAsync(address);

        return new AuthResponseDto() {Success = true, Message = "Address updated successfully"};
    }

    public async Task<AuthResponseDto> DeleteAddressAsync(int userId, int id)
    {
        if (id == null)
            return new AuthResponseDto { Success = false, Message = "Provided Number is null" };

        var address = await _addressRepository.GetAddressByIdAsync(id);
        if (address is null)
            return new AuthResponseDto { Success = false, Message = "There is no Address on this ID" };
        
        var user = await _userRepository.getUserById(userId);
        if (user is null)
            return new AuthResponseDto { Success = false, Message = "User does now exist" };
        
        if (address.UserId != userId && user.PrivilageId != 1 && user.PrivilageId != 2)
        {
            return new AuthResponseDto { Success = false, Message = "Youdo not have access to change this address" };
        }
        
        await _addressRepository.DeleteAddressAsync(id);

        return new AuthResponseDto { Success = true, Message = "Address Removed from the database" };
    }
}