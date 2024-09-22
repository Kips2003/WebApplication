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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressById(int id)
    {
        var address = await _address.GetAddressByIdAsync(id);
        if (address is null)
            return NotFound();

        return Ok(address);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(AddressCreateDto cartCreate)
    {
        var address = await _address.CreateAddressByIdAsync(cartCreate);
        return CreatedAtAction(nameof(GetAddressById), new { Id = address.Id }, address);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, AddressCreateDto addressCreate)
    {
        var address = await _address.UpdateAddressAsync(id, addressCreate);

        if (address is null)
            return NotFound();

        return Ok(address);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var result = await _address.DeleteAddressAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}