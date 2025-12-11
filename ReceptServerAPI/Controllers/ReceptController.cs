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
    public async Task<ActionResult<List<DTORecept>>> GetReceptByCpr(string cprNummer)
    {
        var result = await _receptService.GetByCpr(cprNummer);
        return Ok(result);
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<DTORecept>> GetReceptById(int id)
    {
        var result = await _receptService.GetById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("lægehus/{lægehusId}")] 
    public async Task<ActionResult<List<DTORecept>>> GetReceptByLægehus(int lægehusId)
    {
        var result = await _receptService.GetLægehusRecepter(lægehusId);
        return Ok(result);      
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRecept([FromBody] DTORecept recept)
    {
        await _receptService.Add(recept);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DTORecept recept)
    {
        await _receptService.Update(recept);
        return Ok();
    }   
}