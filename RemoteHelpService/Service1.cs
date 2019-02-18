using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RemoteHelpService
{
    public partial class Service1 : ServiceBase
    {
        WebServiceHost host;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var baseAddress = new Uri("http://localhost:8733/remote");  //remote 后面不能有/"
            host = new WebServiceHost(typeof(RemoteHelpWcf.Service1), baseAddress);
            host.Open();
        
           
        }

        protected override void OnStop()
        {
            host.Close();
        }
    }
}
