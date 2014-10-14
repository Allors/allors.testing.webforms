// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationLifeCycleEvent.cs" company="allors bvba">
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
    public enum ApplicationLifeCycleEvent
    {
	    AcquireRequestState, // 	Occurs when ASP.NET acquires the current state (for example, session state) that is associated with the current request.
	    AuthenticateRequest, // 	Occurs when a security module has established the identity of the user.
	    AuthorizeRequest, // Occurs when a security module has verified user authorization.
	    BeginRequest, // Occurs as the first event in the HTTP pipeline chain of execution when ASP.NET responds to a request.
	    Disposed, // Occurs when the application is disposed.
	    EndRequest, // Occurs as the last event in the HTTP pipeline chain of execution when ASP.NET responds to a request.
	    Error, // Occurs when an unhandled exception is thrown.
	    LogRequest, // Occurs just before ASP.NET performs any logging for the current request.
	    MapRequestHandler, // Infrastructure. Occurs when the handler is selected to respond to the request.
	    PostAcquireRequestState, // 	Occurs when the request state (for example, session state) that is associated with the current request has been obtained.
	    PostAuthenticateRequest, // 	Occurs when a security module has established the identity of the user.
	    PostAuthorizeRequest, // Occurs when the user for the current request has been authorized.
	    PostLogRequest, // Occurs when ASP.NET has completed processing all the event handlers for the LogRequest event.
	    PostMapRequestHandler, // Occurs when ASP.NET has mapped the current request to the appropriate event handler.
	    PostReleaseRequestState, // Occurs when ASP.NET has completed executing all request event handlers and the request state data has been stored.
	    PostRequestHandlerExecute, // Occurs when the ASP.NET event handler (for example, a page or an XML Web service) finishes execution.
	    PostResolveRequestCache, // 	Occurs when ASP.NET bypasses execution of the current event handler and allows a caching module to serve a request from the cache.
	    PostUpdateRequestCache, // Occurs when ASP.NET finishes updating caching modules and storing responses that are used to serve subsequent requests from the cache.
	    PreRequestHandlerExecute, // Occurs just before ASP.NET starts executing an event handler (for example, a page or an XML Web service).
	    PreSendRequestContent, // Occurs just before ASP.NET sends content to the client.
	    PreSendRequestHeaders, // Occurs just before ASP.NET sends HTTP headers to the client.
	    ReleaseRequestState, // 	Occurs after ASP.NET finishes executing all request event handlers. This event causes state modules to save the current state data.
        ResolveRequestCache, // Occurs when ASP.NET finishes an authorization event to let the caching modules serve requests from the cache, bypassing execution of the event handler (for example, a page or an XML Web service).
        UpdateRequestCache, // Occurs when ASP.NET finishes executing an event handler in order to let caching modules store responses that will be used to serve subsequent requests from the cache. 
    }
}