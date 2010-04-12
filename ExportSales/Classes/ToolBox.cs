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
using System.Text.RegularExpressions;

namespace ExportSales.Classes
{
    /// <summary>
    /// some static functions
    /// </summary>
    static class ToolBox
    {

        /// <summary>
        /// Trims string and replaces \n with \r\n
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>changed string</returns>
        public static string PrepareString(string str)
        {
            str = str.Trim();
            str = str.Replace("\n", "\r\n");
            return str;
        }


        /// <summary>
        /// +1.1212,02 -> 11212.02
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>changed string</returns>
        public static string GermanAmountToEnglishAmount(string str)
        {
            str = str.Replace(".", "");
            str = str.Replace(",", ".");
            str = str.Replace("+", "");
            return str;
        }


        /// <summary>
        /// +1,1212.02 -> 11212.02
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>changed string</returns>
        public static string EnglishAmountToGermanAmount(string str)
        {
            str = str.Replace(",", "");
            str = str.Replace(".", ",");
            str = str.Replace("+", "");
            return str;
        }


        /// <summary>
        /// 24.12.2007 -> 2007-12-24
        /// 
        /// no check for the right format
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>changed string</returns>
        public static string GermanDateToSQLDate(string str)
        {
            // not needed at the moment
            if (str == null)
            {
                return "NULL";
            }

            string[] strArray = str.Split('.');

            str = strArray[2] + "-" + strArray[1] + "-" + strArray[0];
            return str;
        }





        /// <summary>
        /// Makes an INSERT INTO SQL command or with the help of the form vars
        /// </summary>
        /// <returns>sql command</returns>
        public static string MakeTextCommand(ComparablePaymentDataSetList paymentDataSetList,
                                            string balance, string boxType, bool makeCSV)
        {
            if (boxType == "Kreditkartenkonto")
            {
                return MakeTextCommand_Kredit(paymentDataSetList, balance, makeCSV);
            }
            if (boxType == "Sparkonto")
            {
                return MakeTextCommand_Spar(paymentDataSetList, balance, makeCSV);
            }
            
            throw new Exception("unbekannter Konto-Typ");
        }


        /// <summary>
        /// builds the SQL command for a list that represents a credit card dataset
        /// </summary>
        /// <param name="paymentDataSetList">The payment data set list.</param>
        /// <param name="balance">The balance.</param>
        /// <returns></returns>
        public static string MakeTextCommand_Kredit(ComparablePaymentDataSetList paymentDataSetList, string balance, bool makeCSV)
        {

            string number = paymentDataSetList.GetValueByName("Kredit_Buchungsreferenz");
            string bookingdate = GermanDateToSQLDate(paymentDataSetList.GetValueByName("Kredit_Buchungsdatum"));
            string valuedate = GermanDateToSQLDate(paymentDataSetList.GetValueByName("Kredit_Belegdatum"));
            string reasonForTransfer = paymentDataSetList.GetValueByName("Kredit_Transaktion").Replace("\r\n", "\\n");
            string amount = GermanAmountToEnglishAmount(paymentDataSetList.GetValueByName("Kredit_Betrag"));
            // needs to be a neutral culture string
            balance = balance.Replace(",", "");

           
            if (!paymentDataSetList.GetValueByName("Kredit_Auslandseinsatzentgeld").Equals(""))
            {
                reasonForTransfer += "\\n" + paymentDataSetList.GetValueByName("Kredit_Auslandseinsatzentgeld");
            }

            // looks nicer
            if (reasonForTransfer.Equals("Einzug des Rechnungsbetra\\nges"))
            {
                reasonForTransfer = "Einzug des Rechnungsbetrages";
            }



            string tmp = "";

            // MS Excel compatible
            if (makeCSV)
            {
                tmp += QuoteCSVItem(number) + ";";
                tmp += QuoteCSVItem(bookingdate) + ";";
                tmp += QuoteCSVItem(valuedate) + ";";
                tmp += QuoteCSVItem(reasonForTransfer) + ";";
                tmp += QuoteCSVItem(EnglishAmountToGermanAmount(amount)) + ";";
                tmp += QuoteCSVItem(EnglishAmountToGermanAmount(balance)) + "\r\n";
            }
            else
            {
                tmp += "INSERT INTO payments SET ";
                tmp += "payments_accounts_id = 7, ";
                tmp += "number = '" + number + "', ";
                tmp += "bookingdate = '" + bookingdate + "', ";
                tmp += "valuedate = '" + valuedate + "', ";
                tmp += "reason_for_transfer = '" + reasonForTransfer + "', ";
                tmp += "amount = '" + amount + "', ";
                tmp += "balance = '" + balance + "'";
                tmp += ";\r\n";
            }

            return tmp;

        }


        /// <summary>
        /// builds the SQL command for a list that represents a savings account dataset
        /// </summary>
        /// <param name="paymentDataSetList">The payment data set list.</param>
        /// <param name="balance">The balance.</param>
        /// <returns></returns>
        public static string MakeTextCommand_Spar(ComparablePaymentDataSetList paymentDataSetList, string balance, bool makeCSV)
        {

            string primanota = paymentDataSetList.GetValueByName("Spar_Primanota");
            string bookingdate = GermanDateToSQLDate(paymentDataSetList.GetValueByName("Spar_Buchungstag"));
            string valuedate = GermanDateToSQLDate(paymentDataSetList.GetValueByName("Spar_Wertstellungstag"));
            string tmpKey = paymentDataSetList.GetValueByName("Spar_Schlüssel");
            string name = paymentDataSetList.GetValueByName("Spar_Absender");
            string reasonForTransfer = paymentDataSetList.GetValueByName("Spar_Verwendungszweck").Replace("\r\n", "\\n");
            string amount = GermanAmountToEnglishAmount(paymentDataSetList.GetValueByName("Spar_Betrag"));
            // needs to be a neutral culture string
            balance = balance.Replace(",", "");

            amount = amount.Replace(" EUR", "");

            Match match = Regex.Match(tmpKey, @"\(([0-9]+)\)$");
            string txtKey = "";
            if (match.Groups.Count > 1)
            {
                txtKey = match.Groups[1].ToString();
            }

            string postingtext = Regex.Replace(tmpKey, @"\s{0,1}\([0-9]+\)$", "");
            if (postingtext.Equals(""))
            {
                postingtext = "NULL";
            }
            else
            {
                postingtext = "'" + postingtext + "'";
            }

                       

            string tmp = "";

            if (makeCSV)
            {
                tmp += QuoteCSVItem(primanota) + ";";
                tmp += QuoteCSVItem(bookingdate) + ";";
                tmp += QuoteCSVItem(valuedate) + ";";
                tmp += QuoteCSVItem(txtKey) + ";";
                tmp += QuoteCSVItem(postingtext) + ";";
                tmp += QuoteCSVItem(name) + ";";
                tmp += QuoteCSVItem(reasonForTransfer) + ";";
                tmp += QuoteCSVItem(EnglishAmountToGermanAmount(amount)) + ";";
                tmp += QuoteCSVItem(EnglishAmountToGermanAmount(balance)) + "\r\n";
            }
            else
            {
                tmp += "INSERT INTO payments SET ";
                tmp += "payments_accounts_id = 6, ";
                tmp += "primanota = '" + primanota + "', ";
                tmp += "bookingdate = '" + bookingdate + "', ";
                tmp += "valuedate = '" + valuedate + "', ";
                tmp += "txt_key = '" + txtKey + "', ";
                tmp += "postingtext = " + postingtext + ", ";
                tmp += "name = '" + name + "', ";
                tmp += "reason_for_transfer = '" + reasonForTransfer + "', ";
                tmp += "amount = '" + amount + "', ";
                tmp += "balance = '" + balance + "'";
                tmp += ";\r\n";
            }

            return tmp;

        }

        /// <summary>
        /// Quotes given string
        /// </summary>
        /// <returns>quoted string, if needed</returns>
        public static string QuoteCSVItem(string s)
        {
            if (s.IndexOfAny("\";".ToCharArray()) > -1)
            {
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            }
            return s;
        }


        /// <summary>
        /// Returns one of the two hardcoded string
        /// </summary>
        /// <param name="boxType">'Kreditkartenkonto' or 'Sparkonto'</param>
        /// <returns>header</returns>
        public static string MakeCsvHeader(string boxType)
        {
            if (boxType == "Kreditkartenkonto")
            {
                return "Buchungsreferenz;Buchungsdatum;Belegdatum;Transaktion;Betrag;Saldo\r\n";
            }
            if (boxType == "Sparkonto")
            {
                return "Primanota;Buchungstag;Wertstellungstag;Schlüssel;Absender;Verwendungszweck;Betrag;Saldo\r\n";
            }
            throw new Exception("unbekannter Konto-Typ");
        }
    }
}
