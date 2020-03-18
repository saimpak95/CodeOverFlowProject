using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeOverFlowProject.ServiceLayer;
using CodeOverFlowProject.ViewModels;

namespace CodeOverFlowProject.ApiControllers
{
    public class AccountController : ApiController
    {
        IUsersService us;
        public AccountController(IUsersService us)
        {
            this.us = us;
        }

        public string Get(string Email)
        {
           
            if (this.us.GetUserByEmail(Email) != null)
            {
                return "Found";
            }
            else
            {
                return "Not Found";
            }
        }
    }
}
