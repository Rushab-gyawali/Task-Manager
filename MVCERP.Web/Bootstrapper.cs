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
using MVCERP.Business.Business.TaskManager;
using MVCERP.Repository.Repository.TaskManager;
using MVCERP.Business.Business.Member;
using MVCERP.Repository.Repository.Member;
<<<<<<< HEAD
using MVCERP.Business.Business.SprintBusiness;
using MVCERP.Repository.Repository.Sprint;
=======
using MVCERP.Business.Business.TaskReport;
using MVCERP.Business.Business.BackLog;
using MVCERP.Repository.Repository.BackLog;
using MVCERP.Business.Business.Roles;
using MVCERP.Repository.Repository.Roles;
using MVCERP.Business.Business.Permission;
using MVCERP.Repository.Repository.Permission;
>>>>>>> d3ff3af5091d257b06876227cd2975ea7733c9a5

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
            container.RegisterType<ITaskManagerBusiness, TaskManagerBusiness>();
            container.RegisterType<ITaskManagerRepository, TaskManagerRepository>();
            container.RegisterType<IMemberRepository, MemberRepository>();
            container.RegisterType<IMemberBusiness, MemberBusiness>();
<<<<<<< HEAD
            container.RegisterType<ISprintBusiness, SprintBusiness>();
            container.RegisterType<ISprintRepository, SprintRepository>();
=======
            container.RegisterType<ITaskReportBusiness, TaskReportBusiness>();
            container.RegisterType<IBackLogBusiness, BackLogBusiness>();
            container.RegisterType<IBackLogRepository, BackLogRepository>();
            container.RegisterType<IRolesBusiness, RolesBusiness>();
            container.RegisterType<IRolesRepository, RolesRepository > ();
            container.RegisterType<IPermissionBusiness, PermissionBusiness>();
            container.RegisterType<IPermissionRepository, PermissionRepository>();

>>>>>>> d3ff3af5091d257b06876227cd2975ea7733c9a5

            return container;
        }
    }
}