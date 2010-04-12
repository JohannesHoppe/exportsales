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
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace ExportSales.Forms
{
    partial class AboutBox : Form
    {
		#region Constructors (1) 

        public AboutBox()
        {
            InitializeComponent();

            //  Initialisieren Sie AboutBox, um die Produktinformationen aus den Assemblyinformationen anzuzeigen.
            //  Ändern Sie die Einstellungen für Assemblyinformationen für Ihre Anwendung durch eine der folgenden Vorgehensweisen:
            //  - Projekt->Eigenschaften->Anwendung->Assemblyinformationen
            //  - AssemblyInfo.cs
            Text = String.Format("Info über {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;
            linkLabelCompanyName.Text = AssemblyCompany;
            linkLabelCompanyName.Links.Add(0, 999, AssemblyCompanyUrl);
            // Add an event handler to do something when the links are clicked.
            linkLabelCompanyName.LinkClicked += linkLabelCompanyName_LinkClicked;
            textBoxDescription.Text = AssemblyDescription;
        }

		#endregion Constructors 

		#region Methods (4) 

		// Public Methods (1) 

        /// <summary>
        /// Shows the form with the DrawAnimatedRectsAPI and centers it
        /// </summary>
        public void ShowAnimated() {

            int x = (Screen.PrimaryScreen.Bounds.Width / 2) - (Size.Width / 2);
            int y = (Screen.PrimaryScreen.Bounds.Height / 2) - (Size.Height / 2);

            Location = new Point(x, y);
            Show();
        }
		// Private Methods (3) 

        /// <summary>
        /// Handles the FormClosing event of the MainWindow control.
        /// </summary>
        private void AboutBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if user closed the windows
            if (e.CloseReason == CloseReason.UserClosing)
            {

                e.Cancel = true;
                this.Hide();
            }
        }

        private void linkLabelCompanyName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        /// <summary>
        /// Handles the Click event of the okButton control.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

		#endregion Methods 



        #region Assemblyattributaccessoren

        public string AssemblyTitle
        {
            get
            {
                // Alle Title-Attribute in dieser Assembly abrufen
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // Wenn mindestens ein Title-Attribut vorhanden ist
                if (attributes.Length > 0)
                {
                    // Erstes auswählen
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // Zurückgeben, wenn es keine leere Zeichenfolge ist
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // Wenn kein Title-Attribut vorhanden oder das Title-Attribut eine leere Zeichenfolge war, den EXE-Namen zurückgeben
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // Alle Description-Attribute in dieser Assembly abrufen
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // Eine leere Zeichenfolge zurückgeben, wenn keine Description-Attribute vorhanden sind
                if (attributes.Length == 0)
                    return "";
                // Den Wert des Description-Attributs zurückgeben, wenn eines vorhanden ist
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // Alle Product-Attribute in dieser Assembly abrufen
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // Eine leere Zeichenfolge zurückgeben, wenn keine Product-Attribute vorhanden sind
                if (attributes.Length == 0)
                    return "";
                // Den Wert des Product-Attributs zurückgeben, wenn eines vorhanden ist
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // Alle Copyright-Attribute in dieser Assembly abrufen
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // Eine leere Zeichenfolge zurückgeben, wenn keine Copyright-Attribute vorhanden sind
                if (attributes.Length == 0)
                    return "";
                // Den Wert des Copyright-Attributs zurückgeben, wenn eines vorhanden ist
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // Alle Company-Attribute in dieser Assembly abrufen
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // Eine leere Zeichenfolge zurückgeben, wenn keine Company-Attribute vorhanden sind
                if (attributes.Length == 0)
                    return "";
                // Den Wert des Company-Attributs zurückgeben, wenn eines vorhanden ist
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public string AssemblyCompanyUrl
        {
            get
            {
                // Alle Company-Attribute in dieser Assembly abrufen
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyUrlAttribute), false);
                // Eine leere Zeichenfolge zurückgeben, wenn keine CompanyUrl-Attribute vorhanden sind
                if (attributes.Length == 0)
                    return "";
                // Den Wert des Company-Attributs zurückgeben, wenn eines vorhanden ist
                return ((AssemblyCompanyUrlAttribute)attributes[0]).CompanyUrl;
            }
        }
        #endregion
    }
}
