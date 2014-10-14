// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxTest.cs" company="allors bvba">
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

    [WebformsTest("CheckBoxPage.aspx")]
    public class CheckBoxTest : WebformsTest<CheckBoxPage>
    {
        #region Controls
        private Label Label
        {
            get { return this.MyPage.Select<Label>("Label"); }
        }

        private CheckBox CheckBox
        {
            get { return this.MyPage.Select<CheckBox>("CheckBox"); }
        }

        private CheckBox AutoPostBackCheckBox
        {
            get { return this.MyPage.Select<CheckBox>("AutoPostBackCheckBox"); }
        }

        #endregion

        [PreRender(1)]
        public void Request1()
        {
            this.CheckBox.Checked = true;

            this.Browser.Click(MyPage.Select<Button>("Button"));
        }

        [PreRender(2)]
        public void Request2()
        {
            Assert.AreEqual("Normal: True", this.Label.Text);

            this.CheckBox.Checked = false;

            this.Browser.Click(MyPage.Select<Button>("Button"));
        }
 
        [PreRender(3)]
        public void Request3()
        {
            Assert.AreEqual("Normal: False", this.Label.Text);

            this.Browser.AutoPostBack(this.AutoPostBackCheckBox);
        }

        [PreRender(4)]
        public void Request4()
        {
            Assert.AreEqual("Auto: True", this.Label.Text);

            this.Browser.AutoPostBack(this.AutoPostBackCheckBox);
        }

        [PreRender(5)]
        public void Request5()
        {
            Assert.AreEqual("Auto: False", this.Label.Text);
        }
    }
}