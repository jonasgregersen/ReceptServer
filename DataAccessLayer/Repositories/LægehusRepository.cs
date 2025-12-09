using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class LægehusRepository
{
    private DatabaseContext _context;
    
    public LægehusRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public List<Lægehus> GetAll()
    {
        return _context.Lægehus.ToList();
    }
    
    public Lægehus GetById(int id)
    {
        return _context.Lægehus.Find(id);
    }

    public Lægehus GetByYdernummer(string ydernummer)
    {
        return _context.Lægehus.FirstOrDefault(l => l.YderNummer.Equals(ydernummer));
    }
    
    public void Add(Lægehus lægehus)
    {
        _context.Lægehus.Add(lægehus);
        _context.SaveChanges();
    }
    
    public void Update(Lægehus lægehus)
    {
        _context.Lægehus.Update(lægehus);
        _context.SaveChanges();
    }
    
    public void Delete(int id)
    {
        var lægehus = _context.Lægehus.Find(id);
        if (lægehus != null)
        {
            _context.Lægehus.Remove(lægehus);
            _context.SaveChanges();
        }
    }
}