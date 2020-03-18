using CodeOverFlowProject.ServiceLayer;
using CodeOverFlowProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeOverFlowProject.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionsService qs;
        ICategoriesService cs;
        IAnswerService ass;
        public QuestionsController(IQuestionsService qs, ICategoriesService cs, IAnswerService ass)
        {
            this.qs = qs;
            this.cs = cs;
            this.ass = ass;
        }
        public ActionResult View(int id)
        {
            this.qs.UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm = this.qs.GetQuestionByID(id, uid);
            return View(qvm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddAnswer(NewAnswerViewModel navm)
        {
            navm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            navm.AnswerDateAndTime = DateTime.Now;
            navm.VotesCount = 0;
            if (ModelState.IsValid)
            {
                this.ass.InsertAnswer(navm);
                return RedirectToAction("View", "Questions", new { id = navm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid");
                QuestionViewModel qvm = qs.GetQuestionByID(navm.QuestionID, navm.UserID);
                return View("View",qvm);
            }
           
        }
    }
}