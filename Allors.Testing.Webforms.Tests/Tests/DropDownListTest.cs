// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DropDownListTest.cs" company="allors bvba">
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
    using System.Web.UI.WebControls;

    using NUnit.Framework;

    public class DropDownListTest : WebformsTest
    {
        public DropDownListPage MyPage
        {
            get
            {
                return (DropDownListPage)this.Page;
            }
        }

        protected override string InitialPath
        {
            get
            {
                return "DropDownListPage.aspx";
            }
        }

        [PreRender(1)]
        public void ServerSelectedIndex()
        {
            this.MyPage.Select<DropDownList>("DropDownList").SelectedIndex = 2;

            this.Browser.Click(this.MyPage.Select<Button>("Button"));
        }

        // AutoPostBack
        [PreRender(2)]
        public void BrowserAutoPostBack()
        {
            Assert.AreEqual("2", this.MyPage.Select<Label>("Label").Text);

            this.Browser.AutoPostBack(this.MyPage.Select<DropDownList>("AutoPostBackDropDownList"), 1);
        }

        [PreRender(3)]
        public void BrowserSet()
        {
            Assert.AreEqual("ValueB", this.MyPage.Select<Label>("Label").Text);

            var dropDownList = this.MyPage.Select<DropDownList>("DropDownList");

            this.Browser.Set(dropDownList, 1);
            this.Browser.Click(this.MyPage.Select<Button>("Button"));
        }

        [PreRender(4)]
        public void FinalCheck()
        {
            Assert.AreEqual("1", this.MyPage.Select<Label>("Label").Text);
        }
    }
}