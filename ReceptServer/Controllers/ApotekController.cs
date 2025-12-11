using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<ActionResult> Index()
        {
            var apoteker = await _apotekBLL.GetAll();
            return View(apoteker);
        }

        [Route("apotek/{id:int}")]
        public async Task<ActionResult> Index(int id)
        {
            var apotek = await _apotekBLL.GetById(id);
            HttpContext.Session.SetInt32("ApotekId", id);
            return View("ApotekDetail", apotek);

        }
        [HttpGet]
        [Route("apotek/SearchRecept")]
        public async Task<ActionResult> Search(string query)
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
            var cprRecepter = await _receptBLL.GetByCpr(query);
            if (!await CheckAktivRecepter(query))
            {
                TempData["Error"] = "Ingen aktive recepter fundet på CPR nummer.";
                return View("SearchRecept");
            }
            return View("SearchReceptResult", cprRecepter);
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
                return View("SearchReceptResult", await _receptBLL.GetByCpr(cprNummer));
            }
            try 
            {
                await _apotekBLL.UdleverOrdination(ordinationId, apotekId, receptId, antal);
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View("SearchReceptResult", await _receptBLL.GetByCpr(cprNummer));
        }

        private bool ValidateApotek()
        {
            return HttpContext.Session.GetString("ApotekId") != null;
        }


        private async Task<bool> CheckAktivRecepter(string cpr)
        {
            var cprRecepter = await _receptBLL.GetByCpr(cpr);
            if (cprRecepter.IsNullOrEmpty())
            {
                return false;
            }
            foreach(var recept in cprRecepter)
            {
                if (!recept.Lukket)
                {
                    return true;
                }
            }
            return false;
        }      
    }
}

