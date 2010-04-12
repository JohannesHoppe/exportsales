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

namespace ExportSales.Classes
{
    class PaymentDataSet
    {
        readonly string _name;
        readonly string _value;

        /// <summary>
        /// Initializes a new instance of the PaymentDataSet class.
        /// </summary>
        public PaymentDataSet(string name, string value)
        {
            _name = name;
            _value = value;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                return _value;
            }
        }


        /// <summary>
        /// Bestimmt, ob das angegebene <see cref="T:System.Object"></see> und das aktuelle <see cref="T:System.Object"></see> gleich sind.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;

            // safe because of the GetType check
            PaymentDataSet paymentDataSet = (PaymentDataSet)obj;

            // comparison
            if (!Name.Equals(paymentDataSet.Name)) {
                return false;
            }

            // comparison
            if (!Value.Equals(paymentDataSet.Value))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// i have no real clue
        /// </returns>
        public override int GetHashCode()
        {
            return (Name + Value).GetHashCode();
        }
    }
}
