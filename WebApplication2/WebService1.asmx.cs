using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace WebApplication2
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod(Description = "验证登录操作", EnableSession = true, MessageName = "Login")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Result Login(string userName, string pwd)
        {
            Result rc = null;

            try
            {
                if (userName == "1" && pwd == "1")
                {
                    Session["User"] = new UserInfo { UserName = userName, pwd = pwd };
                    rc = Result.ToResult("true", "登录成功");
                }
                else rc = Result.ToResult("false", "登录失败");
            }
            catch (Exception ex)
            {
                // 可以在此保存日志
                rc = Result.ToResult("false", ex.Message);
            }

            return rc;
        }

        [WebMethod(Description = "验证登录", EnableSession = true, MessageName = "IsLogin")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Result IsLogin()
        {
            Result rc = null;

            try
            {
                rc = Session["User"] != null ? Result.ToResult("true", "已经登录") : Result.ToResult("false", "暂未登录");
            }
            catch (Exception ex)
            {
                rc = Result.ToResult("false", ex.Message);
            }

            return rc;
        }

        public class Result
        {
            public string Code { get; set; }
            public string Message { get; set; }

            public static Result ToResult(string code, string message)
            {
                return new Result { Code = code, Message = message };
            }
        }

        private class UserInfo
        {
            public string UserName { get; set; }
            public string pwd { get; set; }
        }
    }
}
