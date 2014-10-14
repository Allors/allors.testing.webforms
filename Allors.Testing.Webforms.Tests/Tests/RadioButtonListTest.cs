// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadioButtonListTest.cs" company="allors bvba">
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
    using System.Diagnostics;
    using System.Web.UI.WebControls;

    using NUnit.Framework;

    public class RadioButtonListTest : WebformsTest<RadioButtonListPage>
    {
        protected override string InitialPath
        {
            get
            {
                return "RadioButtonListPage.aspx";
            }
        }

        // Button Click (Set on OnLoad)
        [Load(2)]
        public void SetSelectedIndexToOne()
        {
            Debug.Print("SetSelectedIndexToOne");
            this.MyPage.Select<RadioButtonList>("RadioButtonList").SelectedIndex = 1;
        }

        [PreRender(1)]
        public void FirstTimeRaiseClickEventOnNextPost()
        {
            Debug.Print("FirstTimeRaiseClickEventOnNextPost");

            this.Browser.Click(this.MyPage.Select<Button>("Button"));
        }

        [LoadComplete(2)]
        public void CheckFirstTime()
        {
            Debug.Print("CheckFirstTime");
            Assert.AreEqual("yValue", this.MyPage.Select<Label>("Label").Text);
        }

        // Button Click (Set in ViewState)
        [PreRender(2)]
        public void SetSelectedIndexToZeroAndRaiseClickEventOnNextPost()
        {
            Debug.Print("SetSelectedIndexToZeroAndRaiseClickEventOnNextPost");
            this.MyPage.Select<RadioButtonList>("RadioButtonList").SelectedIndex = 0;

            this.Browser.Click(this.MyPage.Select<Button>("Button"));
        }

        [PreRender(3)]
        public void CheckSecondTimeAndRaiseAutoPostBackNextPost()
        {
            Debug.Print("CheckSecondTimeAndRaiseAutoPostBackNextPost");
            Assert.AreEqual("xValue", this.MyPage.Select<Label>("Label").Text);

            this.Browser.AutoPostBack(this.MyPage.Select<RadioButtonList>("AutoPostBackRadioButtonList"), 1);
        }

        [LoadComplete(4)]
        public void CheckThirdTime()
        {
            Debug.Print("CheckThirdTime");
            Assert.AreEqual("bValue", this.MyPage.Select<Label>("Label").Text);
        }
    }
}