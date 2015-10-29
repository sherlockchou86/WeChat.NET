using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WeChat.NET
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());


            //WeChat.NET.HTTP.BaseService bs = new HTTP.BaseService();
            //bs.SendGetRequest("");

            //bs.SendGetRequest("http://www.jd.com/");

            //Console.Read();
        }
    }
}
