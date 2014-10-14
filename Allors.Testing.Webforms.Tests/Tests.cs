// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tests.cs" company="allors bvba">
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
    using System;
    using System.IO;

    using NUnit.Framework;

    [TestFixture]
    public class Tests
    {
        private Application application;

        [TestFixtureSetUp]
        public virtual void Setup()
        {
            var directory = new DirectoryInfo(@"..\..\..\Allors.Testing.Webforms.Tests.WebApplication");
            var applicationFactory = new ApplicationFactory(directory, "/");
            this.application = applicationFactory.Create();
        }

        [TestFixtureTearDown]
        public virtual void Dispose()
        {
        }

        [Test]
        public void TextBox()
        {
            this.application.Test(typeof(TextBoxTest));
        }

        [Test]
        public void TextBoxPost()
        {
            this.application.Test(typeof(TextBoxPostTest), new Logger());
        }

        [Test]
        public void TextBoxNoViewState()
        {
            this.application.Test(typeof(TextBoxWithoutViewStateOnChangedTest));
        }

        [Test]
        public void DropDownList()
        {
            this.application.Test(typeof(DropDownListTest));
        }

        [Test]
        public void RadioButtonList()
        {
            this.application.Test(typeof(RadioButtonListTest));
        }

        [Test]
        public void RadioButton()
        {
            this.application.Test(typeof(RadioButtonTest), new Logger());
        }

        [Test]
        public void CheckBoxList()
        {
            this.application.Test(typeof(CheckBoxListTest));
        }

        [Test]
        public void CheckBox()
        {
            this.application.Test(typeof(CheckBoxTest));
        }

        [Test]
        public void HtmlButton()
        {
            this.application.Test(typeof(HtmlButtonTest));
        }

        [Test]
        public void LinkButton()
        {
            this.application.Test(typeof(LinkButtonTest));
        }

        [Test]
        public void AutoLoginList()
        {
            this.application.Test(typeof(AutoLoginTest), new Logger());
        }

        [Test]
        public void Select()
        {
            this.application.Test(typeof(SelectTest));
        }

        [Test]
        public void EncryptedViewState()
        {
            this.application.Test(typeof(EncryptedViewStateTest));
        }

        [Test]
        public void Redirect()
        {
            this.application.Test(typeof(RedirectTest));
        }

        [Test]
        public void Transfer()
        {
            this.application.Test(typeof(TransferTest), new Logger());
        }

        [Test]
        public void HttpHandler()
        {
            this.application.Test(typeof(HttpHandlerTest));
        }
        
        [Test]
        public void Route()
        {
            this.application.Test(typeof(RouteTest));
        }

        [Test]
        public void Binary()
        {
            this.application.Test(typeof(BinaryTest));
        }

        [Test]
        public void Header()
        {
            this.application.Test(typeof(HeaderTest));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void DuplicateClient()
        {
            this.application.Test(typeof(DuplicateClientTest), new Logger());
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void DuplicateApplication()
        {
            this.application.Test(typeof(DuplicateApplicationTest), new Logger());
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void DuplicatePage()
        {
            this.application.Test(typeof(DuplicatePageTest), new Logger());
        }
    }
}