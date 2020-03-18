using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using CodeOverFlowProject.ServiceLayer;
using System.Web.Mvc;

namespace CodeOverFlowProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IQuestionsService, QuestionsService>();
            container.RegisterType<IUsersService,UsersService>();
            container.RegisterType<IAnswerService, AnswersService>();
            container.RegisterType<ICategoriesService, CategoriesService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container)); //Dependency Injection for MVC

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);  //Dependency Injection for WEB API
           
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}