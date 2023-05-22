using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketingSystem.DAL.Entities;
using WebApplication1.Models;

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

        public async Task<IActionResult> Index()
        {
            
                var url = "https://localhost:7032/api/Tickets/Get";
                var json = await _httpClient.CreateClient().GetStringAsync(url);
                List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
                return View(tickets);
            
           

        }

    }   
}
