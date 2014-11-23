// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoPostBackTest.cs" company="allors bvba">
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
    using Allors.Testing.Webforms.Extensions;
    using NUnit.Framework;

    public class AutoPostBackTest : WebformsTest
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
        public void One()
        {
            this.Browser.AutoPostBack(this.MyPage.Select<DropDownList>("AutoPostBackDropDownList"), 1);
        }

        [PreRender(2)]
        public void Two()
        {
            Assert.AreEqual("ValueB", this.MyPage.Select<Label>("Label").Text);

            this.Browser.AutoPostBackByText(this.MyPage.Select<DropDownList>("AutoPostBackDropDownList"), "TextC");
        }

        [PreRender(3)]
        public void Three()
        {
            Assert.AreEqual("ValueC", this.MyPage.Select<Label>("Label").Text);

            this.Browser.AutoPostBackByValue(this.MyPage.Select<DropDownList>("AutoPostBackDropDownList"), "ValueA");
        }

        [PreRender(4)]
        public void Four()
        {
            Assert.AreEqual("ValueA", this.MyPage.Select<Label>("Label").Text);
        }
    }
}