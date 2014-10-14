// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvokeClick.cs" company="allors bvba">
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
namespace Allors.Testing.Webforms.Testers
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Web.UI.WebControls;

    public class InvokeClick
    {
        private readonly IButtonControl control;
        private Exception exception;

        public InvokeClick(IButtonControl control)
        {
            this.control = control;
        }

        public void Execute()
        {
            var thread = new Thread(new ParameterizedThreadStart(PerformClickOnNewThread));
            thread.Start(this);
            thread.Join();
            if (this.exception != null)
            {
                if (!(this.exception is ThreadAbortException))
                {
                    throw this.exception;
                }
            }
        }

        /// <summary>
        /// Performs the click on new thread.
        /// This is necessary if e.g. a Response.Redirect() will be called.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        private static void PerformClickOnNewThread(object obj)
        {
            var invokeClick = (InvokeClick)obj;

            var type = invokeClick.control.GetType();
            var method = type.GetMethod("OnClick", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            object[] methodArgs = { new EventArgs() };
            try
            {
                method.Invoke(invokeClick.control, methodArgs);
            }
            catch (Exception exception)
            {
                invokeClick.exception = exception;
            }
        }
    }
}