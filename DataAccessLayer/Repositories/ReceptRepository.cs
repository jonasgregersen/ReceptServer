using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ReceptRepository
{
    private readonly DatabaseContext _context;
    
    public ReceptRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<Recept>> GetByCpr(string cprNummer)
    {
        return await _context.Recepter
            .Include(r => r.Ordinationer)
            .Where(r => r.CprNummer == cprNummer).ToListAsync();
    }
    public async Task<List<Recept>> GetAll()
    {
        return await _context.Recepter
            .Include(r => r.Ordinationer)
            .ToListAsync();
    }

    public async Task<List<Recept>> GetByLaegehusId(int id)
    {
        return await _context.Recepter
            .Include(r => r.Ordinationer)
            .Where(r => r.LægehusId == id)
            .ToListAsync();
    }

    public async Task<Recept?> GetById(int id)
    {
        return await _context.Recepter
            .Include(r => r.Ordinationer)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
    public async Task Add(Recept recept)
    {
        await _context.Recepter.AddAsync(recept);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Recept recept)
    {
        if (recept != null)
        {
            _context.Recepter.Update(recept);
            await _context.SaveChangesAsync();
        }
    
    }
}