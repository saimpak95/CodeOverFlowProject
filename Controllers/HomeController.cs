using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeOverFlowProject.ServiceLayer;
using CodeOverFlowProject.ViewModels;

namespace CodeOverFlowProject.Controllers
{
    public class HomeController : Controller
    {
        IQuestionsService qs;
        ICategoriesService cs;
        public HomeController(IQuestionsService qs, ICategoriesService cs)
        {
            this.qs = qs;
            this.cs = cs;
        }
        [Route("Home")]
        public ActionResult Index()
        {
            List<QuestionViewModel> question = this.qs.GetQuestion().Take(10).ToList();
            return View(question);
        }

        [Route("About")]
        public ActionResult About()
        {
            return View();
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            return View();
        }

        [Route("AllCategories")]
        public ActionResult Categories()
        {
            List<CategoryViewModel> category = this.cs.GetCategories();
            return View(category);
        }

        
        public ActionResult Question()
        {
            List<QuestionViewModel> questions = this.qs.GetQuestion();
            return View(questions);
        }

        public ActionResult Search(string SearchValue)
        {
            List<QuestionViewModel> questions = this.qs.GetQuestion().Where(temp => temp.QuestionName.ToLower().Contains(SearchValue.ToLower()) ||
             temp.Category.CategoryName.ToLower().Contains(SearchValue.ToLower())
            ).ToList();
            ViewBag.SearchValue = SearchValue;
            return View(questions);
        }
    }
}