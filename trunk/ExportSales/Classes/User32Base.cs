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
using System.Collections.Generic;

namespace ExportSales.Classes
{
    class User32Base
    {

        /// <summary>
        /// goes recoursively through all windows and finds the window with the given windowText
        /// </summary>
        /// <param name="currentWindow">the current window</param>
        /// <param name="searchText">the requested search string</param>
        /// <returns>found window or IntPtr.Zero</returns>
        public IntPtr FindWindowRecoursivelyByText(IntPtr currentWindow, string searchText)
        {

            // do we have the right window??!
            string windowText = User32API.GetWindowText(currentWindow);
            if (windowText.Equals(searchText))
            {
                return currentWindow;
            }

            // no, lets continue the search!
            List<IntPtr> childWindows = User32API.GetChildWindows(currentWindow);

            foreach (IntPtr childWindow in childWindows)
            {
                IntPtr findWindow = FindWindowRecoursivelyByText(childWindow, searchText);

                if (findWindow != IntPtr.Zero)
                {
                    return findWindow;
                }
            }

            return IntPtr.Zero;
        }

    }
}
