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

namespace ExportSales
{
    /// <summary>
    /// Extends AssemblyInfo.cs with the Attribute "AssemblyCompanyUrl"
    /// </summary>

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = true)]
    public class AssemblyCompanyUrlAttribute : Attribute
    {

        private string _strCompanyUrl;

        public AssemblyCompanyUrlAttribute(string url)
        {
            _strCompanyUrl = url;
        }

        public virtual string CompanyUrl
        {
            get
            {
                return _strCompanyUrl;
            }
        }
    }
}