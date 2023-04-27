using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsAPI
{
    public static class Extensions
    {
        public static string GetClass(this IntPtr window)
        {
            return WindowsAPI.Window.GetClass(window);
        }

        public static string GetTitle(this IntPtr window)
        {
            return WindowsAPI.Window.GetTitle(window);
        }

        public static void SetTitle(this IntPtr window, string title)
        {
            WindowsAPI.Window.SetTitle(window, title);
        }

        public static string GetCaption(this IntPtr window)
        {
            return WindowsAPI.Window.GetCaption(window);
        }


        public static void SetFocused(this IntPtr window)
        {
            WindowsAPI.Window.SetFocused(window);
        }


        public static void Maximize(this IntPtr window)
        {
            WindowsAPI.Window.Maximize(window);
        }


        public static void Minimize(this IntPtr window)
        {
            WindowsAPI.Window.SetFocused(window);
        }


        public static void Restore(this IntPtr window)
        {
            WindowsAPI.Window.Restore(window);
        }


        public static void Activate(this IntPtr window)
        {
            WindowsAPI.Window.Activate(window);
        }


        public static void Move(this IntPtr window, int x, int y)
        {
            WindowsAPI.Window.Move(window, x, y);
        }

        public static void Resize(this IntPtr window, int width, int height)
        {
            WindowsAPI.Window.Resize(window, width, height);
        }

        public static List<WinAPI.WindowStyles> GetStyles(this IntPtr window)
        {
            return WindowsAPI.Window.GetStyles(window);
        }
    }
}
