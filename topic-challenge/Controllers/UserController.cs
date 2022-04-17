using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using topic_challenge.Models;
using topic_challenge.Services.Interfaces;
using topic_challenge.ViewModels;

namespace topic_challenge.Controllers
{
    [Route("/")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View(new UserViewModel());
        }

        [HttpGet("logout")]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return View("index", new UserViewModel());
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserViewModel user)
        {
            ModelState.Remove("ConfirmPassword");

            if (!ModelState.IsValid)
            {
                return View("Index", user);
            }

            User userDB = _userService.Login(user.Name, user.Password);
            if(userDB is not null)
            {
                user.Id = userDB.Id;
                user.Name = userDB.Name;

                SignIn(user);

                TopicListViewModel topicListViewModel = new TopicListViewModel();
                topicListViewModel.CurrentUser = user;

                return RedirectToAction("Index", "Topic", topicListViewModel);
            }

            user.Valid = false;

            return View("Index", user);
        }

        [HttpGet("form")]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userVielModel)
        {
            if (!ModelState.IsValid)
            {
                return View("form",  userVielModel);
            }

            User user = new User();
            user.Name = userVielModel.Name;
            user.Password = userVielModel.Password;            

            _userService.Create(user);
            return View("index", userVielModel);
        }

        private async void SignIn(UserViewModel user)
        {
            List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString())
                };
            var identity = new ClaimsIdentity(claims, "TopicCookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("TopicCookieAuth", claimsPrincipal);
        }
    }
}
