using System.Web.Mvc;
using Mentor.Models.Repositories.Interfaces;
using Mentor.Models;

namespace Mentor.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IRepository<User> _userRepository;

        public DashBoardController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
 
        // GET: DashBoard
        public ActionResult Index(int id)
        {

            return View(_userRepository.Read(id));
        }
    }
}