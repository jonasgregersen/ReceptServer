using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class OrdinationRepository
{
    private DatabaseContext _context;
    
    public OrdinationRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public List<Ordination> GetAll()
    {
        return _context.Ordinationer.ToList();
    }
    
    public Ordination GetById(int id)
    {
        return _context.Ordinationer.Find(id);
    }
    
    public void Add(Ordination ordination)
    {
        _context.Ordinationer.Add(ordination);
        _context.SaveChanges();
    }
    
    public void Update(Ordination ordination)
    {
        _context.Ordinationer.Update(ordination);
        _context.SaveChanges();
    }
    
    public void Delete(int id)
    {
        var ordination = _context.Ordinationer.Find(id);
        if (ordination != null)
        {
            _context.Ordinationer.Remove(ordination);
            _context.SaveChanges();
        }
    }
}