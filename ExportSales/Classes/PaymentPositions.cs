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
    class PaymentPositions
    {
        private readonly List<PositionItem> _positions;
        private readonly string _accountType;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public PaymentPositions(string accountType) {

            _accountType = accountType;

            _positions = new List<PositionItem>();

            if (_accountType == "Kreditkartenkonto")
            { 
                _positions.Add(new PositionItem("Kredit_Kontoname", 0));
                _positions.Add(new PositionItem("Kredit_Belegdatum", 8));
                _positions.Add(new PositionItem("Kredit_Buchungsdatum", 10));
                _positions.Add(new PositionItem("Kredit_Betrag", 20));
                _positions.Add(new PositionItem("Kredit_Buchungsreferenz", 19));
                _positions.Add(new PositionItem("Kredit_Auslandseinsatzentgeld", 22));
                _positions.Add(new PositionItem("Kredit_Transaktion", 15));
            }
            else if (_accountType == "Sparkonto")
            {
                _positions.Add(new PositionItem("Spar_Kontoname", 0));
                _positions.Add(new PositionItem("Spar_Buchungstag", 8));
                _positions.Add(new PositionItem("Spar_Wertstellungstag", 9));
                _positions.Add(new PositionItem("Spar_Absender", 19));
                _positions.Add(new PositionItem("Spar_Stornobuchung", 25));
                _positions.Add(new PositionItem("Spar_Primanota", 24));
                _positions.Add(new PositionItem("Spar_Schlüssel", 11));
                _positions.Add(new PositionItem("Spar_Betrag", 16));
                _positions.Add(new PositionItem("Spar_Verwendungszweck", 17));
            }
            else
            {
                throw new Exception("unbekannter Konto-Typ");
            }

        }

        /// <summary>
        /// Gets the name of the position.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>
        /// item (string), or empty string if there is no needed data on this position
        /// </returns>
        public string GetPositionName(int index)
        {
            foreach (PositionItem position in _positions)
            {
                if (index == position.Index)
                {
                    return position.Name;
                }
            }

            return "";
        }
    }
}
