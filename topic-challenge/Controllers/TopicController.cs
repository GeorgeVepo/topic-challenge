using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using topic_challenge.Models;
using topic_challenge.Services.Interfaces;
using topic_challenge.ViewModels;

namespace topic_challenge.Controllers
{
    [Route("topic/")]
    public class TopicController : Controller
    {
        private ITopicService _topicService;
        private IUserService _userService;
        public TopicController(ITopicService topicService, IUserService userService)
        {
            _topicService = topicService;
            _userService = userService;
        }

        public async Task<ActionResult> Index(TopicListViewModel topicListViewModel)
        {
            if (!CheckUserIsAuthenticated())
            {
                return RedirectToAction("Index", "User");
            }
          
            return View(GetTopicListViewModel());
        }

        [HttpGet("create/")]
        public ActionResult FormCreate()
        {
            if (!CheckUserIsAuthenticated())
            {
                return RedirectToAction("Index", "User");
            }

            TopicViewModel topicViewModel = new TopicViewModel();
            topicViewModel.User = new UserViewModel();
            topicViewModel.User.Id = GetLoggedUserID();
            return View("Create", topicViewModel);
        }
        
        [HttpGet("edit/")]
        public ActionResult FormEdit(TopicViewModel topicViewModel)
        {
            if (!CheckUserIsAuthenticated())
            {
                return RedirectToAction("Index", "User");
            }

            return View("Edit", topicViewModel);
        }

        [HttpPost("create/")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TopicViewModel topicViewModel)
        {
            RemoveUserValidation();
            if (!ModelState.IsValid)
            {
                return View("create", topicViewModel);
            }

            if (!CheckUserIsAuthenticated())
            {
                return RedirectToAction("Index", "User");
            }

            topicViewModel.CreationDate = DateTime.Now;
            _topicService.Create(GetTopicModel(topicViewModel));
            return View("index", GetTopicListViewModel());
        }

        [HttpPost("edit/")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TopicViewModel topicViewModel)
        {
            RemoveUserValidation();
            if (!ModelState.IsValid)
            {
                return View("edit", topicViewModel);
            }

            if (!CheckUserIsAuthenticated())
            {
                return RedirectToAction("Index", "User");
            }

            _topicService.Update(GetTopicModel(topicViewModel));
            return View("index", GetTopicListViewModel());
        }

        private List<TopicViewModel> GetTopicViewModels(List<Topic> topics)
        {
            List<TopicViewModel> topicViewList = new List<TopicViewModel>();
            TopicViewModel topicView = new TopicViewModel();
            topics.ForEach(topic =>
            {
                topicView = new TopicViewModel();
                topicView.Id = topic.Id;
                topicView.Title = topic.Title;
                topicView.Description = topic.Description;
                topicView.CreationDate = topic.CreationDate;
                topicView.User = new UserViewModel()
                {
                    Id = topic.User.Id,
                    Name = topic.User.Name,
                    Password = topic.User.Password,
                };
                topicViewList.Add(topicView);

            });
            return topicViewList;
        }

        private void RemoveUserValidation()
        {
            ModelState.Remove("User.Name");
            ModelState.Remove("User.Password");
            ModelState.Remove("User.ConfirmPassword");
        }

        private TopicListViewModel GetTopicListViewModel()
        {
            TopicListViewModel topicListViewModel = new TopicListViewModel();
            topicListViewModel.Topics = this.GetTopicViewModels(_topicService.GetAll());
            topicListViewModel.CurrentUser = new UserViewModel();
            topicListViewModel.CurrentUser.Id = GetLoggedUserID();
            topicListViewModel.CurrentUser.Name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
            return topicListViewModel;
        }

        private Topic GetTopicModel(TopicViewModel topicViewModel)
        {
            return new Topic()
            {
                Id = topicViewModel.Id,
                Title = topicViewModel.Title,
                Description = topicViewModel.Description,
                CreationDate = topicViewModel.CreationDate,
                User = _userService.Find(GetLoggedUserID())
            };

        }

        private int GetLoggedUserID()
        {
            return int.Parse(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);
        }

        private bool CheckUserIsAuthenticated()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            return true;
        }
    }
}
