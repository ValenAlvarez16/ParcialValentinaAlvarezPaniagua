using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketingSystem.DAL.Entities;

namespace WebApplication1.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public TicketsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var url = "https://localhost:7032/api/Tickets/Get";
                var json = await _httpClient.CreateClient().GetStringAsync(url);
                List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
                return View(tickets);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
