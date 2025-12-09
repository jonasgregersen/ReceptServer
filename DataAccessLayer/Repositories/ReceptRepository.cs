using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

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
        return _context.Recepter
            .Include(r => r.Ordinationer)
            .Where(r => r.CprNummer == cprNummer).ToList();
    }
    public List<Recept> GetAll()
    {
        return _context.Recepter
            .Include(r => r.Ordinationer)
            .ToList();
    }

    public List<Recept> GetByLægehusId(int id)
    {
        return _context.Recepter
            .Include(r => r.Ordinationer)
            .Where(r => r.LægehusId == id)
            .ToList();
    }

    public Recept? GetById(int id)
    {
        return _context.Recepter
            .Include(r => r.Ordinationer)
            .FirstOrDefault(r => r.Id == id);
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