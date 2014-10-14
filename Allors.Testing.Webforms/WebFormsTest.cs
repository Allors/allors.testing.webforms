// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebformsTest.cs" company="allors bvba">
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
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Allors.Testing.Webforms.Testers;

    public abstract class WebformsTest : IWebformsTest, IBrowser
    {
        private const string ViewStateKey = "__VIEWSTATE";
        private const string ViewStateEncryptedKey = "__VIEWSTATEENCRYPTED";
        private const string EventvalidationKey = "__EVENTVALIDATION";
        private const string Equal = "=";
        private const string Separator = "&";

        private static readonly ITester DefaultTester = new DefaultTester();

        private static readonly Regex ViewStateRegex = new Regex("__VIEWSTATE\" value=\"(.*)\"", RegexOptions.Multiline | RegexOptions.Compiled);
        private static readonly Regex EventValidationRegex = new Regex("__EVENTVALIDATION\" value=\"(.*)\"", RegexOptions.Multiline | RegexOptions.Compiled);

        private readonly Dictionary<Type, ITester> testerByType;

        private readonly Methods methods;

        private int requestCount;
        private string navigatePath;
        private string navigateQuery;
        private Dictionary<string, HttpCookie> cookies;
        private Dictionary<string, string> postDataByControlUniqueId;
        private Control autoPostBackControl;

        private Exception exception;

        private bool loginInProgress;
        private int expectedRequestCount;
        private Page page;
        
        protected WebformsTest()
        {
            this.testerByType = new Dictionary<Type, ITester>();
            this.methods = new Methods(this.GetType());

            this.cookies = new Dictionary<string, HttpCookie>();
            this.postDataByControlUniqueId = new Dictionary<string, string>();
        }

        public WorkerResponse Response { get; private set; }

        public Dictionary<string, string> PostDataByControlUniqueId
        {
            get { return this.postDataByControlUniqueId; }
        }

        public int RequestCount
        {
            get { return this.requestCount; }
        }

        public virtual string LoginPath
        {
            get
            {
                return null;
            }
        }

        public Page Page
        {
            get { return this.page; }
        }
        
        public virtual Logger Logger { get; set; }

        public IBrowser Browser 
        { 
            get
            {
                return this;
            }
        }

        protected virtual string InitialPath
        {
            get
            {
                var attribute = (WebformsTestAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(WebformsTestAttribute));
                if (attribute != null)
                {
                    return attribute.Path;
                }

                return "Pages/local/Test.aspx";
            }
        }

        protected virtual string InitialQuery
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual string Login
        {
            get
            {
                var attribute = (WebformsTestAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(WebformsTestAttribute));
                if (attribute != null)
                {
                    return attribute.Login;
                }

                return null;
            }
        }

        protected virtual bool CheckForErrorsInPage
        {
            get { return true; }
        }

        protected virtual int ExpectedStatusCode
        {
            get
            {
                return 200;
            }
        }

        void IBrowser.Navigate(string url)
        {
            this.navigatePath = this.GetPath(url);
            this.navigateQuery = this.GetQuery(url);
        }

        void IBrowser.Navigate(string path, string query)
        {
            this.navigatePath = path;
            this.navigateQuery = query;
        }

        void IBrowser.Click(Control control)
        {
            var tester = this.GetTester(control.GetType());
            tester.Click(control, this);
        }

        void IBrowser.Set(ITextControl textControl, string text)
        {
            var tester = this.GetTester(textControl.GetType());
            tester.Set(this, textControl, text);
        }

        void IBrowser.Set(ListControl listControl, int index)
        {
            var tester = this.GetTester(listControl.GetType());
            tester.Set(this, listControl, index);
        }

        void IBrowser.Set(ICheckBoxControl checkBox, bool value)
        {
            var tester = this.GetTester(checkBox.GetType());
            tester.Set(this, checkBox, value);
        }

        void IBrowser.AutoPostBack(Control control)
        {
            this.autoPostBackControl = control;
            var tester = this.GetTester(control.GetType());
            tester.AutoPostBack(control, this);
        }

        void IBrowser.AutoPostBack(Control control, int index)
        {
            this.autoPostBackControl = control;
            var tester = this.GetTester(control.GetType());
            tester.AutoPostBack(control, this, index);
        }

        void IBrowser.AutoPostBackByText(Control control, string text)
        {
            var listControl = (ListControl)control;
            var listItem = listControl.Items.FindByText(text);
            var index = listControl.Items.IndexOf(listItem);
            this.Browser.AutoPostBack(listControl, index);
        }

        void IBrowser.AutoPostBackByValue(Control control, string value)
        {
            var listControl = (ListControl)control;
            var listItem = listControl.Items.FindByValue(value);
            var index = listControl.Items.IndexOf(listItem);
            this.Browser.AutoPostBack(listControl, index);
        }

        public void SetTester(Type controlType, ITester tester)
        {
            if (tester == null)
            {
                if (this.testerByType.ContainsKey(controlType))
                {
                    this.testerByType.Remove(controlType);
                }
            }
            else
            {
                this.testerByType[controlType] = tester;
            }
        }

        public ITester GetTester(Type controlType)
        {
            if (this.testerByType.ContainsKey(controlType))
            {
                return this.testerByType[controlType];
            }

            return DefaultTester;
        }
        
        public virtual void Setup()
        {
            HttpModule.ApplicationLifeCycleEventHandler += this.ApplicationEventOccured;
            HttpModule.PageLifeCycleEventHandler += this.PageEventOccured;
        }

        public virtual void TearDown()
        {
            HttpModule.PageLifeCycleEventHandler -= this.PageEventOccured;
            HttpModule.ApplicationLifeCycleEventHandler -= this.ApplicationEventOccured;
        }

        public virtual void Test()
        {
            if (this.Login != null)
            {
                this.loginInProgress = true;
                try
                {
                    if (this.LoginPath != null)
                    {
                        this.requestCount = -1;
                        this.Get(this.LoginPath);
                    }
                    else
                    {
                        this.requestCount = -1;
                        this.Get("Local/Login.aspx");
                        if (this.Response.StatusCode != 200)
                        {
                            this.requestCount = -1;
                            this.Get(FormsAuthentication.LoginUrl);
                        }
                    }
                }
                finally
                {
                    this.loginInProgress = false;
                }
            }

            this.expectedRequestCount = this.GetMaxRequestCount();
            this.OnTest();

            if (this.requestCount < this.expectedRequestCount)
            {
                throw new Exception("Not all request were handled.\n");
            }
        }

        protected virtual void OnTest()
        {
            if (this.expectedRequestCount > 0)
            {
                for (var i = 1; i <= this.expectedRequestCount; i++)
                {
                    if (!string.IsNullOrEmpty(this.navigatePath))
                    {
                        var path = this.navigatePath;
                        var query = this.navigateQuery;

                        this.navigatePath = null;
                        this.navigateQuery = null;
 
                        if (string.IsNullOrEmpty(query))
                        {
                            this.Get(path);
                        }
                        else
                        {
                            this.Get(path, query);
                        }
                    }
                    else
                    {
                        if (i == 1)
                        {
                            if (string.IsNullOrEmpty(this.InitialQuery))
                            {
                                this.Get(this.InitialPath);
                            }
                            else
                            {
                                this.Get(this.InitialPath, this.InitialQuery);
                            }
                        }
                        else
                        {
                            var location = this.Response.GetHeader(HttpWorkerRequest.HeaderLocation);
                            if (!string.IsNullOrEmpty(location))
                            {
                                this.Get(location);
                            }
                            else
                            {
                                this.Post(this.Response.Path, this.Response.Query);
                            }
                        }
                    }
                }

                if (!this.Response.StatusCode.Equals(this.ExpectedStatusCode))
                {
                    throw new Exception("Status code: " + this.Response.StatusCode);
                }
            }
        }

        protected virtual void OnRequest(WorkerRequest request)
        {
        }

        protected void Get(string url)
        {
            this.Get(this.GetPath(url), this.GetQuery(url));
        }

        protected void Get(string path, string query)
        {
            var request = new WorkerRequest(path, query);
            this.Response = request.Response;
            this.requestCount = this.RequestCount + 1;
            this.OnRequest(request);

            this.postDataByControlUniqueId = new Dictionary<string, string>();
            HttpRuntime.ProcessRequest(request);
            this.UpdateAspNetHiddenFields();

            var method = this.methods.GetAfterMethodInfo(this.RequestCount);
            if (method != null)
            {
                method.Invoke(this, null);
            }

            if (this.exception != null)
            {
                this.LogMessage(this.exception);
                var rethrowException = this.exception;
                while (rethrowException.InnerException != null)
                {
                    rethrowException = rethrowException.InnerException;
                }

                throw rethrowException;
            }
        }
        
        protected void Post(string url)
        {
            this.Post(this.GetPath(url), this.GetQuery(url));
        }

        protected void Post(string path, string query)
        {
            var postDataStringBuilder = new StringBuilder();
            var requiresSeparator = false;
            foreach (var postData in this.postDataByControlUniqueId)
            {
                if (requiresSeparator)
                {
                    postDataStringBuilder.Append(Separator);
                }
                else
                {
                    requiresSeparator = true;
                }

                var key = HttpUtility.UrlEncode(postData.Key);
                var value = HttpUtility.UrlEncode(postData.Value);

                postDataStringBuilder.Append(key);
                postDataStringBuilder.Append(Equal);
                postDataStringBuilder.Append(value);
            }

            var request = new WorkerRequest(path, query);
            this.Response = request.Response;
            this.requestCount = this.RequestCount + 1;
            this.OnRequest(request);
            request.PostData = Encoding.UTF8.GetBytes(postDataStringBuilder.ToString());
            this.postDataByControlUniqueId = new Dictionary<string, string>();
            
            HttpRuntime.ProcessRequest(request);

            this.UpdateAspNetHiddenFields();

            if (this.exception != null)
            {
                if (this.exception.InnerException != null)
                {
                    this.LogMessage(this.exception.InnerException);
                    throw this.exception.InnerException;
                }

                this.LogMessage(this.exception);
                throw this.exception;
            }
        }

        protected void LogMessage(string message)
        {
            if (this.Logger != null)
            {
                this.Logger.WriteLine(message);
            }
        }

        protected void LogMessage(Exception exceptionToLog)
        {
            if (this.Logger != null)
            {
                this.Logger.WriteLine(exceptionToLog.Message);
                this.Logger.WriteLine(exceptionToLog.StackTrace);
            }
        }

        /// <summary>
        /// Force Asp.Net to persist the ViewState of all controls that have ViewState enabled.
        /// Newer versions of Asp.Net optimize ViewState by not
        /// persisting ViewState of controls if the value is not used.
        /// If we attach an EventHandler then we disable this optimization.
        /// </summary>
        /// <param name="parentControl">The parent control.</param>
        /// TODO: Remove this, "POST" takes care of this now
        private static void ForceViewState(Control parentControl)
        {
            var textBox = parentControl as TextBox;
            if (textBox != null)
            {
                textBox.TextChanged += TextBoxTextChanged;
            }

            var checkBoxControl = parentControl as CheckBox;
            if (checkBoxControl != null)
            {
                checkBoxControl.CheckedChanged += CheckBoxControlCheckedChanged;
            }

            var listControl = parentControl as ListControl;
            if (listControl != null)
            {
                listControl.SelectedIndexChanged += ListControlSelectedIndexChanged;
            }

            foreach (Control childControl in parentControl.Controls)
            {
                ForceViewState(childControl);
            }
        }

        #region Dummy event handlers
        private static void CheckBoxControlCheckedChanged(object sender, EventArgs e)
        {
        }

        private static void TextBoxTextChanged(object sender, EventArgs e)
        {
        }

        private static void ListControlSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        #endregion

        private void PageEventOccured(object sender, PageLifeCycleEventArgs args)
        {
            try
            {
                if (this.RequestCount == 0)
                {
                    if (this.Login != null && args.PageLifeCycleEvent.Equals(PageLifeCycleEvent.OnLoadComplete))
                    {
                        FormsAuthentication.SetAuthCookie(this.Login, false);
                    }

                    return;
                }
                
                this.page = (Page)sender;

                if (args.PageLifeCycleEvent == PageLifeCycleEvent.OnPreRenderComplete)
                {
                    this.SavePostData(this.page);
                    this.autoPostBackControl = null;
                    ForceViewState(this.page);
                }

                var method = this.methods.GetMethodInfo(args, this.RequestCount);
                if (method != null)
                {
                    method.Invoke(this, null);
                }
            }
            catch (Exception e)
            {
                this.exception = e;
                throw;
            }
        }

        private void ApplicationEventOccured(object sender, ApplicationLifeCycleEventArgs args)
        {
            try
            {
                var httpApplication = (HttpApplication)sender;

                if (args.ApplicationLifeCycleEvent == ApplicationLifeCycleEvent.BeginRequest)
                {
                    foreach (var cookie in this.cookies.Values)
                    {
                        httpApplication.Request.Cookies.Add(cookie);
                    }
                }

                if (args.ApplicationLifeCycleEvent == ApplicationLifeCycleEvent.EndRequest)
                {
                    this.cookies = new Dictionary<string, HttpCookie>();

                    // first the request _cookies
                    foreach (var httpRequestCookieName in httpApplication.Request.Cookies.AllKeys)
                    {
                        this.cookies[httpRequestCookieName] = httpApplication.Request.Cookies[httpRequestCookieName];
                    }

                    // then the response _cookies
                    foreach (var httpResponseCookieName in httpApplication.Response.Cookies.AllKeys)
                    {
                        this.cookies[httpResponseCookieName] = httpApplication.Response.Cookies[httpResponseCookieName];
                    }
                }

                var method = this.methods.GetMethodInfo(args, this.RequestCount);
                if (method != null)
                {
                    method.Invoke(this, null);
                }
            }
            catch (Exception e)
            {
                this.LogMessage(e);
                this.exception = e;
                throw;
            }
        }

        private void UpdateAspNetHiddenFields()
        {
            var html = this.Response.Html;
            if (!string.IsNullOrEmpty(html))
            {
                if (html.Contains(ViewStateEncryptedKey))
                {
                    this.postDataByControlUniqueId[ViewStateEncryptedKey] = string.Empty;
                }

                var viewStateMatch = ViewStateRegex.Match(html);
                var viewState = viewStateMatch.Groups[1].Value;
                this.postDataByControlUniqueId[ViewStateKey] = viewState;

                var eventValidationMatch = EventValidationRegex.Match(html);
                var eventValidation = eventValidationMatch.Groups[1].Value;
                this.postDataByControlUniqueId[EventvalidationKey] = eventValidation;

                if (!this.loginInProgress && this.CheckForErrorsInPage)
                {
                    if (Regex.IsMatch(html, @"\[.*Exception\]"))
                    {
                        this.LogMessage(html);
                        throw new Exception("Exception in page\n" + html);
                    }
                }
            }
        }

        private void SavePostData(Control control)
        {
            if (control is IPostBackDataHandler)
            {
                this.GetTester(control.GetType()).SavePostData(this.postDataByControlUniqueId, control, this.autoPostBackControl);
            }

            foreach (Control childControl in control.Controls)
            {
                this.SavePostData(childControl);
            }
        }

        private int GetMaxRequestCount()
        {
            var methods = this.GetType().GetMethods();

            var maxRequestCount = 0;

            foreach (var methodInfo in methods)
            {
                var attributes = methodInfo.GetCustomAttributes(typeof(LifeCycleEventAttribute), false);
                foreach (var attribute in attributes)
                {
                    var lifeCycleEventAttribute = (LifeCycleEventAttribute)attribute;

                    if (lifeCycleEventAttribute.RequestCount > maxRequestCount)
                    {
                        maxRequestCount = lifeCycleEventAttribute.RequestCount;
                    }
                }
            }

            return maxRequestCount;
        }

        private string GetPath(string url)
        {
            var index = url.IndexOf("?", StringComparison.Ordinal);
            if (index >= 0)
            {
                return url.Substring(0, index);
            }

            return url;
        }

        private string GetQuery(string url)
        {
            var index = url.IndexOf("?", StringComparison.Ordinal);
            if (index >= 0)
            {
                return url.Substring(index + 1);
            }

            return null;
        }
    }
}