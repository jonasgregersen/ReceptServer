using Business.Models;
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
    public ActionResult<List<Lægehus>> GetAll()
    {
        return Ok(_lægehusService.GetAll());
    }
    [HttpGet("{id}")]
    public ActionResult<Lægehus> GetById(int id)
    {
        return Ok(_lægehusService.GetById(id));
    }
    [HttpPost]
    public IActionResult Create([FromBody] Lægehus lægehus)
    {
        _lægehusService.Add(lægehus);
        return Ok();
    }
    [HttpPut]
    public IActionResult Update([FromBody] Lægehus lægehus)
    {
        _lægehusService.Update(lægehus);
        return Ok();
    }
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _lægehusService.Delete(id);
        return Ok();
    }
}