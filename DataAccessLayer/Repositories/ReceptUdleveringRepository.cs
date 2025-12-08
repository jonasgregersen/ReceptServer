using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class ReceptUdleveringRepository
{
    private DatabaseContext _context;
    
    public ReceptUdleveringRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public List<ReceptUdlevering> GetAll()
    {
        return _context.ReceptUdleveringer.ToList();
    }
    
    public ReceptUdlevering GetById(int id)
    {
        return _context.ReceptUdleveringer.Find(id);
    }
    
    public void Add(ReceptUdlevering receptUdlevering)
    {
        _context.ReceptUdleveringer.Add(receptUdlevering);
        _context.SaveChanges();
    }
    
    public void Update(ReceptUdlevering receptUdlevering)
    {
        _context.ReceptUdleveringer.Update(receptUdlevering);
        _context.SaveChanges();
    }
    
    public void Delete(int id)
    {
        var receptUdlevering = _context.ReceptUdleveringer.Find(id);
        if (receptUdlevering != null)
        {
            _context.ReceptUdleveringer.Remove(receptUdlevering);
            _context.SaveChanges();
        }
    }
}