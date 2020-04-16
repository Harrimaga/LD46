using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{


    class Program
    {
        [System.Runtime.InteropServices.DllImport("nvapi64.dll", EntryPoint = "fake")]
        static extern int LoadNvApi64();

        [System.Runtime.InteropServices.DllImport("nvapi.dll", EntryPoint = "fake")]
        static extern int LoadNvApi32();

        private static void RemoveIntel()
        {
            try
            {
                if (Environment.Is64BitProcess)
                    LoadNvApi64();
                else
                    LoadNvApi32();
            }
            catch { } // will always fail since 'fake' entry point doesn't exists
        }

        static void Main(string[] args)
        {
            RemoveIntel();
            using(Window w = new Window(600, 500, "test"))
            {
                //w.VSync = OpenTK.VSyncMode.Off;
                w.Run();
            }
        }
    }
}
