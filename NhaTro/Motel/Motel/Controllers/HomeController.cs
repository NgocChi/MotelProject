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
        //private readonly IConfiguration Configuration = null;
        //public KhachHangQueries Queries = null;
        //public KhachHangRepository Repository = null;
        //public HomeController(IConfiguration configuration)
        //{
        //    //this.Configuration = configuration;
        //    //Queries = new KhachHangQueries();
        //    //Repository = new KhachHangRepository();
        //}

        public IActionResult Index()
        {
            return View();

        }

    }
}