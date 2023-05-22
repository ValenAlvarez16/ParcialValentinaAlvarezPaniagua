using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Sockets;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            try
            {
                var url = "https://localhost:7032/api/Tickets/Create";
                await _httpClient.CreateClient().PostAsJsonAsync(url, ticket);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var url = String.Format("https://localhost:7032/api/Tickets/Get/{0}", id);
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Ticket ticket)
        {
            var url = String.Format("https://localhost:7032/api/Tickets/Edit/{0}", id);
            await _httpClient.CreateClient().PutAsJsonAsync(url, ticket);
            return RedirectToAction("Index");
               
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var url = String.Format("https://localhost:7032/api/Tickets/Get/{0}", id);
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            Ticket ticket= JsonConvert.DeserializeObject<Ticket>(json);
            return View(ticket);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id, Ticket ticket)
        {
            var url = String.Format("https://localhost:7032/api/Tickets/Delete/{0}", id);
            await _httpClient.CreateClient().DeleteAsync(url);
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Route("Validate/{id}")]
        public async Task<IActionResult> Validate(Guid? id)
        {
            var url = String.Format("https://localhost:7032/api/Tickets/Get/{0}", id);
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);

            if (ticket == null)
            {
                return NotFound("The Ticket is not valid");
            }

            return View(ticket);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate(Guid? id, Ticket ticket)
        {
            if (id == null)
            {
                return BadRequest("Invalid ticket ID");
            }

            var url = $"https://localhost:7032/api/Tickets/Edit/{id}";
            var response = await _httpClient.CreateClient().PutAsJsonAsync(url, ticket);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }



    }
}
