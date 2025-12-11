using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ReceptUdleveringRepository
{
    private DatabaseContext _context;
    
    public ReceptUdleveringRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<ReceptUdlevering>> GetAll()
    {
        return await _context.ReceptUdleveringer.ToListAsync();
    }
    
    public async Task<ReceptUdlevering> GetById(int id)
    {
        return await _context.ReceptUdleveringer.FindAsync(id);
    }
    
    public async Task Add(ReceptUdlevering receptUdlevering)
    {
        await _context.ReceptUdleveringer.AddAsync(receptUdlevering);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(ReceptUdlevering receptUdlevering)
    {
        _context.ReceptUdleveringer.Update(receptUdlevering);
        await _context.SaveChangesAsync();
    }
}