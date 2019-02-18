using RemoteHelpWcf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace remoteServer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            var baseAddress = new Uri("http://localhost:8080/remote");  //remote 后面不能有/"
            var host = new WebServiceHost(typeof(Service1), baseAddress);
            host.Open();
            Console.ReadLine();
            host.Close();
            //Process process = new Process();
            //ProcessStartInfo startInfo = new ProcessStartInfo();

            //startInfo.FileName = @"./remoteServers.exe";
            //startInfo.UseShellExecute = false;
            //startInfo.CreateNoWindow = true;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //process.StartInfo = startInfo;
            //process.Start();         



        }
    }
}
