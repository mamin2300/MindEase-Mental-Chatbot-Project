
using Microsoft.AspNetCore.Mvc;
using MindEase_Mental_Chatbot_Project.Models;
using MindEase_Mental_Chatbot_Project.Services;
namespace MindEase_Mental_Chatbot_Project.Controllers
{

    // Author: Malika
    // Handles all analytics and report related requests for the campus wellness admin.
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        // Constructor - IAnalyticsService injected via dependency injection
        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        // GET: Analytics/Index - landing page for analytics
        public async Task<IActionResult> Index()
        {
            var reports = await _analyticsService.GetAllReportsAsync();
            return View(reports);
        }

        // GET: Analytics/Dashboard - shows summary metrics
        public async Task<IActionResult> Dashboard()
        {
            var summary = await _analyticsService.GetDashboardSummaryAsync();
            return View(summary);
        }

        // GET: Analytics/GenerateReport - shows the report generation form
        
        public IActionResult GenerateReport()
        {
            return View(new MentalHealthReport());
        }

        // POST: Analytics/GenerateReport - saves a new report
        [HttpPost]
        public async Task<IActionResult> GenerateReport(MentalHealthReport report)
        {
            if (ModelState.IsValid)
            {
                await _analyticsService.AddReportAsync(report);
                return RedirectToAction("Index");
            }
            return View(report);
        }

        // GET: Analytics/ViewReport/5 - view a single report
        public async Task<IActionResult> ViewReport(int id)
        {
            var report = await _analyticsService.GetReportByIdAsync(id);
            if (report == null) return NotFound();
            return View(report);
        }
    }
}