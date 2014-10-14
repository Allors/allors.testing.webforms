// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Methods.cs" company="allors bvba">
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
namespace Allors.Testing.Webforms
{
    using System;
    using System.Reflection;

    public class Methods
    {
        private readonly Type type;

        public Methods(Type type)
        {
            this.type = type;
        }

        public MethodInfo GetMethodInfo(PageLifeCycleEventArgs args, int requestCount)
        {
            MethodInfo testMethodInfo = null;

            var methods = this.type.GetMethods();
            foreach (var methodInfo in methods)
            {
                var attributes = methodInfo.GetCustomAttributes(typeof(PageLifeCycleEventAttribute), false);
                foreach (var attribute in attributes)
                {
                    var pageLifeCycleEventAttribute = (PageLifeCycleEventAttribute)attribute;
                    if (args.PageLifeCycleEvent.Equals(pageLifeCycleEventAttribute.PageLifeCycleEvent)
                        && pageLifeCycleEventAttribute.RequestCount == requestCount)
                    {
                        if (testMethodInfo != null)
                        {
                            throw new Exception("Duplicate method + " + testMethodInfo);
                        }

                        testMethodInfo = methodInfo;
                    }
                }
            }

            return testMethodInfo;
        }

        public MethodInfo GetMethodInfo(ApplicationLifeCycleEventArgs args, int requestCount)
        {
            MethodInfo testMethodInfo = null;

            var methods = this.type.GetMethods();
            foreach (var methodInfo in methods)
            {
                var attributes = methodInfo.GetCustomAttributes(typeof(ApplicationLifeCycleEventAttribute), false);
                foreach (var attribute in attributes)
                {
                    var applicationLifeCycleEventAttribute = (ApplicationLifeCycleEventAttribute)attribute;
                    if (args.ApplicationLifeCycleEvent.Equals(applicationLifeCycleEventAttribute.ApplicationLifeCycleEvent)
                        && applicationLifeCycleEventAttribute.RequestCount == requestCount)
                    {
                        if (testMethodInfo != null)
                        {
                            throw new Exception("Duplicate method + " + testMethodInfo);
                        }

                        testMethodInfo = methodInfo;
                    }
                }
            }

            return testMethodInfo;
        }

        public MethodInfo GetAfterMethodInfo(int requestCount)
        {
            MethodInfo afterMethodInfo = null;

            var methods = this.type.GetMethods();
            foreach (var methodInfo in methods)
            {
                var attributes = methodInfo.GetCustomAttributes(typeof(AfterAttribute), false);
                foreach (var attribute in attributes)
                {
                    var afterAttribute = (AfterAttribute)attribute;
                    if (afterAttribute.RequestCount == requestCount)
                    {
                        if (afterMethodInfo != null)
                        {
                            throw new Exception("Duplicate method + " + afterMethodInfo);
                        }

                        afterMethodInfo = methodInfo;
                    }
                }
            }

            return afterMethodInfo;
        }
    }
}