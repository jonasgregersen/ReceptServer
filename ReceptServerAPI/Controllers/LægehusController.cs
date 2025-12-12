using Business.Services;
using DataTransferLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ReceptServerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LægehusController : ControllerBase
{
    private readonly LægehusService _lægehusService;

    public LægehusController(LægehusService lægehusService)
    {
        _lægehusService = lægehusService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Lægehus>>> GetAll()
    {
        return Ok(await _lægehusService.GetAll());
    }
    [HttpGet("{yderNummer}")]
    public async Task<ActionResult<Lægehus>> GetByYdernummer(string yderNummer)
    {

         var lægehus = await _lægehusService.GetByYdernummer(yderNummer);

            if (lægehus == null)
            {
                return NotFound();
            }
            return Ok(lægehus);
        
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Lægehus lægehus)
    {
        await _lægehusService.Add(lægehus);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Lægehus lægehus)
    {
        await _lægehusService.Update(lægehus);
        return Ok();
    }
}