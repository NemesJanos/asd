using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeKi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static bool systemTrayStart = false;

        public static bool SystemTrayStart { get => systemTrayStart; set => systemTrayStart = value; }
        static Mutex mutex = new Mutex(true, "{65602011-8706-42cb-aabe-e66f9db8e2be}"); //http://sanity-free.org/143/csharp_dotnet_single_instance_application.html
        [STAThread]
        static void Main(string[] args)
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (args.Length > 0 && args[0] == "systray")
                {
                    systemTrayStart = true;
                }
                Application.Run(new BeKi());
            }
            else
            {
                UzenetAblak.Ablak("Egyszerre csak egy példány futhat a szoftverből.\nEllenőrizze az ikonokat a tálcaóra mellett!", "Már fut a szoftver", Szint.Info);
            }
        }
    }
}
//https://stackoverflow.com/questions/1617784/how-to-start-the-application-directly-in-system-tray-net-c