using System.Web.Mvc;
using MVCERP.Business.Business.Common;
using MVCERP.Business.Business.Role;
using MVCERP.Business.Business.User;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using MVCERP.Web.Services.UserService;
using MVCERP.Business.Business.Reports;
using MVCERP.Business.Business.Error;
using MVCERP.Business.Business.TaskReporting;
using MVCERP.Repository.Repository.TaskReporting;
using MVCERP.Repository.Repository.Member;
using MVCERP.Business.Business.Member;

namespace MVCERP.Web
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserBusiness, UserBusiness>();
            container.RegisterType<IRoleBusiness, RoleBusiness>();
            container.RegisterType<ICommonBuss, CommonBuss>();
            container.RegisterType<IReportBusiness, ReportBusiness>();
            container.RegisterType<IErrorBusiness, ErrorBusiness>();
            container.RegisterType<IMISReportComponentBusiness, MISReportComponentBusiness>();
            container.RegisterType<ITaskReportingBusiness, TaskReportingBusiness>();
            container.RegisterType<ITaskReportingRepository, TaskReportingRepository>();
            container.RegisterType<IMemberRepository, MemberRepository>();
            container.RegisterType<IMemberBusiness, MemberBusiness>();
            return container;
        }
    }
}