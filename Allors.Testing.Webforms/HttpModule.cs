// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpModule.cs" company="allors bvba">
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
    using System.Web;
    using System.Web.UI;

    public class HttpModule : IHttpModule
    {
        public static PageLifeCycleEventHandler PageLifeCycleEventHandler { get; set; }

        public static ApplicationLifeCycleEventHandler ApplicationLifeCycleEventHandler { get; set; }

        public void Init(HttpApplication application)
        {
            // Application level
            application.AcquireRequestState += this.ApplicationAcquireRequestState;
            application.AuthenticateRequest += this.ApplicationAuthenticateRequest;
            application.AuthorizeRequest += this.ApplicationAuthorizeRequest;
            application.BeginRequest += this.ApplicationBeginRequest;
            application.Disposed += this.ApplicationDisposed;
            application.EndRequest += this.ApplicationEndRequest;
            application.Error += this.ApplicationError;
            application.PostAcquireRequestState += this.ApplicationPostAcquireRequestState;
            application.PostAuthenticateRequest += this.ApplicationPostAuthenticateRequest;
            application.PostAuthorizeRequest += this.ApplicationPostAuthorizeRequest;
            application.PostMapRequestHandler += this.ApplicationPostMapRequestHandler;
            application.PostReleaseRequestState += this.ApplicationPostReleaseRequestState;
            application.PostRequestHandlerExecute += this.ApplicationPostRequestHandlerExecute;
            application.PostResolveRequestCache += this.ApplicationPostResolveRequestCache;
            application.PostUpdateRequestCache += this.ApplicationPostUpdateRequestCache;
            application.PreRequestHandlerExecute += this.ApplicationPreRequestHandlerExecute;
            application.PreSendRequestContent += this.ApplicationPreSendRequestContent;
            application.PreSendRequestHeaders += this.ApplicationPreSendRequestHeaders;
            application.ReleaseRequestState += this.ApplicationReleaseRequestState;
            application.ResolveRequestCache += this.ApplicationResolveRequestCache;
            application.UpdateRequestCache += this.ApplicationUpdateRequestCache;

            /*
            // Following events require an IIS pipeline.
            application.LogRequest += new EventHandler(application_LogRequest);
            application.MapRequestHandler += new EventHandler(application_MapRequestHandler);
            application.PostLogRequest += new EventHandler(application_PostLogRequest);
*/
        }

        public void Dispose()
        {
        }

        private void OnApplicationLifeCycleEvent(object sender, ApplicationLifeCycleEvent applicationLifeCycleEvent)
        {
            try
            {
                var applicationLifeCycleEventHandler = ApplicationLifeCycleEventHandler;
                if (applicationLifeCycleEventHandler != null)
                {
                    applicationLifeCycleEventHandler(sender, new ApplicationLifeCycleEventArgs(applicationLifeCycleEvent));
                }
            }
            catch
            {
            }
        }

        private void OnPageLifeCycleEvent(object sender, PageLifeCycleEvent pageLifeCycleEvent)
        {
            try
            {
                var pageLifeCycleLifeCycleEventHandler = PageLifeCycleEventHandler;
                if (pageLifeCycleLifeCycleEventHandler != null)
                {
                    pageLifeCycleLifeCycleEventHandler(sender, new PageLifeCycleEventArgs(pageLifeCycleEvent));
                }
            }
            catch
            {
            }
        }

        private void RegisterPageHandlers(Page page)
        {
            page.AbortTransaction += this.PageAbortTransaction; // Raises the AbortTransaction event. (Inherited from TemplateControl.) 
            page.CommitTransaction += this.PageCommitTransaction;  // Raises the CommitTransaction event. (Inherited from TemplateControl.) 
            page.DataBinding += this.PageDataBinding; // Raises the DataBinding event. (Inherited from Control.) 
            page.Error += this.PageError; // Raises the Error event. (Inherited from TemplateControl.) 
            page.Init += this.PageInit; // Raises the Init event to initialize the page. (Overrides Control..::.OnInit(EventArgs).) 
            page.InitComplete += this.PageInitComplete; // Raises the InitComplete event after page initialization. 
            page.Load += this.PageLoad; // Raises the Load event. (Inherited from Control.) 
            page.LoadComplete += this.PageLoadComplete; // Raises the LoadComplete event at the end of the page load stage. 
            page.PreInit += this.PagePreInit; // Raises the PreInit event at the beginning of page initialization. 
            page.PreLoad += this.PagePreLoad; // Raises the PreLoad event after postback data is loaded into the page server controls but before the OnLoad event.  
            page.PreRender += this.PagePreRender; // Raises the PreRender event. (Inherited from Control.) 
            page.PreRenderComplete += this.PagePreRenderComplete; // Raises the PreRenderComplete event after the OnPreRenderComplete event and before the page is rendered. 
            page.SaveStateComplete += this.PageSaveStateComplete; // Raises the SaveStateComplete event after the page state has been saved to the persistence medium. 
            page.Unload += this.PageUnload; // Raises the Unload event. (Inherited from Control.) 
        }

        private void ApplicationUpdateRequestCache(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.UpdateRequestCache);
        }

        private void ApplicationResolveRequestCache(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.ResolveRequestCache);
        }

        private void ApplicationReleaseRequestState(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.ReleaseRequestState);
        }

        private void ApplicationPreSendRequestHeaders(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PreSendRequestHeaders);
        }

        private void ApplicationPreSendRequestContent(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PreSendRequestContent);
        }

        private void ApplicationPreRequestHandlerExecute(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PreRequestHandlerExecute);

            var application = (HttpApplication)sender;
            var page = application.Context.Handler as Page;
            if (page != null)
            {
                this.RegisterPageHandlers(page);
            }
        }

        private void ApplicationPostUpdateRequestCache(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PostUpdateRequestCache);
        }

        private void ApplicationPostResolveRequestCache(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PostResolveRequestCache);
        }

        private void ApplicationPostRequestHandlerExecute(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PostRequestHandlerExecute);
        }

        private void ApplicationPostReleaseRequestState(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PostReleaseRequestState);
        }

        private void ApplicationPostMapRequestHandler(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PostMapRequestHandler);
        }

        private void ApplicationPostAuthorizeRequest(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PostAuthorizeRequest);
        }

        private void ApplicationPostAuthenticateRequest(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PostAuthenticateRequest);
        }

        private void ApplicationPostAcquireRequestState(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.PostAcquireRequestState);
        }

        private void ApplicationError(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.Error);
        }

        private void ApplicationEndRequest(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.EndRequest);
        }

        private void ApplicationDisposed(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.Disposed);
        }

        private void ApplicationAuthorizeRequest(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.AuthorizeRequest);
        }

        private void ApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.AuthenticateRequest);
        }

        private void ApplicationAcquireRequestState(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.AcquireRequestState);
        }

        private void ApplicationBeginRequest(object sender, EventArgs e)
        {
            this.OnApplicationLifeCycleEvent(sender, ApplicationLifeCycleEvent.BeginRequest);
        }

        private void PageAbortTransaction(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnAbortTransaction);
        }

        private void PageCommitTransaction(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnCommitTransaction);
        }

        private void PageDataBinding(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnDataBinding);
        }

        private void PageError(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnError);
        }

        private void PageInit(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnInit);
        }

        private void PageInitComplete(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnInitComplete);
        }

        private void PageLoad(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnLoad);
        }

        private void PageLoadComplete(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnLoadComplete);
        }

        private void PagePreInit(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnPreInit);
        }

        private void PagePreLoad(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnPreLoad);
        }

        private void PagePreRender(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnPreRender);
        }

        private void PagePreRenderComplete(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnPreRenderComplete);
        }

        private void PageSaveStateComplete(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnSaveStateComplete);
        }

        private void PageUnload(object sender, EventArgs e)
        {
            this.OnPageLifeCycleEvent(sender, PageLifeCycleEvent.OnUnload);
        }
    }
}