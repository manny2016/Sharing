using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sharing.Core;
using Sharing.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sharing.Agent.Delivery
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {			
			Application.ThreadException += Application_ThreadException;
			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

		private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e){
			
			MessageBox.Show("错误提示",e.Exception.Message);
		}
	}
}
