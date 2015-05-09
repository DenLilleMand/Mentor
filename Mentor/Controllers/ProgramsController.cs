using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mentor.Models;
using Mentor.ViewModels;
using Mentor.Models.Repositories.Interfaces;
using Microsoft.Ajax.Utilities;

namespace Mentor.Controllers
{
    public class ProgramsController : Controller
    {
        private IRepository<Program> _programRepository;

        public ProgramsController(IRepository<Program> programRepository)
        {
            _programRepository = programRepository;
        }


        // GET: Programs
        public ActionResult Index(int? id)
        {
            ProgramViewModel programViewModel = new ProgramViewModel();
            if (id.HasValue)
            {
                programViewModel.Program = _programRepository.Read(id);
            }
            else
            {
                return HttpNotFound();
            }
            return View(programViewModel);
        }
    }
}