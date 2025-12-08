using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class ReceptRepository
{
    private DatabaseContext _context;
    
    public ReceptRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public List<Recept> GetByCpr(string cprNummer)
    {
        return _context.Recepter.Where(r => r.CprNummer == cprNummer).ToList();
    }
    public List<Recept> GetAll()
    {
        return _context.Recepter.ToList();
    }
    
    public Recept GetById(int id)
    {
        return _context.Recepter.Find(id);
    }
    
    public void Add(Recept recept)
    {
        _context.Recepter.Add(recept);
        _context.SaveChanges();
    }

    public void Update(int id, Recept recept)
    {
        _context.Recepter.Find(id);
        if (recept != null)
        {
            _context.Recepter.Update(recept);
            _context.SaveChanges();
        }
    
    }
    
    public void Delete(int id)
    {
        var recept = _context.Recepter.Find(id);
        if (recept != null)
        {
            _context.Recepter.Remove(recept);
            _context.SaveChanges();
        }
    }
}