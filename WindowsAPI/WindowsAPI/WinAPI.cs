using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using static WindowsAPI.Structs;
using static WindowsAPI.WindowSearch;

namespace WindowsAPI
{
    /// <summary>
    /// Windows API methods. All the other classes are wrappers for these methods.
    /// </summary>
    public static class WinAPI
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindowEx(IntPtr parentHandle, int childAfter, string className, int windowTitle);

        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        internal static extern bool GetWindowRect(IntPtr hWnd, out Structs.Rect lpRect);

        [DllImport("user32.dll")]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindow(String sClassName, String sAppName);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cX, int cY, int wFlags);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetWindowText(IntPtr hWnd, String lpString);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int value);

        [DllImport("user32.dll")]
        internal static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetMenu(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        internal static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool EndTask(IntPtr hWnd, bool fShutDown, bool fForce);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        internal static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        internal static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint SendInput(uint nInputs, ref Structs.INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        internal static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumChildWindows(IntPtr window, EnumWindowsProc callback, IntPtr i);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr win);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, ref int processId);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        internal static extern uint RealGetWindowClass(IntPtr win, StringBuilder pszType, uint cchType);

        [DllImport("user32.dll")]
        internal static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        internal static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        internal static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern void keybd_event(int bVk, byte bScan, UInt32 dwFlags, int dwExtraInfo);


        internal const int GW_HWNDNEXT = 2;
        internal const int GW_CHILD = 5;
        internal const int TV_FIRST = 0x1100;
        internal const int TVGN_ROOT = 0x0;
        internal const int TVGN_NEXT = 0x1;
        internal const int TVGN_CHILD = 0x4;
        internal const int TVGN_FIRSTVISIBLE = 0x5;
        internal const int TVGN_NEXTVISIBLE = 0x6;
        internal const int TVGN_CARET = 0x9;
        internal const int TVM_SELECTITEM = (TV_FIRST + 11);
        internal const int TVM_GETNEXTITEM = (TV_FIRST + 10);
        internal const int TVM_GETITEM = (TV_FIRST + 12);
        internal const int TVM_EXPAND = (TV_FIRST + 2);
        internal const int TVE_COLLAPSE = 0x0001;
        internal const int TVE_EXPAND = 0x0002;
        internal const int TVE_COLLAPSERESET = 0x8000;
        internal const int LVM_GETNEXTITEM = 0x100c;
        internal const int LVNI_BELOW = 0x0200;

        internal const int WM_GETTEXT = 13;
        internal const int GCL_HICONSM = -34;
        internal const int GCL_HICON = -14;
        internal const int ICON_SMALL = 0;
        internal const int ICON_BIG = 1;
        internal const int ICON_SMALL2 = 2;
        internal const int WM_GETICON = 0x7F;
        internal const int GWL_STYLE = -16;


        internal const int defaultMaxWaitSec = 2;
        internal const int defaultSleep = 10;
        
        internal const int WM_SYSCOMMAND = 0x0112;
        internal const int SC_MINIMIZE = 0xF020;
        internal const int SC_MAXIMIZE = 0xF030;
        internal const int SC_CLOSE = 0xF060;
        internal const int SC_RESTORE = 0xF120;
        internal const int WM_ACTIVATEAPP = 0x001C;
        internal const int WM_USER = 0x0400;
        internal const int WM_LBUTTONDBLCLK = 0x0203;
        internal const int WM_LBUTTONDOWN = 0x0201;
        internal const int WM_LBUTTONUP = 0x0202;
        internal const int WM_ACTIVATE = 0x0006;
        internal const int WA_CLICKACTIVE = 2;
        
        [Flags]
        public enum WindowStyles : uint
        {
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = 0x80000000,
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,

            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000
        }

        // Enumeration is set to unicode, ANSI counterparts are commented out.
        // Contains a few undocumented messages of which the name was invented.
        internal enum LVM
        {
            FIRST = 0x1000,
            SETUNICODEFORMAT = 0x2005,        // CCM_SETUNICODEFORMAT,
            GETUNICODEFORMAT = 0x2006,        // CCM_GETUNICODEFORMAT,
            GETBKCOLOR = (FIRST + 0),
            SETBKCOLOR = (FIRST + 1),
            GETIMAGELIST = (FIRST + 2),
            SETIMAGELIST = (FIRST + 3),
            GETITEMCOUNT = (FIRST + 4),
            GETITEMA = (FIRST + 5),
            GETITEMW = (FIRST + 75),
            GETITEM = GETITEMW,
            //GETITEM                = GETITEMA,
            SETITEMA = (FIRST + 6),
            SETITEMW = (FIRST + 76),
            SETITEM = SETITEMW,
            //SETITEM                = SETITEMA,
            INSERTITEMA = (FIRST + 7),
            INSERTITEMW = (FIRST + 77),
            INSERTITEM = INSERTITEMW,
            //INSERTITEM             = INSERTITEMA,
            DELETEITEM = (FIRST + 8),
            DELETEALLITEMS = (FIRST + 9),
            GETCALLBACKMASK = (FIRST + 10),
            SETCALLBACKMASK = (FIRST + 11),
            GETNEXTITEM = (FIRST + 12),
            FINDITEMA = (FIRST + 13),
            FINDITEMW = (FIRST + 83),
            GETITEMRECT = (FIRST + 14),
            SETITEMPOSITION = (FIRST + 15),
            GETITEMPOSITION = (FIRST + 16),
            GETSTRINGWIDTHA = (FIRST + 17),
            GETSTRINGWIDTHW = (FIRST + 87),
            HITTEST = (FIRST + 18),
            ENSUREVISIBLE = (FIRST + 19),
            SCROLL = (FIRST + 20),
            REDRAWITEMS = (FIRST + 21),
            ARRANGE = (FIRST + 22),
            EDITLABELA = (FIRST + 23),
            EDITLABELW = (FIRST + 118),
            EDITLABEL = EDITLABELW,
            //EDITLABEL              = EDITLABELA,
            GETEDITCONTROL = (FIRST + 24),
            GETCOLUMNA = (FIRST + 25),
            GETCOLUMNW = (FIRST + 95),
            SETCOLUMNA = (FIRST + 26),
            SETCOLUMNW = (FIRST + 96),
            INSERTCOLUMNA = (FIRST + 27),
            INSERTCOLUMNW = (FIRST + 97),
            DELETECOLUMN = (FIRST + 28),
            GETCOLUMNWIDTH = (FIRST + 29),
            SETCOLUMNWIDTH = (FIRST + 30),
            GETHEADER = (FIRST + 31),
            CREATEDRAGIMAGE = (FIRST + 33),
            GETVIEWRECT = (FIRST + 34),
            GETTEXTCOLOR = (FIRST + 35),
            SETTEXTCOLOR = (FIRST + 36),
            GETTEXTBKCOLOR = (FIRST + 37),
            SETTEXTBKCOLOR = (FIRST + 38),
            GETTOPINDEX = (FIRST + 39),
            GETCOUNTPERPAGE = (FIRST + 40),
            GETORIGIN = (FIRST + 41),
            UPDATE = (FIRST + 42),
            SETITEMSTATE = (FIRST + 43),
            GETITEMSTATE = (FIRST + 44),
            GETITEMTEXTA = (FIRST + 45),
            GETITEMTEXTW = (FIRST + 115),
            SETITEMTEXTA = (FIRST + 46),
            SETITEMTEXTW = (FIRST + 116),
            SETITEMCOUNT = (FIRST + 47),
            SORTITEMS = (FIRST + 48),
            SETITEMPOSITION32 = (FIRST + 49),
            GETSELECTEDCOUNT = (FIRST + 50),
            GETITEMSPACING = (FIRST + 51),
            GETISEARCHSTRINGA = (FIRST + 52),
            GETISEARCHSTRINGW = (FIRST + 117),
            GETISEARCHSTRING = GETISEARCHSTRINGW,
            //GETISEARCHSTRING       = GETISEARCHSTRINGA,
            SETICONSPACING = (FIRST + 53),
            SETEXTENDEDLISTVIEWSTYLE = (FIRST + 54),            // optional wParam == mask
            GETEXTENDEDLISTVIEWSTYLE = (FIRST + 55),
            GETSUBITEMRECT = (FIRST + 56),
            SUBITEMHITTEST = (FIRST + 57),
            SETCOLUMNORDERARRAY = (FIRST + 58),
            GETCOLUMNORDERARRAY = (FIRST + 59),
            SETHOTITEM = (FIRST + 60),
            GETHOTITEM = (FIRST + 61),
            SETHOTCURSOR = (FIRST + 62),
            GETHOTCURSOR = (FIRST + 63),
            APPROXIMATEVIEWRECT = (FIRST + 64),
            SETWORKAREAS = (FIRST + 65),
            GETWORKAREAS = (FIRST + 70),
            GETNUMBEROFWORKAREAS = (FIRST + 73),
            GETSELECTIONMARK = (FIRST + 66),
            SETSELECTIONMARK = (FIRST + 67),
            SETHOVERTIME = (FIRST + 71),
            GETHOVERTIME = (FIRST + 72),
            SETTOOLTIPS = (FIRST + 74),
            GETTOOLTIPS = (FIRST + 78),
            SORTITEMSEX = (FIRST + 81),
            SETBKIMAGEA = (FIRST + 68),
            SETBKIMAGEW = (FIRST + 138),
            GETBKIMAGEA = (FIRST + 69),
            GETBKIMAGEW = (FIRST + 139),
            SETSELECTEDCOLUMN = (FIRST + 140),
            SETVIEW = (FIRST + 142),
            GETVIEW = (FIRST + 143),
            INSERTGROUP = (FIRST + 145),
            SETGROUPINFO = (FIRST + 147),
            GETGROUPINFO = (FIRST + 149),
            REMOVEGROUP = (FIRST + 150),
            MOVEGROUP = (FIRST + 151),
            GETGROUPCOUNT = (FIRST + 152),
            GETGROUPINFOBYINDEX = (FIRST + 153),
            MOVEITEMTOGROUP = (FIRST + 154),
            GETGROUPRECT = (FIRST + 98),
            SETGROUPMETRICS = (FIRST + 155),
            GETGROUPMETRICS = (FIRST + 156),
            ENABLEGROUPVIEW = (FIRST + 157),
            SORTGROUPS = (FIRST + 158),
            INSERTGROUPSORTED = (FIRST + 159),
            REMOVEALLGROUPS = (FIRST + 160),
            HASGROUP = (FIRST + 161),
            GETGROUPSTATE = (FIRST + 92),
            GETFOCUSEDGROUP = (FIRST + 93),
            SETTILEVIEWINFO = (FIRST + 162),
            GETTILEVIEWINFO = (FIRST + 163),
            SETTILEINFO = (FIRST + 164),
            GETTILEINFO = (FIRST + 165),
            SETINSERTMARK = (FIRST + 166),
            GETINSERTMARK = (FIRST + 167),
            INSERTMARKHITTEST = (FIRST + 168),
            GETINSERTMARKRECT = (FIRST + 169),
            SETINSERTMARKCOLOR = (FIRST + 170),
            GETINSERTMARKCOLOR = (FIRST + 171),
            GETSELECTEDCOLUMN = (FIRST + 174),
            ISGROUPVIEWENABLED = (FIRST + 175),
            GETOUTLINECOLOR = (FIRST + 176),
            SETOUTLINECOLOR = (FIRST + 177),
            CANCELEDITLABEL = (FIRST + 179),
            MAPINDEXTOID = (FIRST + 180),
            MAPIDTOINDEX = (FIRST + 181),
            ISITEMVISIBLE = (FIRST + 182),
            GETACCVERSION = (FIRST + 193),
            GETEMPTYTEXT = (FIRST + 204),
            GETFOOTERRECT = (FIRST + 205),
            GETFOOTERINFO = (FIRST + 206),
            GETFOOTERITEMRECT = (FIRST + 207),
            GETFOOTERITEM = (FIRST + 208),
            GETITEMINDEXRECT = (FIRST + 209),
            SETITEMINDEXSTATE = (FIRST + 210),
            GETNEXTITEMINDEX = (FIRST + 211),
            SETPRESERVEALPHA = (FIRST + 212),
            SETBKIMAGE = SETBKIMAGEW,
            GETBKIMAGE = GETBKIMAGEW,
            //SETBKIMAGE             = SETBKIMAGEA,
            //GETBKIMAGE             = GETBKIMAGEA,
        }

       internal const UInt32 KEYEVENTF_EXTENDEDKEY = 1;
       internal const UInt32 KEYEVENTF_KEYUP = 2;
       internal const int KEY_ALT = 0x12;
       internal const int KEY_CONTROL = 0x11;
    }
}