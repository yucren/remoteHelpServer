using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RemoteHelp
{
    public partial class help : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Process process = new Process();
            //try
            //{
            //    var clientIp = Request.UserHostAddress;
            //    var clientHostName = Request.UserHostName;
            //    process.StartInfo.UseShellExecute = false;   //是否使用操作系统shell启动 
            //    process.StartInfo.CreateNoWindow = true;   //是否在新窗口中启动该进程的值 (不显示程序窗口)
            //    process.StartInfo.RedirectStandardInput = true;  // 接受来自调用程序的输入信息 
            //    process.StartInfo.RedirectStandardOutput = true;  // 由调用程序获取输出信息
            //    process.StartInfo.RedirectStandardError = true;  //重定向标准错误输出
            //    process.StartInfo.FileName = "cmd.exe";
            //    process.Start();                         // 启动程序
            //    process.StandardInput.WriteLine("msra /offerra " + clientIp); //向cmd窗口发送输入信息
            //    process.StandardInput.AutoFlush = true;
            //}
            //catch (Exception ex)
            //{

            //}
            var version = GetOSVersion(Request);
            if (version == "Windows XP")
            {
                Response.Write("你的电脑是XP系统,不支持远程协助,请用RTX远程");

            }
            else
            {
                var clientIp = Request.UserHostAddress;
                var clientHostName = Request.UserHostName;
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo(@"msra.exe", "/offerra " + clientIp);
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = true;
                processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.StartInfo = processStartInfo;
                var success = process.Start();
                if (success)
                {
                    Response.Write("请接受任务栏上闪动图标的远程控制请求");
                }
                else
                {
                    Response.Write("请失败,请与RTX9010联系");
                }
            }

            
           

            
        }
        #region 获取操作系统版本号

        /// <summary> 
        /// 获取操作系统版本号 
        /// </summary> 
        /// <returns></returns>

        public static string GetOSVersion(HttpRequest request)
        {
            //UserAgent 
            var userAgent = request.ServerVariables["HTTP_USER_AGENT"];

            var osVersion = "未知";
            if (userAgent.Contains("NT 10.0"))
            {
                osVersion = "Windows 10";
            }
            else if (userAgent.Contains("NT 6.3"))
            {
                osVersion = "Windows 8.1";
            }
            else if (userAgent.Contains("NT 6.2"))
            {
                osVersion = "Windows 8";
            }

            else if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows 7";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            return osVersion;
        }
        #endregion
    }
}