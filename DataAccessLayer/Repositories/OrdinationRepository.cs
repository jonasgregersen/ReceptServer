using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class OrdinationRepository : IOrdinationRepository
{
    private readonly DatabaseContext _context;
    
    public OrdinationRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<Ordination>> GetAll()
    {
        return await _context.Ordinationer.ToListAsync();
    }
    
    public async Task<Ordination> GetById(int id)
    {
        return await _context.Ordinationer.FindAsync(id);
    }
    
    public async Task Add(Ordination ordination)
    {
        await _context.Ordinationer.AddAsync(ordination);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(Ordination ordination)
    {
        _context.Ordinationer.Update(ordination);
        await _context.SaveChangesAsync();
    }
}