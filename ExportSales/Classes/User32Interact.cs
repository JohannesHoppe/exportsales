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

namespace ExportSales.Classes
{
    class User32Interact : User32Base
    {
        readonly string _starMoneyWindowTitle;
        readonly string _starMoneyWindowClass;
        readonly string _settingsText;


        /// <summary>
        /// Initializes a new instance of the <see cref="User32Interact"/> class.
        /// </summary>
        /// <param name="starMoneyWindowTitle">The star money window title.</param>
        /// <param name="starMoneyWindowClass">The star money window class.</param>
        /// <param name="settingsText">The settings text.</param>
        public User32Interact(string starMoneyWindowTitle, string starMoneyWindowClass, string settingsText)
        {
            _starMoneyWindowTitle = starMoneyWindowTitle;
            _starMoneyWindowClass = starMoneyWindowClass;
            _settingsText = settingsText;
        }


        /// <summary>
        /// Does the next click in the window labeled "Einstellungen" (settings)
        /// </summary>
        public void DoNextClick()
        {
            IntPtr settingsWindow = GetSettingsWindow();

            const int wParam = (int)User32API.MsgButtons.MK_LBUTTON;

            const int xPos = 140;
            const int yPos = 20;
            int lParam = User32API.MakeLParam(xPos, yPos);

            User32API.SendMessage(settingsWindow, (int)User32API.Msg.WM_LBUTTONDOWN, wParam, lParam);
            User32API.SendMessage(settingsWindow, (int)User32API.Msg.WM_LBUTTONUP, 0x00000000, lParam);
   
        }


        /// <summary>
        /// Gets the settings window that contains the Next Button
        /// </summary>
        /// <returns></returns>
        public IntPtr GetSettingsWindow()
        {
            IntPtr hwndStarMoney = User32API.FindWindow(_starMoneyWindowClass, _starMoneyWindowTitle);

            if (hwndStarMoney.Equals(IntPtr.Zero))
            {
                throw new WindowNotFoundException(_starMoneyWindowTitle);
            }
            IntPtr hwndSettings = FindWindowRecoursivelyByText(hwndStarMoney, _settingsText);

            if (hwndSettings.Equals(IntPtr.Zero))
            {
                throw new WindowNotFoundException(_settingsText);
            }

            return hwndSettings;

        }


    }




}
