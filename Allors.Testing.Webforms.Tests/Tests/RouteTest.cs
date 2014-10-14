// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteTest.cs" company="allors bvba">
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

    [WebformsTest("/Routing/Route.aspx")]
    public class RouteTest : WebformsTest<RoutePage>
    {
        [PreRender(1)]
        public void CheckPage()
        {
            var label1 = this.MyPage.Select<Label>("Label1");
            var label2 = this.MyPage.Select<Label>("Label2");

            Assert.IsTrue(string.IsNullOrEmpty(label1.Text));
            Assert.IsTrue(string.IsNullOrEmpty(label2.Text));

            this.Browser.Navigate("/route/a");
        }

        [PreRender(2)]
        public void Route1()
        {
            var label1 = this.MyPage.Select<Label>("Label1");
            var label2 = this.MyPage.Select<Label>("Label2");

            Assert.AreEqual("a", label1.Text);
            Assert.IsTrue(string.IsNullOrEmpty(label2.Text));

            this.Browser.Navigate("/route/a/b");
        }

        [PreRender(3)]
        public void Route2()
        {
            var label1 = this.MyPage.Select<Label>("Label1");
            var label2 = this.MyPage.Select<Label>("Label2");

            Assert.AreEqual("a", label1.Text);
            Assert.AreEqual("b", label2.Text);
        }

        [PreRender(4)]
        public void Route2Again()
        {
            var label1 = this.MyPage.Select<Label>("Label1");
            var label2 = this.MyPage.Select<Label>("Label2");

            Assert.AreEqual("a", label1.Text);
            Assert.AreEqual("b", label2.Text);
        }
    }
}