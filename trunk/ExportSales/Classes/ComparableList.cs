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

using System.Collections.Generic;

namespace ExportSales.Classes
{
    class ComparablePaymentDataSetList : List<PaymentDataSet>
    {

        /// <summary>
        /// decides if the given list identical in case of contained elements and their order
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;

            // safe because of the GetType check
            ComparablePaymentDataSetList otherList = (ComparablePaymentDataSetList)obj;

            // comparison
            int thisCount = Count;
            int otherListCount = otherList.Count;

            for (int i = 0; i < thisCount; i++)
            {

                if (i >= otherListCount)
                {
                    return false;
                }

                if (!this[i].Equals(otherList[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <returns>
        /// i have no real clue
        /// </returns>
        public override int GetHashCode()
        {
            int code = 0;

            foreach (PaymentDataSet paymentDataSet in this) {

                code = code ^ paymentDataSet.GetHashCode();
            }

            return code;
        }


        /// <summary>
        /// returns a value for a given name in the list OR null
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetValueByName(string name)
        {
            PaymentDataSet paymentDataSet = Find(p => (p.Name == name));
            return (paymentDataSet != null) ? paymentDataSet.Value: null;
        }

    }
}
