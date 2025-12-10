using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Recept = DataTransferLayer.Models.Recept;

namespace ReceptServer.Controllers
{
    public class ApotekController : Controller
    {
        private readonly ApotekService _apotekBLL;
        private readonly ReceptService _receptBLL;

        public ApotekController(ApotekService apotekBLL, ReceptService receptBLL)
        {
            _apotekBLL = apotekBLL;
            _receptBLL = receptBLL;
        }
        // GET: ApotekController
        public ActionResult Index()
        {
            var apoteker = _apotekBLL.GetAll();
            return View(apoteker);
        }

        [Route("apotek/{id:int}")]
        public ActionResult Index(int id)
        {
            var apotek = _apotekBLL.GetById(id);
            HttpContext.Session.SetInt32("ApotekId", id);
            return View("ApotekDetail", apotek);

        }
        [HttpGet]
        [Route("apotek/SearchRecept")]
        public ActionResult Search(string query)
        {
            if (!ValidateApotek())
            {
                return RedirectToAction("Index", null);
            }
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("SearchRecept");
            }
            HttpContext.Session.SetString("CprNummer", query);
            var recept = _receptBLL.GetByCpr(query);
            return View("SearchReceptResult", recept);
        }
        [HttpPost]
        [Route("apotek/UdleverOrdination")]
        public async Task<ActionResult> UdleverOrdination(int antal, int receptId, int ordinationId)
        {
            var cprNummer = HttpContext.Session.GetString("CprNummer");
            if (!ValidateApotek())
            {
                return RedirectToAction("Index", null);
            }
            var apotekId = HttpContext.Session.GetInt32("ApotekId").Value;
            if (antal <= 0)
            {
                TempData["Error"] = "Antal skal være større end 0.";
                return View("SearchReceptResult", _receptBLL.GetByCpr(cprNummer));
            }
            try 
            {
                await _apotekBLL.UdleverOrdination(ordinationId, apotekId, receptId, antal);
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View("SearchReceptResult", _receptBLL.GetByCpr(cprNummer));
        }
        private bool ValidateApotek()
        {
            return HttpContext.Session.GetString("ApotekId") != null;
        }
        // GET: ApotekController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
      
    }
}

