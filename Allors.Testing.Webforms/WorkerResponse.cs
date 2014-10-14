// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerResponse.cs" company="allors bvba">
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
    using System.IO;
    using System.Text;
    using System.Web;

    public class WorkerResponse 
    {
        private readonly Dictionary<int, string> headerByIndex;
        private readonly List<byte> content;

        internal WorkerResponse(string path, string query)
        {
            this.Query = query;
            this.Path = path;

            this.headerByIndex = new Dictionary<int, string>();
            this.content = new List<byte>();
        }

        public string Path { get; private set; }

        public string Query { get; private set; }

        public byte[] Content
        {
            get
            {
                return this.content.ToArray();
            }
        }

        public string Html
        {
            get
            {
                var headerContentType = this.GetHeader(HttpWorkerRequest.HeaderContentType);
                if (headerContentType != null && headerContentType.ToLower().Contains("html"))
                {
                    // TODO: use encoding from header
                    return new string(Encoding.Default.GetChars(this.Content, 0, this.Content.Length));
                }

                return null;
            }
        }

        public int StatusCode { get; private set; }

        public string GetHeader(int httpWorkerRequestIndex)
        {
            string value;
            this.headerByIndex.TryGetValue(httpWorkerRequestIndex, out value);
            return value;
        }

        internal void SendKnownResponseHeader(int index, string value)
        {
            this.headerByIndex[index] = value;
        }

        internal void SendResponseFromFile(string filename, long offset, long length)
        {
            var fileContent = File.ReadAllBytes(filename);
            if (offset == 0 && fileContent.LongLength.Equals(length))
            {
                this.content.AddRange(fileContent);
            }
            else
            {
                var range = new byte[length];
                Array.Copy(fileContent, offset, range, 0, length);
                this.content.AddRange(range);
            }
        }

        internal void SendResponseFromMemory(byte[] data, int length)
        {
            if (data.Length == length)
            {
                this.content.AddRange(data);
            }
            else
            {
                var range = new byte[length];
                Array.Copy(data, range, length);
                this.content.AddRange(range);
            }
        }

        internal void SendStatus(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
        }
    }
}