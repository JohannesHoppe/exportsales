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
using System.Text;

namespace ExportSales.Classes
{
    class PaymentDataSetHolder
    {
        private readonly ComparablePaymentDataSetList _dataSets;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public PaymentDataSetHolder()
        {
            _dataSets = new ComparablePaymentDataSetList();
        }

        /// <summary>
        /// Adds a new name / value pair to the list
        /// </summary>
        public void AddDataSet(string name, string value)
        {
            _dataSets.Add(new PaymentDataSet(name, value));
        }


        /// <summary>
        /// Returns intern list
        /// </summary>
        /// <returns></returns>
        public ComparablePaymentDataSetList GetPaymentDataSetList()
        {
            return _dataSets;
        }
    }
}
