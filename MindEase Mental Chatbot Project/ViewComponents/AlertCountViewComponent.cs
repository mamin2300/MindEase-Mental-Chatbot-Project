using Microsoft.AspNetCore.Mvc;
using MindEase_Mental_Chatbot_Project.Data;

namespace MindEase_Mental_Chatbot_Project.ViewComponents
{
    public class AlertCountViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public AlertCountViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var count = _context.CrisisAlerts.Count(a => !a.IsResolved);
            return View(count);
        }
    }
}