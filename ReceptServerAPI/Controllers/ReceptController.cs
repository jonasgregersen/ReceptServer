using Business.Models;
using Microsoft.AspNetCore.Mvc;
using DTORecept = DataTransferLayer.Models.Recept;

namespace ReceptServerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceptController: ControllerBase
{
    private readonly ReceptService _receptService;
    
    public ReceptController(ReceptService receptService)
    {
        _receptService = receptService;
    }
    
    [HttpGet("cpr/{cprNummer}")]
    public ActionResult<List<DTORecept>> GetReceptByCpr(string cprNummer)
    {
        var result = _receptService.GetByCpr(cprNummer);
        return Ok(_receptService.GetAll());
    }

    [HttpGet("id/{id}")]
    public ActionResult<DTORecept> GetReceptById(int id)
    {
        var result = _receptService.GetById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("lægehus/{lægehusId}")] 
    public ActionResult<List<DTORecept>> GetReceptByLægehus(int lægehusId)
    {
        var result = _receptService.GetLægehusRecepter(lægehusId);
        return Ok(result);      
    }
    
    [HttpPost]
    public IActionResult CreateRecept([FromBody] DTORecept receipt)
    {
        _receptService.Add(receipt);
        return Ok();
    }
    
    [HttpPut]
    public IActionResult Update([FromBody] DTORecept receipt)
    {
        _receptService.Update(receipt);
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _receptService.Delete(id);
        return Ok();
    }
    
}