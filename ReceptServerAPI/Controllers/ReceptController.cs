using Business.Models;
using Microsoft.AspNetCore.Mvc;
using DTOReceipt = DataTransferLayer.Models.Recept;

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
    
    [HttpGet]
    public ActionResult<List<DTOReceipt>> GetReceiptByCpr(string cprNummer)
    {
        var result = _receptService.GetByCpr(cprNummer);
        return Ok(_receptService.GetAll());
    }
    
    
    [HttpPost]
    public IActionResult CreateReceipt([FromBody] DTOReceipt receipt)
    {
        _receptService.Add(receipt);
        return Ok();
    }
    
    [HttpPut]
    public IActionResult Update(int id, [FromBody] DTOReceipt receipt)
    {
        _receptService.Update(id, receipt);
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _receptService.Delete(id);
        return Ok();
    }
    
}