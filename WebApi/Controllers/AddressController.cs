using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Address;
using WebApi.DTO.Cart;
using WebApi.Services.Address;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _address;

    public AddressController(IAddressService address)
    {
        _address = address;
    }

    [HttpGet]
    public async Task<IActionResult> GetAddresses()
    {
        var addresses = await _address.GetAddressesAsync();

        return Ok(addresses);
    }
    [HttpGet("byId/{id}")]
    public async Task<IActionResult> GetAddressById(int id)
    {
        var address = await _address.GetAddressesByIdAsync(id);
        if (address is null)
            return NotFound();

        return Ok(address);
    }

    [HttpGet("byUserId/{userId}")]
    public async Task<IActionResult> GetAddressesByUserId(int userId)
    {
        var addresses = await _address.GetAddressBuUserIdAsync(userId);

        return Ok(addresses);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAddress(AddressCreateDto cartCreate)
    {
        var address = await _address.CreateAddressByIdAsync(cartCreate);
        return CreatedAtAction(nameof(GetAddressById), new { Id = address.Id }, address);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateAddress(int userId, AddressDto addressCreate)
    {
        var address = await _address.UpdateAddressAsync(userId, addressCreate);

        if (!address.Success)
            return BadRequest(address.Message);

        return Ok(address.Message);
    }

    [HttpDelete("{userId}/{id}")]
    public async Task<IActionResult> DeleteAddress(int userId, int id)
    {
        var result = await _address.DeleteAddressAsync(userId, id);
        if (!result.Success)
            return BadRequest(result.Message);
            
        return Ok(result.Message);
    }
}