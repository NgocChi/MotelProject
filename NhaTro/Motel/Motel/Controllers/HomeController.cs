using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Controls;
using Microsoft.Extensions.Configuration;
using Motel.Queries;
using Motel.Repositories;

namespace Motel.Controllers
{
 
    public class HomeController : Controller
    {
      
        public HomeController()
        {
         
        }

        public IActionResult Index()
        {
            return View();

        }

    }
}