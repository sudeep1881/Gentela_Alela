using gentela_Alela.Models;
using Microsoft.AspNetCore.Mvc;

namespace gentela_Alela.Controllers
{
    public class RouteController : Controller
    {
        private readonly GentleProjectContext _db;

         public RouteController(GentleProjectContext db)
        {
            _db = db;
        }

        public IActionResult Open(string key)
        {

            var route = _db.RouteLinks.FirstOrDefault(x => x.Key == key);

            if (route == null)
                return NotFound();

            return RedirectToAction(route.Action, route.Controller,
                new { id = route.ParamId });

        }
    }
}