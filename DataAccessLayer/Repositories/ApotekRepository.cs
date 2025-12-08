using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class ApotekRepository
{
    private readonly DatabaseContext _context;
    
    public ApotekRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public List<Apotek> GetAll()
    {
        return _context.Apoteker.ToList();
    }
    
    public Apotek GetById(int id)
    {
        return _context.Apoteker.Find(id);
    }
    
    public void Add(Apotek apotek)
    {
        _context.Apoteker.Add(apotek);
        _context.SaveChanges();
    }
    
    public void Update(Apotek apotek)
    {
        _context.Apoteker.Update(apotek);
        _context.SaveChanges();
    }
    
    public void Delete(int id)
    {
        var apotek = _context.Apoteker.Find(id);
        if (apotek != null)
        {
            _context.Apoteker.Remove(apotek);
            _context.SaveChanges();
        }
    }
    
}