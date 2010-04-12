using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ExportSales;

// Allgemeine Informationen über eine Assembly werden über die folgenden 
// Attribute gesteuert. Ändern Sie diese Attributwerte, um die Informationen zu ändern,
// die mit einer Assembly verknüpft sind.
[assembly: AssemblyTitle("Kreditkarten-Umsätze exportieren")]
[assembly: AssemblyDescription(
@"ExportSales liest die Umsatzdetails von Kreditkarten- und Sparkonten direkt aus den angezeigten Textfeldern in StarMoney aus.

Dieses Programm wurde entwickelt, um die nicht als CSV-Datei exportierbaren Umsätze von Kreditkarten- und Sparkonten in die interne Finanzbuchhaltung zu importieren. Für Umsätze von Girokonten wird der in StarMoney integrierte CSV-Export empfohlen!

Der Autor dieser Software steht in keiner geschäftlichen Beziehung zur Star Finanz GmbH.  
StarMoney ist eine Marke der Star Finanz GmbH.
Mehr Informationen zu StarMoney finden Sie unter www.starmoney.de

+++ Copyright 2007-2010 Johannes Hoppe +++

This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program. If not, see <http://www.gnu.org/licenses/>.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Footbag Freaks Shop")]
[assembly: AssemblyCompanyUrl("http://www.footbag-shop.de")]
[assembly: AssemblyProduct("ExportSales")]
[assembly: AssemblyCopyright("Copyright 2008 Johannes Hoppe")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Durch Festlegen von ComVisible auf "false" werden die Typen in dieser Assembly unsichtbar 
// für COM-Komponenten. Wenn Sie auf einen Typ in dieser Assembly von 
// COM zugreifen müssen, legen Sie das ComVisible-Attribut für diesen Typ auf "true" fest.
[assembly: ComVisible(false)]

// Die folgende GUID bestimmt die ID der Typbibliothek, wenn dieses Projekt für COM verfügbar gemacht wird
[assembly: Guid("0ccffbed-b736-4a56-92f4-f9441d840f39")]

// Versionsinformationen für eine Assembly bestehen aus den folgenden vier Werten:
//
//      Hauptversion
//      Nebenversion 
//      Buildnummer
//      Revision
//
[assembly: AssemblyVersion("0.1.*")]
[assembly: AssemblyFileVersion("0.1")]