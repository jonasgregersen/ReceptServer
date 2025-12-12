using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class ReceptUdleveringRepository : IReceptUdleveringRepository
{
    private readonly DatabaseContext _context;
    
    public ReceptUdleveringRepository(DatabaseContext context)
    {
        _context = context;
    }   
    public async Task Add(ReceptUdlevering receptUdlevering)
    {
        await _context.ReceptUdleveringer.AddAsync(receptUdlevering);
        await _context.SaveChangesAsync();
    }
   
}