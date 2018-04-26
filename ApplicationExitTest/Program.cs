using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ApplicationExitTest
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);
        static void Main(string[] args)
        {
            var cur = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
            var parent = cur.Parent.Parent.Parent.FullName.ToString();
            var dir = System.IO.Path.Combine(parent, "TestingFormApp", "bin", "Debug", "TestingFormApp.exe");
            Console.WriteLine(dir);
            var pi = new ProcessStartInfo(dir);
            var p = System.Diagnostics.Process.Start(pi);
            var h = p.Handle;
            if (h == IntPtr.Zero) {
                p.Kill();
                return;
            }
            Console.ReadKey();
            Console.WriteLine(p.MainWindowTitle);
            SendMessage(p.MainWindowHandle, 0x0010, 0,0);
            Console.ReadKey();
            Console.WriteLine("now killing process.");
            p.Kill();
            Console.ReadKey();
        }
    }
}
