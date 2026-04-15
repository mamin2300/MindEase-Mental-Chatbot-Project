using Microsoft.AspNetCore.Mvc;
using MindEase_Mental_Chatbot_Project.Data;
using MindEase_Mental_Chatbot_Project.Models;
using MindEase_Mental_Chatbot_Project.Services;

namespace MindEase_Mental_Chatbot_Project.Controllers
{
    public class TriageController : Controller
    {
        private readonly ITriageService _triageService;
        private readonly AppDbContext _context;

        public TriageController(ITriageService triageService, AppDbContext context)
        {
            _triageService = triageService;
            _context = context;
        }

        // GET: /Triage
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Triage/AssessRisk
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssessRisk(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return RedirectToAction(nameof(Index));

            var result = _triageService.AssessRisk(message);
            _context.TriageResults.Add(result);
            await _context.SaveChangesAsync();

            if (_triageService.IsCrisisLevel(result.RiskLevel))
                return RedirectToAction(nameof(CrisisProtocol), new { id = result.Id });

            return View("AssessRisk", result);
        }

        // GET: /Triage/CrisisProtocol/5
        public async Task<IActionResult> CrisisProtocol(int id)
        {
            var result = await _context.TriageResults.FindAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }
    }
}