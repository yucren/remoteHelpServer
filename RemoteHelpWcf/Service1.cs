using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace RemoteHelpWcf
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class Service1 : IService1
    {
       
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        [WebGet(UriTemplate = "helpme",ResponseFormat =  WebMessageFormat.Xml)]
        public string Help()
        {
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                HttpRequestMessageProperty httpRequestMessageProperty = properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                var osVersion = GetOSVersion(httpRequestMessageProperty);
                if (osVersion == "Windows XP")
                {
                   return "你的电脑是XP系统,不支持远程协助,请用RTX远程";

                }
                var ip = endpoint.Address;
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo(@"msra.exe", "/offerra " + ip);
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = true;
                processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.StartInfo = processStartInfo;
                var success = process.Start();
                if (success)
                {
                   return "请接受任务栏上闪动图标的远程协助请求,或者弹出的远程协助窗口";
                }
                else
                {
                   return "失败,请与远程协助者联系";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
          
            
        }
        public static string GetOSVersion(HttpRequestMessageProperty request)
        {
            //UserAgent 
            var userAgent = request.Headers["User-Agent"];

            var osVersion = "未知";
            if (userAgent.Contains("NT 10.0"))
            {                osVersion = "Windows 10";
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
    }
}
