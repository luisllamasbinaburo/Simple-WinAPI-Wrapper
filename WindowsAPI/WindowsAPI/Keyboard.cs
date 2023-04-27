using System.Threading;
using System.Windows.Forms;
using System;

namespace WindowsAPI
{
    public static class Keyboard
    {
        static Random random = new Random(Environment.TickCount);

        public static void PressKey(int key, bool shift, bool alt, bool ctrl, int holdFor)
        {
            byte bScan = 0x45;
            if (key == KeyConstants.VK_SNAPSHOT) bScan = 0;
            if (key == KeyConstants.VK_SPACE) bScan = 39;

            if (alt) WinAPI.keybd_event(KeyConstants.VK_MENU, 0, 0, 0);
            if (ctrl) WinAPI.keybd_event(KeyConstants.VK_LCONTROL, 0, 0, 0);
            if (shift) WinAPI.keybd_event(KeyConstants.VK_LSHIFT, 0, 0, 0);

            WinAPI.keybd_event(key, bScan, WinAPI.KEYEVENTF_EXTENDEDKEY, 0);

            if (holdFor > 0) Thread.Sleep(holdFor);

            WinAPI.keybd_event(key, bScan, WinAPI.KEYEVENTF_EXTENDEDKEY | WinAPI.KEYEVENTF_KEYUP, 0);

            if (shift) WinAPI.keybd_event(KeyConstants.VK_LSHIFT, 0, WinAPI.KEYEVENTF_KEYUP, 0);
            if (ctrl) WinAPI.keybd_event(KeyConstants.VK_LCONTROL, 0, WinAPI.KEYEVENTF_KEYUP, 0);
            if (alt) WinAPI.keybd_event(KeyConstants.VK_MENU, 0, WinAPI.KEYEVENTF_KEYUP, 0);
        }

        public static void PressKey(int key, bool shift, bool alt, bool ctrl)
        {
            PressKey(key, shift, alt, ctrl, 0);
        }

        public static void PressKey(int key)
        {
            PressKey(key, false, false, false);
        }

        public static void Type(string text, int delay)
        {
            Type(text, delay, delay);
        }

        public static void Type(string text, int delayFrom, int delayTo)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                bool shift = char.IsUpper(c);

                int key;
                if (char.IsLetterOrDigit(c)) key = (int)char.ToUpper(c);
                else
                {
                    if (c == '.' || c == ',') key = KeyConstants.VK_DECIMAL;
                    else if (c == ' ') key = KeyConstants.VK_SPACE;
                    else continue;
                }

                PressKey(key, shift, false, false);

                int delay = random.Next(delayFrom, delayTo);

                Thread.Sleep(delay);
            }
        }
    }
}
