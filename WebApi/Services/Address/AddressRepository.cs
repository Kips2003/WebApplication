using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Services.Address;

public class AddressRepository : IAddressRepository
{
    private readonly DataContext _context;

    public AddressRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Models.Address> GetAddressByIdAsync(int id)
    {
        return await _context.Addresses.FindAsync(id);
    }

    public async Task<Models.Address> CreateAddressAsync(Models.Address address)
    {
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<Models.Address> UpdateAddressAsync(Models.Address address)
    {
        _context.Entry(address).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<bool> DeleteAddressAsync(int id)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address is null)
            return false;

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
        return true;
    }
}