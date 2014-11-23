// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxListTest.cs" company="allors bvba">
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

    [WebformsTest("CheckBoxListPage.aspx")]
    public class CheckBoxListTest : WebformsTest<CheckBoxListPage>
    {
        [PreRender(1)]
        public void Request1()
        {
            this.MyPage.Select<CheckBoxList>("CheckBoxList").SelectedIndex = 1;

            this.Browser.Click(this.MyPage.Select<Button>("Button"));
        }

        [PreRender(2)]
        public void Request2()
        {
            foreach (ListItem item in this.MyPage.Select<CheckBoxList>("CheckBoxList").Items)
            {
                if (item.Value.Equals("xValue"))
                {
                    Assert.IsTrue(item.Selected);
                }
                else
                {
                    Assert.IsFalse(item.Selected);
                }
            }

            Assert.AreEqual("xValue;", this.MyPage.Select<Label>("Label").Text);

            this.Browser.AutoPostBack(this.MyPage.Select<CheckBoxList>("AutoPostBackCheckBoxList"), 1);
        }

        [PreRender(3)]
        public void Request3()
        {
            foreach (ListItem item in this.MyPage.Select<CheckBoxList>("AutoPostBackCheckBoxList").Items)
            {
                if (item.Value.Equals("bValue"))
                {
                    Assert.IsTrue(item.Selected);
                }
                else
                {
                    Assert.IsFalse(item.Selected);
                }
            }

            this.Browser.AutoPostBack(this.MyPage.Select<CheckBoxList>("AutoPostBackCheckBoxList"), 0);
        }

        [PreRender(4)]
        public void Request4()
        {
            foreach (ListItem item in this.MyPage.Select<CheckBoxList>("AutoPostBackCheckBoxList").Items)
            {
                if (item.Value.Equals("bValue") || item.Value.Equals("aValue"))
                {
                    Assert.IsTrue(item.Selected);
                }
                else
                {
                    Assert.IsFalse(item.Selected);
                }
            }
        }
    }
}