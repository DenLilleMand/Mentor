using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mentor.Models;
using Mentor.Models.Repositories.AbstractInterfaces;
using Mentor.Models.Repositories.ConcreteImplementation;
using Mentor.ViewModels;
using Mentor.Models.Repositories.Interfaces;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Mentor.customAttributes;
using Mentor.hubs;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Mentor.Controllers
{
    public class ProgramsController : Controller
    {
        private readonly ProgramRepository _programRepository;
        private readonly UserRepository _userRepository;
        private readonly InterestRepository _interestRepository;

        /* 
         * Reasoning behind our Controller setup, this comment is general for all our controllers and their logic.
         * When i made this to begin with, it was thouth out to be very flexible, with Ninject, injecting 
         * our repositories into the controller, and being able to make a mock repository for tests and at the same time
         * give repositories the option to implement individual methods w/o interupting the other classes. 
         * The result though, looks very hardcoded, because we dont use IRepository as argument forexample, 
         * but i think the looks might be deceiveing, we've actually bound the injection(NinjectWebCommon.cs) up on the 
         * abstractinterface AbstractIRepository, with type argument <Program>, <User> etc. so if we said that the goal was to
         * "implement individual methods in all repositories(because adding them to the IRepository 
         * Interface would mess up all classes." Then we've reached our goal.
         * If we said that the goal was to "get rid of duplicate code e.g. CRUD in Repositories" then that goal has succeeded aswell,
         * by abstracting it into our AbstractIRepository.  The final test is to ask ourselfes if we can test our code, 
         * right now it looks like we just take in a hardcoded dependency ProgramRepository, which allready inherits from a 
         * Abstract class that implements methods that call the database, so to make this happen, we would have to 
         * make a class that inherits from AbstractIRepository<Program>, override all CRUD methods + create the methods 
         * that are repository specific for Program, And then ninject that Concrete implementation, so maybe Ninject 
         * allows us to implement kind of badly thought out design, but it works. 
         */

        public ProgramsController(ProgramRepository programRepository, UserRepository userRepository,
            InterestRepository interestRepository)
        {
            _programRepository = programRepository;
            _userRepository = userRepository;
            _interestRepository = interestRepository;
        }

        //Burde lave en lille state klasse, der  indeholder vores currentUser, pr. request.
        // GET: Programs
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                ProgramViewModel programViewModel = new ProgramViewModel();
                programViewModel.Program = _programRepository.Read(id);
                Debug.WriteLine("Program haps 2:" + programViewModel.Program);
                    //i rly hate to cast things, eww, but might be nessecery.
                programViewModel.ProgramId = id;
                int currentUserId = User.Identity.GetUserId<int>();
                programViewModel.CurrentUser = _userRepository.Read(currentUserId);
                System.Diagnostics.Debug.WriteLine("User:" + programViewModel.CurrentUser);
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
                        programViewModel.ProgramApplication = new ProgramApplication();
                        programViewModel.ProgramApplication.SendingUser = programViewModel.CurrentUser;
                        return View("~/Views/programs/NonMemberProgram.cshtml", programViewModel);
                    }
                    return View(programViewModel);
                }
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult MyPrograms()
        {
            MyProgramsViewModel myProgramsViewModel = new MyProgramsViewModel();
            myProgramsViewModel.CurrentUser = _userRepository.Read(User.Identity.GetUserId<int>());
            myProgramsViewModel.ProgramSurgestions = _programRepository.RetrieveAllPrograms().ToList();
            return View(myProgramsViewModel);
        }

        [HttpGet]
        public ActionResult CreateProgram()
        {
            CreateProgramViewModel createProgramViewModel = new CreateProgramViewModel();
            createProgramViewModel.CurrentUser = _userRepository.Read(User.Identity.GetUserId<int>());
            Debug.WriteLine("Result of currentUser CreateProgramL" + createProgramViewModel.CurrentUser);
            createProgramViewModel.ProgramToBeCreated = new Program();
            createProgramViewModel.UndefinedInterests = createProgramViewModel.CurrentUser.UndefinedInterests;
            return View(createProgramViewModel);
        }

        [HttpPost]
        [NoCache]
        public ActionResult CreateProgram(CreateProgramViewModel createProgramViewModel)
        {
            Program newProgram = createProgramViewModel.ProgramToBeCreated;
            int interestId = Int32.Parse(createProgramViewModel.InterestId);
            newProgram.Interest = _interestRepository.Read(interestId);
            Visibility result;
            if (Visibility.TryParse(createProgramViewModel.VisibilityHolder, true, out result))
            {
                newProgram.Visibility = result;
            }
            else
            {
                Debug.WriteLine(
                    "Something went wrong when trying to create a new program and the visibility couldnt be parsed to a  enum.");
            }
            HttpPostedFileBase file = createProgramViewModel.File;
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/pictures"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    newProgram.Picture = array;
                }
            }
            else
            {
                Debug.WriteLine("File is null at CreateProgram in  ProgramsController");
            }

            newProgram.Creator = _userRepository.Read(User.Identity.GetUserId<int>());
            newProgram.Mentors = new List<User>()
            {
                newProgram.Creator
            };
            newProgram.Admins = new List<User>()
            {
                newProgram.Creator
            };
            Debug.WriteLine("Result of cReatpr CreateProgram post" + newProgram.Creator);
            int id = _programRepository.Create(newProgram);
            var context = GlobalHost.ConnectionManager.GetHubContext<MyProgramsHub>();
            Debug.WriteLine("The id from the inserted program:" + id);
            context.Clients.All.programWasAdded(id, newProgram.Name);
            return RedirectToAction("Index", new {Id = id});
        }

        public ActionResult GetPicture(int id)
        {
            byte[] imageData = _programRepository.Read(id).Picture;
            if (imageData == null)
            {
                var dir = Server.MapPath("/Content/Images");
                var path = Path.Combine(dir, "default.png");
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "image/png");
            }
            return File(imageData, "image/png"); // Might need to adjust the content type based on your actual image type
        }

        public ActionResult ProgramApplication(ProgramViewModel model)
        {
            ProgramApplication application = model.ProgramApplication;
            Debug.WriteLine("model hap haps:" + model);
            Program program = _programRepository.Read(model.ProgramId);
            Debug.WriteLine("Program hap haps:" + program);
            Debug.WriteLine("programApplications hap haps:" + program.ProgramApplications);

            program.ProgramApplications.Add(application);
            application.SendingUser = _userRepository.Read(User.Identity.GetUserId<int>());
            if (application.Role == Role.Mentor)
            {
                program.Mentors.Add(application.SendingUser); 
            }
            else //if Role == Role.Mentee
            {
                program.Mentee.Add(application.SendingUser);
            }
            _programRepository.Update(program);
            return RedirectToAction("Index", new { Id = program.Id}); //current state of this implementation, obviously the person should only have access to the program after a mentor
                        //accepted the request. 
        }


        public ActionResult WishList()
        {
            WishListViewModel wishListViewModel = new WishListViewModel();
            wishListViewModel.CurrentUser = _userRepository.Read(User.Identity.GetUserId<int>());
            wishListViewModel.UndefinedInterests = wishListViewModel.CurrentUser.UndefinedInterests;
            return View(wishListViewModel);
        }

        [HttpPost]
        public ActionResult CreateWish(WishListViewModel model)
        {
            //skal have lavet en interest
            Interest interest = new Interest(model.Interest);
            //skal have fat i currentUser,
            User currentUser = _userRepository.Read(User.Identity.GetUserId<int>());
            //skal have converteret vores program til et rigtigt program
            Program program = new Program(interest, currentUser,model.Description);
            int newId = _programRepository.Create(program);
            //skal ligge det ind i databasen, for at skaffe det nye Id, så returnere vi en ViewModel
            //måske burde vi kalde dette med AJAX, sådan at vi har adgang til callback program, til at indsætte. 
            var context = GlobalHost.ConnectionManager.GetHubContext<MyProgramsHub>();
            var serializedViewModel = JsonConvert.SerializeObject(new WishListViewModel()
            {
                Id = newId,
                Interest = model.Interest,
                Name = model.Name,
                Description = model.Description,
                AcceptCriteria = model.AcceptCriteria
            }, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            WishListInMemoryRepository.GetInstance().AddProgram();
            context.Clients.All.programWasAdded(serializedViewModel);
            return RedirectToAction("WishList");

        }
    }
}