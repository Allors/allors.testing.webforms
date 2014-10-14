// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderTest.cs" company="allors bvba">
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
    using System.Web;
    using System.Web.UI.WebControls;

    using NUnit.Framework;

    [WebformsTest("Header.aspx")]
    public class HeaderTest : WebformsTest<HeaderPage>
    {
        [PreRender(1)]
        public void Check()
        {
            var label = this.MyPage.Select<Label>("Label");

            Assert.AreEqual("navigator", label.Text);
        }

        protected override void OnRequest(WorkerRequest request)
        {
            base.OnRequest(request);

            if (this.RequestCount == 1)
            {
                request.SetHeader(HttpWorkerRequest.HeaderUserAgent, "navigator");
            }
        }
    }
}