using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsAPI
{
    /// <summary>
    /// Windows API wrapper class for window related functions.
    /// </summary>
    public static class WindowSearch
    {
        /// <summary>
        /// Find and return a handle on the first window with the specified title.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <returns>The handle to the window.</returns>
        public static IntPtr GetByTitle(string windowTitle)
        {
            IntPtr hWnd = WinAPI.FindWindow(null, windowTitle);
            if (hWnd == IntPtr.Zero)
                throw new Exception("Window not found.");
            return hWnd;
        }

        /// <summary>
        /// Get the handle of the window that is currently in focus.
        /// </summary>
        /// <returns>The handle to the focused Window.</returns>
        public static IntPtr GetFocused()
        {
            return WinAPI.GetForegroundWindow();
        }

        public static IntPtr GetForegroundWindow()
        {
            return WinAPI.GetForegroundWindow();            
        }

        /// <summary>
        /// Callback method to be used when enumerating windows.
        /// </summary>
        /// <param name="handle">Handle of the next window</param>
        /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
        /// <returns>True to continue the enumeration, false to bail</returns>
        private static bool EnumWindow(IntPtr handle, int pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(new IntPtr(pointer));
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        /// <summary>
        /// Delegate for the EnumChildWindows method
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
        /// <returns>True to continue enumerating, false to bail.</returns>
        public delegate bool EnumWindowsProc(IntPtr hWnd, int parameter);

        public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        public static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId)
        {
            var handles = new List<IntPtr>();

            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
                WinAPI.EnumThreadWindows(thread.Id, (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);

            return handles;
        }


        /// <summary>
        /// Returns a list of child windows
        /// </summary>
        /// <param name="parent">Parent of the windows to return</param>
        /// <returns>List of child windows</returns>
        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);

            try
            {
                EnumWindowsProc childProc = new EnumWindowsProc(EnumWindow);
                WinAPI.EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result.Distinct().ToList();
        }

        public static List<IntPtr> GetDirectChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();

            var child = WinAPI.GetWindow(parent, WinAPI.GW_CHILD);

            while (child != IntPtr.Zero)
            {
                result.Add(child);

                child = WinAPI.GetWindow(child, WinAPI.GW_HWNDNEXT);
            }

            return result;
        }

        public static List<IntPtr> GetAllWindowsByProcess(string processName, bool onlyVisible)
        {
            List<IntPtr> result = new List<IntPtr>();

            Process process = null;

            Process[] p = Process.GetProcessesByName(processName);
            if (p.Count() > 0)
            {
                process = p[0];
            }
            else return result;

            IntPtr win = process.MainWindowHandle;

            result.Add(win);

            result.AddRange(GetChildWindows(win));

            List<IntPtr> additional = new List<IntPtr>();
            foreach (IntPtr w in result) if (w != win) additional.AddRange(GetChildWindows(w));

            result.AddRange(additional);

            result = result.Distinct().ToList();

            for (int i = 0; i < result.Count; i++)
            {
                if ((onlyVisible && !WinAPI.IsWindowVisible(result[i])) || (GetProcessId(result[i]) != process.Id))
                {
                    result.RemoveAt(i);
                    i--;
                }
            }

            return result;
        }

        public static List<IntPtr> GetAllThreadWindows(string processName, bool onlyVisible)
        {
            List<IntPtr> result = new List<IntPtr>();

            Process process = null;

            Process[] p = Process.GetProcessesByName(processName);
            if (p.Count() > 0)
            {
                process = p[0];
            }
            else return result;

            result.AddRange(EnumerateProcessWindowHandles(process.Id));

            for (int i = 0; i < result.Count; i++)
            {
                if (onlyVisible && !WinAPI.IsWindowVisible(result[i]))
                {
                    result.RemoveAt(i);
                    i--;
                }
            }

            return result;
        }


        public static int GetProcessId(IntPtr win)
        {
            int id = 0;

            WinAPI.GetWindowThreadProcessId(win, ref id);

            return id;
        }

        private static void ParseNode(string node, out string name, out string caption, out int index)
        {
            name = null;
            caption = null;
            index = 0;

            string[] split = node.Split('&');
            foreach (string s in split)
            {
                string[] argVal = s.Split('=');
                switch (argVal[0])
                {
                    case "n":
                        name = argVal[1];
                        break;
                    case "c":
                        caption = argVal[1];
                        break;
                }
            }

            split = node.Split('[');
            if (split.Length > 1) index = Convert.ToInt32(split[1].Replace("]", ""));
        }


        public static IntPtr GetWindowByPath(string path)
        {
            IntPtr win = IntPtr.Zero;

            List<string> nodes = path.Split('/').ToList();

            foreach (string node in nodes)
            {
                string name, caption;
                int index;

                ParseNode(node, out name, out caption, out index);

                IntPtr childAfter = IntPtr.Zero;
                IntPtr curWin = IntPtr.Zero;

                for (int i = 0; i <= index; i++)
                {
                    curWin = WinAPI.FindWindowEx(win, childAfter, name, caption);
                    if (curWin == IntPtr.Zero) return curWin;
                    childAfter = curWin;
                }

                win = curWin;
            }

            return win;
        }
  
        public static List<IntPtr> GetTreeViewItemChildItems(IntPtr treeView, IntPtr item)
        {
            List<IntPtr> children = new List<IntPtr>();

            if (item == IntPtr.Zero) return children;

            children.Add(item);

            var current = WinAPI.SendMessage(treeView, WinAPI.TVM_GETNEXTITEM, WinAPI.TVGN_CHILD, item);

            while (current != IntPtr.Zero)
            {
                children.AddRange(GetTreeViewItemChildItems(treeView, current));

                current = WinAPI.SendMessage(treeView, WinAPI.TVM_GETNEXTITEM, WinAPI.TVGN_NEXT, current);

                if (current == IntPtr.Zero) break;
            }

            return children;
        }

        public static List<IntPtr> GetAllTreeViewItems(IntPtr treeView)
        {
            var item = WinAPI.SendMessage(treeView, WinAPI.TVM_GETNEXTITEM, WinAPI.TVGN_ROOT, IntPtr.Zero);

            List<IntPtr> result = new List<IntPtr>();

            while (item != IntPtr.Zero)
            {
                result.AddRange(GetTreeViewItemChildItems(treeView, item));
                item = WinAPI.SendMessage(treeView, WinAPI.TVM_GETNEXTITEM, WinAPI.TVGN_NEXT, item);
            }

            return result;
        }

        public static IntPtr GetChildByPath(IntPtr parent, params int[] path)
        {
            if (parent == IntPtr.Zero) return IntPtr.Zero;

            var current = parent;

            foreach (var i in path)
            {
                var children = GetDirectChildWindows(current);

                if (children.Count <= i) return IntPtr.Zero;

                current = children[i];
            }

            return current;
        }

        public static string GetWindowClass(IntPtr win)
        {
            if (win == IntPtr.Zero) return "";

            StringBuilder title = new StringBuilder(512);
            WinAPI.RealGetWindowClass(win, title, 512);

            return title.ToString().Trim();
        }

        public static IntPtr GetWindowByClass(IntPtr parent, string className)
        {
            string c = GetWindowClass(parent);

            if (c == className) return parent;

            foreach (var w in GetChildWindows(parent))
            {
                var found = GetWindowByClass(w, className);

                if (found != IntPtr.Zero) return found;
            }

            return IntPtr.Zero;
        }

        public static string GetWindowText(IntPtr hWnd)
        {
            StringBuilder buff = new StringBuilder(256);
            WinAPI.GetWindowText(hWnd, buff, 256);

            return buff.ToString();
        }

        public static IntPtr GetWindowsByTitle(string processName, string windowText, bool exact, bool onlyVisible)
        {
            List<IntPtr> wins = GetAllWindowsByProcess(processName, onlyVisible);

            foreach (IntPtr w in wins)
            {
                string text = GetWindowText(w);
                if ((exact && text == windowText) || (!exact && text.Contains(windowText))) return w;
            }

            return IntPtr.Zero;
        }

        public static IntPtr GetThreadWindowsByText(string processName, string windowText, bool exact, bool onlyVisible)
        {
            List<IntPtr> wins = GetAllThreadWindows(processName, onlyVisible);

            foreach (IntPtr w in wins)
            {
                string text = GetWindowText(w);
                if ((exact && text == windowText) || (!exact && text.Contains(windowText))) return w;
            }

            return IntPtr.Zero;
        }

        public static IntPtr GetThreadWindowsByText(string processName, string windowText, bool exact)
        {
            return GetThreadWindowsByText(processName, windowText, exact, true);
        }

        public static List<IntPtr> GetListViewItems(IntPtr listView)
        {
            var header = WinAPI.SendMessage(listView, (int)WinAPI.LVM.GETHEADER, 0, IntPtr.Zero);
            return new List<IntPtr>();
        }

        public static IntPtr GetChildWindowByText(IntPtr parent, string windowText, bool exact)
        {
            var wins = WindowSearch.GetChildWindows(parent);

            foreach (IntPtr w in wins)
            {
                string text = GetWindowText(w);
                if ((exact && text == windowText) || (!exact && text.Contains(windowText))) return w;
            }

            return IntPtr.Zero;
        }

        public static string GetWindowCaption(IntPtr hWnd)
        {
            StringBuilder buff = new StringBuilder(256);
            WinAPI.SendMessage(hWnd, WinAPI.WM_GETTEXT, 256, buff);
            return buff.ToString();
        }

        public static IntPtr GetWindowByCaption(string processName, string windowCaption, bool exact)
        {
            List<IntPtr> wins = GetAllWindowsByProcess(processName, true);

            foreach (IntPtr w in wins)
            {
                string text = GetWindowCaption(w);
                if ((exact && text == windowCaption) || (!exact && text.Contains(windowCaption))) return w;
            }

            return IntPtr.Zero;
        }

        public static List<IntPtr> GetAllWindowsByTitle(string processName, string windowText, bool exact)
        {
            List<IntPtr> result = new List<IntPtr>();

            List<IntPtr> wins = GetAllWindowsByProcess(processName, true);

            foreach (IntPtr w in wins)
            {
                string text = GetWindowText(w);
                if ((exact && text == windowText) || (!exact && text.Contains(windowText))) result.Add(w);
            }

            return result;
        }

        public static List<IntPtr> GetAllWindowsByCaption(string processName, string windowCaption, bool exact)
        {
            List<IntPtr> result = new List<IntPtr>();

            List<IntPtr> wins = GetAllWindowsByProcess(processName, true);

            foreach (IntPtr w in wins)
            {
                string text = GetWindowCaption(w);
                if ((exact && text == windowCaption) || (!exact && text.Contains(windowCaption))) result.Add(w);
            }

            return result;
        }
    }
}