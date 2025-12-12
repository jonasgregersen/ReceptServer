using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class ApotekRepository : IApotekRepository
{
    private readonly DatabaseContext _context;
    
    public ApotekRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<Apotek>> GetAll()
    {
        return await _context.Apoteker.ToListAsync();
    }
    
    public async Task<Apotek> GetById(int id)
    {
        return await _context.Apoteker.FindAsync(id);
    }
    
    public async Task Add(Apotek apotek)
    {
        await _context.Apoteker.AddAsync(apotek);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(Apotek apotek)
    {
        _context.Apoteker.Update(apotek);
        await _context.SaveChangesAsync();
    }
    
}