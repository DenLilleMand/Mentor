using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mentor.Models;
using Mentor.ViewModels;
using Mentor.Models.Repositories.Interfaces;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace Mentor.Controllers
{
    public class ProgramsController : Controller
    {
        private readonly IRepository<Program> _programRepository;

        public ProgramsController(IRepository<Program> programRepository)
        {
            _programRepository = programRepository;
        }


        // GET: Programs
        public ActionResult Index(int? id)
        {
            
            if (id.HasValue)
            {
                ProgramViewModel programViewModel = new ProgramViewModel();
                programViewModel.Program = _programRepository.Read(id);
                string currentUserIdAsString = User.Identity.GetUserId();
                var currentUserId = Convert.ToInt32(currentUserIdAsString);
                if (programViewModel.Program != null)
                {
                    foreach (var user in programViewModel.Program.Mentee)
                    {
                        if (user.Id == currentUserId)
                        {
                            programViewModel.IsMentee = true;
                        }
                    }
                    if (programViewModel.IsMentee == false)
                    {

                        foreach (var user in programViewModel.Program.Mentors)
                        {
                            if (user.Id == currentUserId)
                            {
                                programViewModel.IsMentor = true;
                            }
                        }
                    }
                    foreach (var user in programViewModel.Program.Admins)
                    {
                        if (user.Id == currentUserId)
                        {
                            programViewModel.IsAdmin = true;
                        }
                    }
                    if (programViewModel.IsAdmin == false && programViewModel.IsMentor == false &&
                        programViewModel.IsMentee == false)
                    {
                        return View("~/Views/programs/NonMemberProgram.cshtml", programViewModel);
                    }
                    else
                    {
                        return View(programViewModel);    
                    }
                }
            }
            return HttpNotFound();
        }
    }
}