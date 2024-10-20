using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTO.Address;

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

    public async Task<IEnumerable<Models.Address>> GetAddressesAsync()
    {
        return await _context.Addresses.ToListAsync();
    }
    public async Task<IEnumerable<Models.Address>> GetAddressByUserIdAsync(int userId)
    {
        // Await the addresses list and filter the result using LINQ
        var addresses = await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
        return addresses;    
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