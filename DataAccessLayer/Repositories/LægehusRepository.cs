using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class LægehusRepository : ILægehusRepository
{
    private readonly DatabaseContext _context;
    
    public LægehusRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<Lægehus>> GetAll()
    {
        return await _context.Lægehus.ToListAsync();
    }
    
    public async Task<Lægehus> GetById(int id)
    {
        return await _context.Lægehus.FindAsync(id);
    }

    public async Task<Lægehus> GetByYdernummer(string ydernummer)
    {
        return await _context.Lægehus.FirstOrDefaultAsync(l => l.YderNummer.Equals(ydernummer));
    }
    
    public async Task Add(Lægehus lægehus)
    {
        await _context.Lægehus.AddAsync(lægehus);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(Lægehus lægehus)
    {
        _context.Lægehus.Update(lægehus);
        await _context.SaveChangesAsync();
    }
    
    public async Task Delete(int id)
    {
        var lægehus = await _context.Lægehus.FindAsync(id);
        if (lægehus != null)
        {
            _context.Lægehus.Remove(lægehus);
            await _context.SaveChangesAsync();
        }
    }
}