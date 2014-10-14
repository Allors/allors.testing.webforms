// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBoxWithoutViewStateOnChangedTest.cs" company="allors bvba">
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

    [WebformsTest("TextBoxWithoutViewState.aspx")]
    public class TextBoxWithoutViewStateOnChangedTest : WebformsTest<TextBoxWithoutViewStatePage>
    {
        [PreRender(1)]
        public void Request1()
        {
            this.MyPage.SelectAll<TextBox>("TextBox")[0].Text = "Hello";

            this.Browser.Click(this.MyPage.SelectAll<Button>("Button")[0]);
        }

        [PreRender(2)]
        public void Request2()
        {
            Assert.AreEqual("Hello", this.MyPage.SelectAll<Label>("Label")[0].Text);
        }

        [PreRender(3)]
        public void Request3()
        {
            this.MyPage.SelectAll<TextBox>("TextBox")[1].Text = "Hello";

            this.Browser.Click(this.MyPage.SelectAll<Button>("Button")[1]);
        }

        [PreRender(4)]
        public void Request4()
        {
            Assert.AreEqual("Hello", this.MyPage.SelectAll<Label>("Label")[1].Text);
        }
    }
}