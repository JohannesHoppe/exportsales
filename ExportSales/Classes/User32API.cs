#region License, Terms and Conditions
//
// ExportSales
// Copyright 2007-2010 Johannes Hoppe
//
// This file is part of ExportSales.
//
// ExportSales is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// ExportSales is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with ExportSales.  If not, see <http://www.gnu.org/licenses/>.
//
#endregion

using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace ExportSales.Classes
{
    public static class User32API
    {
        // The FindWindow function retrieves a handle to the top-level window whose
        // class name and window name match the specified strings. This function does
        // not search child windows. This function does not perform a case-sensitive search.
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        // To search child windows, beginning with a specified child window, use the FindWindowEx function.
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);



        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowProc lpEnumFunc, IntPtr lParam);

        public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);
        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern IntPtr EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);


        // The GetWindowText function copies the text of the specified window's title bar
        // (if it has one) into a buffer. If the specified window is a control, the text
        // of the control is copied. However, GetWindowText cannot retrieve the text of a
        // control in another application.
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);



        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);



        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);



        // Sends the specified message to a window or windows.
        // It calls the window procedure for the specified window and does not
        // return until the window procedure has processed the message.
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, [Out] StringBuilder lParam);
        [DllImport("user32.dll")]
        public static extern uint SendMessage(IntPtr hWnd, uint MSG, uint zero, byte[] text);

        // for SendMessage
        public const uint WM_SETTEXT = 0x000C;
        public const uint WM_GETTEXT = 0x000D;
        public const uint WM_GETTEXTLENGTH = 0x000E;


        // The SendMessage function sends the specified message to a 
        // window or windows. It calls the window procedure for the specified 
        // window and does not return until the window procedure
        // has processed the message. 
        [DllImport("user32.dll")]
        public static extern uint SendMessage(
            IntPtr hWnd,            // handle to destination window
            uint Msg,               // message
            int wParam,             // first message parameter
            [MarshalAs(UnmanagedType.LPStr)] string lParam); // second message parameter

        [DllImport("user32.dll")]
        public static extern uint SendMessage(
            IntPtr hWnd,            // handle to destination window
            uint Msg,               // message
            int wParam,             // first message parameter
            int lParam);            // second message parameter



        /// <summary>
        /// Messages for SendMessage
        /// </summary>
        public enum Msg : int
        {
            WM_MOUSEFIRST = 0x0200,
            WM_MOUSEMOVE = 0x0200,
            
            // !!!
            WM_LBUTTONDOWN = 0x0201,
            // !!!
            WM_LBUTTONUP = 0x0202,
            
            WM_LBUTTONDBLCLK = 0x0203,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_RBUTTONDBLCLK = 0x0206,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_MBUTTONDBLCLK = 0x0209,
            WM_MOUSEWHEEL = 0x020A,
            WM_MOUSELAST = 0x020D,
        }


        /// <summary>
        /// wParam for WM_LBUTTONDOWN Notification
        /// Indicates whether various virtual keys are down.
        /// This parameter can be one or more of the following values.
        /// 
        ///  wParam for WM_LBUTTONUP Notification: 0x00000000 instead of MK_LBUTTON also valid
        /// </summary>
        public enum MsgButtons : int
        {
            /// <summary>
            ///  The left mouse button is down.
            /// </summary>
            MK_LBUTTON = 0x0001,
            /// <summary>
            /// The right mouse button is down.
            /// </summary>
            MK_RBUTTON = 0x0002,
            /// <summary>
            /// The SHIFT key is down.
            /// </summary>
            MK_SHIFT = 0x0004,
            /// <summary>
            /// The CTRL key is down.
            /// </summary>
            MK_CONTROL = 0x0008,
            /// <summary>
            /// The middle mouse button is down.
            /// </summary>
            MK_MBUTTON = 0x0010,
            /// <summary>
            /// Windows 2000/XP: The first X button is down.
            /// </summary>
            MK_XBUTTON1 = 0x0020,
            /// <summary>
            /// Windows 2000/XP: The second X button is down.
            /// </summary>
            MK_XBUTTON2 = 0x0040,
        }



        /// <summary>
        /// Makes an lParam for SendMessage
        /// used WM_LBUTTONDOWN / WM_LBUTTONUP Notification
        /// </summary>
        /// <param name="LoWord">The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.</param>
        /// <param name="HiWord">The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.</param>
        /// <returns></returns>
        public static int MakeLParam(int LoWord, int HiWord)
        {
            return (int) ((HiWord << 16) | (LoWord & 0xffff));
        }

        /// <summary>
        /// not used atm
        /// </summary>
        /// <param name="LoWord">The lo word.</param>
        /// <param name="HiWord">The hi word.</param>
        /// <returns></returns>
        public static int MakeLong(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        /// <summary>
        /// HIWORD macro - not used atm
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static int HiWord(int number)
        {
            if ((number & 0x80000000) == 0x80000000)
                return (number >> 16);
            else
                return (number >> 16) & 0xffff;
        }

        /// <summary>
        /// LOWORD  macro - not used atm 
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static int LoWord(int number)
        {
            return number & 0xffff;
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
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }


        /// <summary>
        /// Callback method to be used when enumerating windows.
        /// </summary>
        /// <param name="handle">Handle of the next window</param>
        /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
        /// <returns>True to continue the enumeration, false to bail</returns>
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
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
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);


        /// <summary>
        /// returns the text of an windows handle
        /// </summary>
        /// <param name="hWnd">windows handle</param>
        /// <returns>text (caption)</returns>
        public static string GetWindowText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }


        /// <summary>
        /// returns the class name of an windows handle
        /// </summary>
        /// <param name="hWnd">windows handle</param>
        /// <returns>class name</returns>
        public static string GetClassName(IntPtr hWnd)
        {
            StringBuilder sb = new StringBuilder("", 256);
            GetClassName(hWnd, sb, 256);
            return sb.ToString();
        }


        /// <summary>
        /// returns the content of an Window that is a Edit-Class or a TextBox-Class
        /// </summary>
        /// <param name="hWnd">windows handle</param>
        /// <returns>class content</returns>
        public static string GetInputText(IntPtr hWnd)
        {
            int textLength = SendMessage(hWnd, WM_GETTEXTLENGTH, 0, IntPtr.Zero);
            StringBuilder sb = new StringBuilder(textLength + 1);
            SendMessage(hWnd, WM_GETTEXT, textLength + 1, sb);
            return sb.ToString(); 
        }
        
    }
}

