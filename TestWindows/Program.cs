using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace TestWindows
{
    public static class Program
    {
        static PerformanceCounter cpuCounter;
        static PerformanceCounter ramCounter;
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /// 
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*try
            {*/
                Application.Run(new testwindows());
            /*}
            catch(Exception e)
            {

                cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                ramCounter = new PerformanceCounter("Memory", "Available MBytes");
                File.WriteAllText("log.log", "Ram Used: " + getAvailableRAM() +
                    "\nCPU Used: " + getCurrentCpuUsage() +
                    "\nError: " + e.Message +
                    "\nTrace: " + e.StackTrace);
            }*/
        }

        public static string getCurrentCpuUsage()
        {
            return cpuCounter.NextValue() + "%";
        }

        public static string getAvailableRAM()
        {
            return ramCounter.NextValue() + "MB";
        }
    }
}
