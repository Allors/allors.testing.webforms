// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBoxTest.cs" company="allors bvba">
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

    [WebformsTest("TextBoxPage.aspx")]
    public class TextBoxTest : WebformsTest<TextBoxPage>
    {
        [PreRender(1)]
        public void FillInTextBox()
        {
            this.MyPage.Select<TextBox>("TextBox").Text = "Hello";

            this.Browser.Click(this.MyPage.Select<Button>("Button"));
        }

        [PreRender(2)]
        public void CheckLabel()
        {
            Assert.AreEqual("Hello", this.MyPage.Select<Label>("Label").Text);
        }
    }
}