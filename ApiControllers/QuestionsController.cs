using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeOverFlowProject.ViewModels;
using CodeOverFlowProject.ServiceLayer;

namespace CodeOverFlowProject.ApiControllers
{
    public class QuestionsController : ApiController
    {
        IAnswerService asr;
        public QuestionsController(IAnswerService asr)
        {
            this.asr = asr;

        }
        public void Post(int AnswerID, int UserID, int Value)
        {
            this.asr.UpdateAnswerVotesCount(AnswerID, UserID, Value);
        }
    }
}
