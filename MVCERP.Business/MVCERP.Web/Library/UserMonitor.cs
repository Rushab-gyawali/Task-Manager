using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCERP.Web.Library
{
    public class UserMonitor
    {
        private static UserMonitor Instance = new UserMonitor();
        private static readonly Dictionary<string, LoggedInUser> UserList = new Dictionary<string, LoggedInUser>();

        public static UserMonitor GetInstance()
        {
            if (Instance == null)
            {
                lock (UserList)
                {
                    if (Instance == null)
                        return Instance = new UserMonitor();
                }
            }
            return Instance;
        }

        public DbResponse MutipleRemoteLogin(LoggedInUser user)
        {
            var dbResult = new DbResponse();

            if (!IsUserExists(user.UserName))
                UserList.Add(user.UserName, user);
            else
            {
                UserMonitor pool = GetInstance();
                var loggedInUsers = pool.GetLoggedInUsers();

                lock (UserList)
                {
                    foreach (
                        KeyValuePair<string, LoggedInUser> loggedInUser in
                            loggedInUsers.ToList().Where(loggedInUser => loggedInUser.Value.UserName == user.UserName))
                    {
                        loggedInUser.Value.UserAccessLevel = "M";
                        loggedInUser.Value.SessionID = user.SessionID;
                        loggedInUser.Value.Browser = user.Browser;
                        loggedInUser.Value.LastLoginTime = user.LastLoginTime;
                        loggedInUser.Value.IPAddress = user.IPAddress;
                        loggedInUser.Value.Menu = user.Menu;
                    }
                }
            }
            dbResult.SetError(0, "Login Successful", user.UserName);
            return dbResult;
        }

        public DbResponse AddUser(LoggedInUser user)
        {
            var dbResult = new DbResponse();
            switch (user.UserAccessLevel)
            {
                case "M":
                    if (!IsUserExists(user.UserName))
                    {
                        lock (UserList)
                        {
                            UserList.Add(user.UserName, user);
                        }
                    }
                    else
                    {
                        UserMonitor pool = GetInstance();
                        var loggedInUsers = pool.GetLoggedInUsers();

                        lock (UserList)
                        {
                            foreach (
                                var loggedInUser in
                                    loggedInUsers.ToList().Where(
                                        loggedInUser => loggedInUser.Value.UserName == user.UserName))
                            {
                                loggedInUser.Value.UserAccessLevel = user.UserAccessLevel;
                                //loggedInUser.Value.DcInfo = user.DcInfo;
                                loggedInUser.Value.SessionID = user.SessionID;
                                loggedInUser.Value.Browser = user.Browser;
                                loggedInUser.Value.LoginTime = user.LoginTime;
                                loggedInUser.Value.LastLoginTime = user.LastLoginTime;
                                loggedInUser.Value.IPAddress = user.IPAddress;
                                loggedInUser.Value.Menu = user.Menu;
                            }
                        }
                    }
                    dbResult.SetError(0, "Login Successful", user.UserName);
                    break;
                case "S":
                    if (IsUserExists(user.UserName))
                        dbResult.SetError(1, "User Already Logged In", user.UserName);
                    else
                    {
                        lock (UserList)
                        {
                            UserList.Add(user.UserName, user);
                        }
                        dbResult.SetError(0, "Login Successful", user.UserName);
                    }
                    break;
            }
            return dbResult;
        }

        public void RemoveUser(string username)
        {
            if (IsUserExists(username))
            {
                lock (UserList)
                {
                    UserList.Remove(username);
                }
            }
        }

        public string GetUserBySessionId(string sessionId)
        {
            foreach (
                var loggedInUser in UserList.Values.ToList().Where(loggedInUser => loggedInUser.SessionID == sessionId))
            {
                return loggedInUser.UserName;
            }
            return "";
        }

        public bool IsUserExists(string username, string SessionID)
        {
            foreach (
                LoggedInUser loggedInUser in
                UserList.Values.Where(loggedInUser => loggedInUser.UserName == username && loggedInUser.SessionID == SessionID))
            {
                return true;
            }
            return false;
        }

        public bool IsUserExists(string username)
        {
            if (UserList.ContainsKey(username))
                return true;
            return false;
        }
        public bool HasRight(string username, string ControllerName, string Action)
        {
            //return true;
            foreach (
                LoggedInUser loggedInUser in
                UserList.Values.Where(loggedInUser => loggedInUser.UserName == username))
            {
                var returnVal = false;
                returnVal = loggedInUser.Menu.function.ToList().Where(a => a.FunctionId == Action).Count() > 0 ? true : false;
                //switch (Action.ToLower())
                //{
                //    case "v":
                //        returnVal = loggedInUser.Menu.ToList().Where(a => a.menuName == ControllerName && a.View > 0).Count() > 0 ? true : false;
                //        break;
                //    case "a":
                //        returnVal = loggedInUser.Menu.ToList().Where(a => a.menuName == ControllerName && a.AddEdit > 0).Count() > 0 ? true : false;
                //        break;
                //    case "d":
                //        returnVal = loggedInUser.Menu.ToList().Where(a => a.menuName == ControllerName && a.Delete > 0).Count() > 0 ? true : false;
                //        break;
                //}
                return returnVal;
            }
            return false;
        }
        public LoggedInUser GetUser(string username)
        {
            var user = new LoggedInUser();
            foreach (
                LoggedInUser loggedInUser in UserList.Values.Where(loggedInUser => loggedInUser.UserName == username))
            {
                return loggedInUser;
            }
            return user;
        }
        public IEnumerable<MenuCommon> GetMenus(string username)
        {
            foreach (
                 LoggedInUser loggedInUser in
                 UserList.Values.Where(loggedInUser => loggedInUser.UserName == username))
            {
                //return loggedInUser.Menu.ToList().GroupBy(x => x.menuName).Select(y => y.First()); ;
                return loggedInUser.Menu.menu.ToList();
            }
            return null;
        }

        public Dictionary<string, LoggedInUser> GetLoggedInUsers()
        {
            return UserList;
        }

    }
    public class LoggedInUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public DateTime LoginTime { get; set; }
        public string UserAccessLevel { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public int SessionTimeOutPeriod { get; set; }
        public string UserAgentName { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime LastActiveTime { get; set; }
        public string SessionID { get; set; }
        public UserMenuFunctions Menu { get; set; }
    }
}