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
using System.Windows.Forms;

namespace ExportSales.Classes
{
    class User32Capture : User32Base
    {
        readonly string _starMoneyWindowTitle;
        readonly string _starMoneyWindowClass;
        readonly string _anchorText;
        readonly string _accountType;


        /// <summary>
        /// Initializes a new instance of the <see cref="User32Capture"/> class.
        /// </summary>
        /// <param name="starMoneyWindowTitle">The star money window title.</param>
        /// <param name="starMoneyWindowClass">The star money window class.</param>
        /// <param name="anchorText">The anchor text.</param>
        /// <param name="accountType">Type of the account.</param>
        public User32Capture(string starMoneyWindowTitle,
                             string starMoneyWindowClass,
                             string anchorText,
                             string accountType)
        {
            _starMoneyWindowTitle = starMoneyWindowTitle;
            _starMoneyWindowClass = starMoneyWindowClass;
            _anchorText = anchorText;
            _accountType = accountType;
        }


        /// <summary>
        /// Does the capture.
        /// </summary>
        /// <returns>List with values</returns>
        public ComparablePaymentDataSetList DoCapture()
        {

            IntPtr formWindow = GetFormWindow();
            List<IntPtr> childWindows = User32API.GetChildWindows(formWindow);


            PaymentPositions paymentPositions = new PaymentPositions(_accountType);
            PaymentDataSetHolder paymentDataSetHolder = new PaymentDataSetHolder();
            

            int i = 0;
            foreach (IntPtr childWindow in childWindows)
            {
                string className = User32API.GetClassName(childWindow);


                // Edit class (same for TextBox, if there would be one )
                string windowText = className.Equals("Edit") ? User32API.GetInputText(childWindow) : User32API.GetWindowText(childWindow);

                // cleaning
                windowText = ToolBox.PrepareString(windowText);

                string positionName = paymentPositions.GetPositionName(i);

                
                // DEBUGGING
                /*
                MessageBox.Show("Kind Text: " + windowText + "\n" +
                "Klasse: " + className + "\n" +
                "Name: " + positionName + "\n" +
                "Index: " + i );
                */
                
                if (!positionName.Equals(""))
                {
                    paymentDataSetHolder.AddDataSet(positionName, windowText);
                }
            
                i++;    
            }

            ComparablePaymentDataSetList dataSetList = paymentDataSetHolder.GetPaymentDataSetList();

            return dataSetList;    
        }


        /// <summary>
        /// Gets the form window that contains the data windows
        /// </summary>
        /// <returns></returns>
        public IntPtr GetFormWindow()
        {
            IntPtr hwndStarMoney = User32API.FindWindow(_starMoneyWindowClass, _starMoneyWindowTitle);

            if (hwndStarMoney.Equals(IntPtr.Zero))
            {
                throw new WindowNotFoundException(_starMoneyWindowTitle);
            }
            
            IntPtr anchorWindow = FindWindowRecoursivelyByText(hwndStarMoney, _anchorText);

            if (anchorWindow.Equals(IntPtr.Zero))
            {
                throw new WindowNotFoundException(_anchorText);
            }

            return User32API.GetParent(anchorWindow);
        }
    }
}
