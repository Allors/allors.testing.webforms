// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectTest.cs" company="allors bvba">
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
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Allors.Testing.Webforms.Extensions;
    using NUnit.Framework;

    [WebformsTest("SelectPage.aspx")]
    public class SelectTest : WebformsTest<SelectPage>
    {
        [PreRender(1)]
        public void SelectAllById()
        {
            var buttonsWithId1 = MyPage.SelectAll<Button>("Button1");
            var buttonsWithId2 = MyPage.SelectAll<Button>("Button2");
            var buttonsWithId3 = MyPage.SelectAll<Button>("Button3");

            Assert.AreEqual(2, buttonsWithId1.Length);
            Assert.AreEqual(2, buttonsWithId2.Length);
            Assert.AreEqual(1, buttonsWithId3.Length);
            
            var userControl = MyPage.Select<UserControl>("SelectUserControl");

            Assert.IsNotNull(userControl);
            
            buttonsWithId1 = userControl.SelectAll<Button>("Button1");
            buttonsWithId2 = userControl.SelectAll<Button>("Button2");
            buttonsWithId3 = userControl.SelectAll<Button>("Button3");

            Assert.AreEqual(1, buttonsWithId1.Length);
            Assert.AreEqual(1, buttonsWithId2.Length);
            Assert.AreEqual(0, buttonsWithId3.Length);

            var exceptionThrown = false;
            try
            {
                MyPage.Select<Button>("Button1");
            }
            catch
            {
                exceptionThrown = true;
            }
           
            Assert.IsTrue(exceptionThrown);

            var button3 = MyPage.Select<Button>("Button3");
            Assert.IsNotNull(button3);

            var checkBox = MyPage.Select<CheckBox>("doesntexist");
            Assert.IsNull(checkBox);

            var checkBoxes = MyPage.SelectAll<CheckBox>("doesntexist");
            Assert.AreEqual(0, checkBoxes.Length);

            var panel = Page.Select<Panel>("SameId");
            Assert.IsNotNull(panel);

            this.Browser.Click(button3);
        }

        [PreRender(2)]
        public void SelectAllByType()
        {
            var allButtons = MyPage.SelectAll<Button>();
            Assert.AreEqual(5, allButtons.Length);
            
            var selectUserControl = MyPage.Select<SelectUserControl>();
            Assert.IsNotNull(selectUserControl);

            var selectUserControlButtons = selectUserControl.SelectAll<Button>();
            Assert.AreEqual(2, selectUserControlButtons.Length);

            var exceptionThrown = false; 
            try
            {
                MyPage.Select<Button>();
                Assert.Fail("A select with multiple controls should fail.");
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            var checkBox = MyPage.Select<CheckBox>();
            Assert.IsNull(checkBox);

            var checkBoxes = MyPage.SelectAll<CheckBox>();
            Assert.AreEqual(0, checkBoxes.Length);
        }
    }
}