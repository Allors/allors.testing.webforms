// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerRequest.cs" company="allors bvba">
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
    using System.Globalization;
    using System.Web;

    public class WorkerRequest : HttpWorkerRequest
    {
        private readonly string path;
        private readonly string query;

        private readonly Dictionary<int, string> headerByIndex;

        private readonly WorkerResponse response;

        public WorkerRequest(string path, string query)
        {
            this.path = string.IsNullOrEmpty(path) ? "/" : path.StartsWith("/") ? path : "/" + path;
            this.query = query;

            this.headerByIndex = new Dictionary<int, string>();

            this.response = new WorkerResponse(path, query);
        }

        public WorkerResponse Response
        {
            get
            {
                return this.response;
            }
        }

        public byte[] PostData { get; internal set; }

        public void SetHeader(int httpWorkerRequestIndex, string value)
        {
            this.headerByIndex[httpWorkerRequestIndex] = value;
        }

        public override void EndOfRequest()
        {
        }

        public override void FlushResponse(bool finalFlush)
        {
        }

        public override string GetHttpVersion()
        {
            return "HTTP/1.0";
        }
        
        public override string GetHttpVerbName()
        {
            if (this.PostData != null)
            {
                return "POST";
            }

            return "GET";
        }
        
        public override string GetLocalAddress()
        {
            return "127.0.0.1";
        }

        public override int GetLocalPort()
        {
            return 80;
        }

        public override string GetQueryString()
        {
            return this.query;
        }

        public override string GetRawUrl()
        {
            if (!string.IsNullOrEmpty(this.query))
            {
                return this.path + "?" + this.query;
            }
            
            return this.path;
        }

        public override string GetRemoteAddress()
        {
            return "127.0.0.1";
        }

        public override int GetRemotePort()
        {
            return 0;
        }

        public override string GetUriPath()
        {
            return this.path;
        }

        public override void SendKnownResponseHeader(int index, string value)
        {
            this.Response.SendKnownResponseHeader(index, value);
        }

        public override void SendResponseFromFile(string filename, long offset, long length)
        {
            this.response.SendResponseFromFile(filename, offset, length);
        }

        public override void SendResponseFromFile(IntPtr handle, long offset, long length)
        {
        }

        public override void SendResponseFromMemory(byte[] data, int length)
        {
            this.Response.SendResponseFromMemory(data, length);
        }

        public override void SendStatus(int statusCode, string statusDescription)
        {
            this.response.SendStatus(statusCode, statusDescription);
        }

        public override void SendUnknownResponseHeader(string name, string value)
        {
        }

        public override string GetKnownRequestHeader(int index)
        {
            string value;
            if (this.headerByIndex.TryGetValue(index, out value))
            {
                return value;
            }
            
            if (this.PostData != null)
            {
                switch (index)
                {
                    case HttpWorkerRequest.HeaderContentLength:
                        return this.PostData.Length.ToString(CultureInfo.InvariantCulture);
                    case HttpWorkerRequest.HeaderContentType:
                        return "application/x-www-form-urlencoded";
                }
            }

            return base.GetKnownRequestHeader(index);
        }

        public override byte[] GetPreloadedEntityBody()
        {
            if (this.PostData != null)
            {
                return this.PostData;
            }

            return base.GetPreloadedEntityBody();
        }
    }
}