// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxListPage.aspx.cs" company="allors bvba">
//   Copyright 2008-2014 Allors bvba.
//   
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU Lesser General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU Lesser General Public License for more details.
//   
//   You should have received a copy of the GNU Lesser General Public License
//   along with this program.  If not, see http://www.gnu.org/licenses.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Allors.Testing.Webforms.Tests
{
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class CheckBoxListPage : System.Web.UI.Page
    {
        protected void Button_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            foreach (ListItem item in CheckBoxList.Items)
            {
                if (item.Selected)
                {
                    sb.AppendFormat("{0};", item.Value);
                }
            }

            Label.Text = sb.ToString();
        }

        protected void AutoPostBackRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            foreach (ListItem item in AutoPostBackCheckBoxList.Items)
            {
                if (item.Selected)
                {
                    sb.AppendFormat("{0};", item.Value);
                }
            }

            Label.Text = sb.ToString();
        }
    }
}
