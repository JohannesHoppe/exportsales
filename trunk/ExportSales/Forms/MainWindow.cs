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
using ExportSales.Classes;
using System.Globalization;
using System.IO;

namespace ExportSales.Forms
{
    public partial class MainWindow : Form
    {
		#region Fields (6) 

        AboutBox _aboutBox;
        User32Capture _capture;
        ComparablePaymentDataSetList _currentPaymentDataSetList;
        User32Interact _interact;
        ComparablePaymentDataSetList _lastPaymentDataSetList;
        readonly Properties.Settings _settings;

		#endregion Fields 

		#region Constructors (1) 

        public MainWindow()
        {
            InitializeComponent();
            _aboutBox = new AboutBox();

            // fills _settings form
            _settings = new Properties.Settings();
            txtFenstername.Text = _settings.Fenstername;

            foreach (string anchor in _settings.Anker)
            {
                boxAnker.Items.Add(anchor);
            }
            if (boxAnker.Items.Count > 0)
            {
                boxAnker.SelectedIndex = 0;
            }

            boxTyp.Items.Add("Kreditkartenkonto");
            boxTyp.Items.Add("Sparkonto");
            boxTyp.SelectedIndex = 0;

        }

		#endregion Constructors 

		#region Methods (12) 

		// Private Methods (12) 

        /// <summary>
        /// Shows the AboutBox
        /// </summary>
        private void BtnAboutClick(object sender, EventArgs e)
        {
            // if (on any reason) the form was disposed / closed
            if (_aboutBox.IsDisposed)
            {
                _aboutBox = new AboutBox();
            }
            _aboutBox.ShowAnimated();
        }

        /// <summary>
        /// Captures the current StarMoneyWindow
        /// </summary>
        private void BtnCaptureClick(object sender, EventArgs e)
        {
            DoCaptureAndFillForm();
        }

        /// <summary>
        /// 1. Saves old form vars to the TextOutput TextBox
        /// 2. Clicks on "Next" in StarMoney
        /// 3. Captures the current StarMoney window
        /// </summary>
        private void BtnNextClick(object sender, EventArgs e)
        {

            bool makeCSV = (btnCSV.Checked) ? true : false;


            // 1.
            if (_currentPaymentDataSetList != null &&
                !_currentPaymentDataSetList.Equals(_lastPaymentDataSetList))
            {

                if (makeCSV && txtTextOutput.Text == "")
                {
                    txtTextOutput.Text += ToolBox.MakeCsvHeader(boxTyp.Text);
                }


                if (boxTyp.Text == "Kreditkartenkonto")
                {

                    txtTextOutput.Text += ToolBox.MakeTextCommand(_currentPaymentDataSetList,
                                                                  txtKreditSaldo.Text,
                                                                  boxTyp.Text,
                                                                  makeCSV);
                }
                else if (boxTyp.Text == "Sparkonto")
                {
                    txtTextOutput.Text += ToolBox.MakeTextCommand(_currentPaymentDataSetList,
                                                                  txtSparSaldo.Text,
                                                                  boxTyp.Text,
                                                                  makeCSV);
                }
                else
                {
                    throw new Exception("unbekannter Konto-Typ");
                }
            }

            // 2.
            if (DoNextClick())
            {

                // 3.
                DoCaptureAndFillForm();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        private void BtnSaveClick(object sender, EventArgs e)
        {
            if (txtTextOutput.Text == "")
            {
                MessageBox.Show("In der Output-Box ist noch kein Text.\n\rEs gibt nichts zum Speichern!", "Speichern", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            bool makeCSV = (btnCSV.Checked) ? true : false;


            SaveFileDialog dialog = new SaveFileDialog { AddExtension = true };

            if (makeCSV)
            {
                dialog.DefaultExt = "csv";
                dialog.FileName = "output.csv";
                dialog.Filter = "CSV-Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
            }
            else
            {
                dialog.DefaultExt = "sql";
                dialog.FileName = "output.sql";
                dialog.Filter = "SQL-Dateien (*.sql)|*.sql|Alle Dateien (*.*)|*.*";
            }

            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(dialog.FileName, false);
                    sw.Write(txtTextOutput.Text);
                    sw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        /// <summary>
        /// Captures the current StarMoneyWindow and fills the form with data from the capture
        /// </summary>
        private void DoCaptureAndFillForm()
        {
            _capture = new User32Capture(
                txtFenstername.Text,
                "SVWORK",
                boxAnker.Text,
                boxTyp.Text);

            try
            {
                ComparablePaymentDataSetList paymentDataSetList = _capture.DoCapture();

                // ok, we are still in business
                _lastPaymentDataSetList = _currentPaymentDataSetList;
                _currentPaymentDataSetList = paymentDataSetList;


                // fills all TextBoxes with the captured data
                foreach (PaymentDataSet paymentDataSet in paymentDataSetList)
                {
                    TextBox workingTextBox = GetWorkingTextBox(paymentDataSet.Name);
                    workingTextBox.Text = paymentDataSet.Value;
                }


                // the amount would be mixed up if you click multiple times on "Auslesen" (capture)
                if (_lastPaymentDataSetList == null ||
                    !_currentPaymentDataSetList.Equals(_lastPaymentDataSetList))
                {
                    UpdateRunnungBalance(paymentDataSetList);
                }
                // displays a warning
                else
                {
                    MessageBox.Show("Die neuen und die alten Daten sind identisch.\n" + 
                                    "Der fortlaufende Saldo wurde nicht angepasst!",
                                    "Hinweis",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }


            }
            catch (WindowNotFoundException)
            {
                MessageBox.Show("Öffnen Sie zuerst die Umsatzdetails in StarMoney.\n" +
                                "Wechseln Sie dann zurück zu StarMoneySpy.\n\n" +
                                "Das StarMoney-Fenster muss während der gesamten Sitzung geöffnet bleiben!",
                                "Warnung",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Clicks on the next button
        /// </summary>
        private bool DoNextClick() {

            try
            {
                _interact = new User32Interact(txtFenstername.Text, "SVWORK", "Einstellungen");
                _interact.DoNextClick();

                return true;
            }
            catch (WindowNotFoundException)
            {
                MessageBox.Show("Öffnen Sie zuerst die Umsatzdetails in StarMoney.\n" +
                                "Wechseln Sie dann zurück zu StarMoneySpy.\n\n" +
                                "Das StarMoney-Fenster muss während der gesamten Sitzung geöffnet bleiben!",
                                "Warnung",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                return false;
            }

        }

        /// <summary>
        /// Gets all working text boxes.
        /// </summary>
        /// <param name="workingBoxTyp">The box typ (Kreditkartenkonto / Sparkonto / all)</param>
        /// <returns>a list</returns>
        private List<TextBox> GetAllWorkingTextBoxes(string workingBoxTyp)
        {

            List<TextBox> textBoxList = new List<TextBox>();

            if (workingBoxTyp == "Kreditkartenkonto" ||
                workingBoxTyp == "all")
            {
                textBoxList.Add(GetWorkingTextBox("Kredit_Kontoname"));
                textBoxList.Add(GetWorkingTextBox("Kredit_Belegdatum"));
                textBoxList.Add(GetWorkingTextBox("Kredit_Buchungsdatum"));
                textBoxList.Add(GetWorkingTextBox("Kredit_Betrag"));
                textBoxList.Add(GetWorkingTextBox("Kredit_Buchungsreferenz"));
                textBoxList.Add(GetWorkingTextBox("Kredit_Auslandseinsatzentgeld"));
                textBoxList.Add(GetWorkingTextBox("Kredit_Transaktion"));
                textBoxList.Add(GetWorkingTextBox("Kredit_Saldo"));
            }
            if (workingBoxTyp == "Sparkonto" ||
                workingBoxTyp == "all")
            {
                textBoxList.Add(GetWorkingTextBox("Spar_Kontoname"));
                textBoxList.Add(GetWorkingTextBox("Spar_Buchungstag"));
                textBoxList.Add(GetWorkingTextBox("Spar_Wertstellungstag"));
                textBoxList.Add(GetWorkingTextBox("Spar_Absender"));
                textBoxList.Add(GetWorkingTextBox("Spar_Stornobuchung"));
                textBoxList.Add(GetWorkingTextBox("Spar_Primanota"));
                textBoxList.Add(GetWorkingTextBox("Spar_Schlüssel"));
                textBoxList.Add(GetWorkingTextBox("Spar_Betrag"));
                textBoxList.Add(GetWorkingTextBox("Spar_Verwendungszweck"));
                textBoxList.Add(GetWorkingTextBox("Spar_Saldo"));
            }

            if (workingBoxTyp != "Kreditkartenkonto" &&
                workingBoxTyp != "Sparkonto" &&
                workingBoxTyp != "all")
            {
                throw new Exception("unbekannter Konto-Typ");
            }

            return textBoxList;
        }

        /// <summary>
        /// Gets the working text box by its real name
        /// </summary>
        /// <param name="paymentDataSetName">Name of the payment data set.</param>
        /// <returns></returns>
        private TextBox GetWorkingTextBox(string paymentDataSetName)
        {
            switch (paymentDataSetName)
            {
                case "Kredit_Kontoname": return txtKreditKontoname;
                case "Kredit_Belegdatum": return txtKreditBelegdatum;
                case "Kredit_Buchungsdatum": return txtKreditBuchungsdatum;
                case "Kredit_Betrag": return txtKreditBetrag;
                case "Kredit_Buchungsreferenz": return txtKreditBuchungsreferenz;
                case "Kredit_Auslandseinsatzentgeld": return txtKreditAuslandseinsatzentgeld;
                case "Kredit_Transaktion": return txtKreditTransaktion;
                case "Kredit_Saldo": return txtKreditSaldo;

                case "Spar_Kontoname": return txtSparKontoname;
                case "Spar_Buchungstag": return txtSparBuchungstag;
                case "Spar_Wertstellungstag": return txtSparWertstellungstag;
                case "Spar_Absender": return txtSparAbsender;
                case "Spar_Stornobuchung": return txtSparStornobuchung;
                case "Spar_Primanota": return txtSparPrimanota;
                case "Spar_Schlüssel": return txtSparSchlüssel;
                case "Spar_Betrag": return txtSparBetrag;
                case "Spar_Verwendungszweck": return txtSparVerwendungszweck;
                case "Spar_Saldo": return txtSparSaldo;

                default:
                    throw new Exception("paymentDataSet hat ein unbekanntes Format!");
            }
        }

        /// <summary>
        /// Resets the vars if you change the account
        /// </summary>
        private void ResetVars()
        {
            _currentPaymentDataSetList = null;
            _lastPaymentDataSetList = null;
            txtTextOutput.Text = "";

            List<TextBox> allWorkingTextBoxes = GetAllWorkingTextBoxes("all");

            foreach (TextBox textBox in allWorkingTextBoxes)
            {
                textBox.Text = textBox.Name.Contains("Saldo") ? "0.00" : "";
            }

        }

        /// <summary>
        /// Switches beetween the displayed forms
        /// </summary>
        private void SwitchForm(object sender, EventArgs e)
        {
            ComboBox senderboxTyp = (ComboBox)sender;

            if (senderboxTyp.Text == "Kreditkartenkonto")
            {
                groupKreditkarte.Visible = true;
                groupSparkonto.Visible = false;
                tableLayoutPanel1.RowStyles[2] = new RowStyle(SizeType.Absolute, 135F);
                tableLayoutPanel1.RowStyles[3] = new RowStyle(SizeType.Absolute, 0F);
            }
            else if (senderboxTyp.Text == "Sparkonto")
            {
                groupKreditkarte.Visible = false;
                groupSparkonto.Visible = true;
                tableLayoutPanel1.RowStyles[2] = new RowStyle(SizeType.Absolute, 0F);
                tableLayoutPanel1.RowStyles[3] = new RowStyle(SizeType.Absolute, 155F);
            }
            else
            {
                throw new Exception("unbekannter Konto-Typ");
            }

        }

        /// <summary>
        /// Does a autoscroll
        /// </summary>
        private void TxtTextOutputTextChanged(object sender, EventArgs e)
        {
            txtTextOutput.SelectionStart = txtTextOutput.TextLength;
            txtTextOutput.ScrollToCaret();
        }

        /// <summary>
        /// Updates the runnung balance. (Form element this.txtSaldo)
        /// </summary>
        /// <param name="paymentDataSetList">The new captured paymentDataSetList</param>
        private void UpdateRunnungBalance(ComparablePaymentDataSetList paymentDataSetList) {
            
            // for the right formating
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;

            string amount;
            TextBox balanceTextBox;

            #region set amount & balanceTextBox
            if (boxTyp.Text == "Kreditkartenkonto")
            {
                amount = paymentDataSetList.GetValueByName("Kredit_Betrag");
                balanceTextBox = GetWorkingTextBox("Kredit_Saldo");
            }
            else if (boxTyp.Text == "Sparkonto")
            {
                amount = paymentDataSetList.GetValueByName("Spar_Betrag");
                amount = amount.Replace(" EUR", "");
                balanceTextBox = GetWorkingTextBox("Spar_Saldo");
            }
            else
            {
                throw new Exception("unbekannter Konto-Typ");
            }
            #endregion


            if (amount.Equals("")) {
                
                MessageBox.Show("Kein Betrag gefunden!\n" +
                                "Wurde der korrekte Kontotyp ausgewählt?",
                                "Warnung",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            
            amount = ToolBox.GermanAmountToEnglishAmount(amount);

            // calculates running balance
            try
            {
                decimal balance = Decimal.Parse(balanceTextBox.Text, cultureInfo);
                balance += Decimal.Parse(amount, cultureInfo);
                balanceTextBox.Text = balance.ToString("N2", cultureInfo);

            }
            catch (FormatException)
            {
                MessageBox.Show("Der eingegebene Saldo hat ein ungültiges Format!\n" +
                                "Korrekte Werte sind z.B. '1002.50' und '-4.99'! (ohne Anführungszeichen)",
                                "Warnung",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

		#endregion Methods 



        #region events when _settings controls have been changed
        
        /// <summary>
        /// Handles the SelectedIndexChanged event of the boxAnker control.
        /// </summary>
        private void BoxAnkerSelectedIndexChanged(object sender, EventArgs e)
        {
            ResetVars();
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the boxTyp control.
        /// </summary>
        private void BoxTypSelectedIndexChanged(object sender, EventArgs e)
        {
            ResetVars();
        }


        /// <summary>
        /// Handles the CheckedChanged event of the btnCSV control.
        /// </summary>
        private void BtnCsvCheckedChanged(object sender, EventArgs e)
        {
            ResetVars();
        }


        /// <summary>
        /// Handles the CheckedChanged event of the btnSQL control.
        /// </summary>
        private void BtnSqlCheckedChanged(object sender, EventArgs e)
        {
            ResetVars();
        }
        #endregion
    }
}