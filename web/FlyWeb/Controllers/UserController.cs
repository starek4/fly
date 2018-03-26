using System.Diagnostics;
using DatabaseController.Repositories;
using Microsoft.AspNetCore.Mvc;
using FlyWeb.Models;
using Microsoft.AspNetCore.Http;

namespace FlyWeb.Controllers
{
    public class UserController : Controller
    {
        private static readonly UserRepository UserRepository = new UserRepository();
        private const string LoginSessionKey = "login";
        private const string StatusKey = "login";
        private const string WrongCredentialsStatus = "Wrong credentials";
        private const string UserAlreadyExistStatus = "This user already exist";
        private const string AddingUserOkStatus = "Creating user sucessfull! You can login now";
        private const string AddingUserFailedStatus = "This user already exist";

        [HttpGet]
        public IActionResult Index()
        {
            UserViewModel viewModel = new UserViewModel();
            if (!string.IsNullOrEmpty(TempData[StatusKey] as string))
            {
                viewModel.Status = (string) TempData[StatusKey];
            }
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(LoginSessionKey)))
                return RedirectToAction("Index", "Admin");
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(string login, string password)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(LoginSessionKey)))
                return RedirectToAction("Index", "Admin");

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                TempData[StatusKey] = WrongCredentialsStatus;
                return RedirectToAction("Index", "User");
            }
            bool logged = UserRepository.VerifyUser(login, password);
            if (logged)
            {
                HttpContext.Session.SetString(LoginSessionKey, login);
                return RedirectToAction("Index", "Admin");
            }
            TempData[StatusKey] = WrongCredentialsStatus;
            return RedirectToAction("Index", "User");
        }




        [HttpGet]
        public IActionResult Registration()
        {
            UserViewModel viewModel = new UserViewModel();
            if (!string.IsNullOrEmpty(TempData[StatusKey] as string))
            {
                viewModel.Status = (string)TempData[StatusKey];
            }
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(LoginSessionKey)))
                return RedirectToAction("Index", "Admin");
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Registration(string login, string password, string email)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(LoginSessionKey)))
                return RedirectToAction("Index", "Admin");

            bool userExist = UserRepository.CheckIfUserExist(login);
            if (userExist)
            {
                TempData[StatusKey] = UserAlreadyExistStatus;
                return RedirectToAction("Registration", "User");
            }

            bool addingUser = UserRepository.AddUser(login, password, email);
            if (!addingUser)
            {
                TempData[StatusKey] = AddingUserFailedStatus;
                return RedirectToAction("Registration", "User");
            }

            TempData[StatusKey] = AddingUserOkStatus;
            return RedirectToAction("Registration", "User");
        }




        public IActionResult Logout()
        {
            HttpContext.Session.Remove(LoginSessionKey);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "User");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
