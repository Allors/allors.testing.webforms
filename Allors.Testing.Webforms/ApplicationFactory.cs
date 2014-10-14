// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationFactory.cs" company="allors bvba">
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
    using System.Threading;
    using System.Web.Hosting;
    using System.Xml;

    public class ApplicationFactory
    {
        private readonly Logger logger;

        private DirectoryInfo destinationBinDirectory;
        private DirectoryInfo destinationDirectory;

        public ApplicationFactory(DirectoryInfo sourceDirectory, string virtualDirectory)
            : this(sourceDirectory, virtualDirectory, null)
        {
        }

        public ApplicationFactory(DirectoryInfo sourceDirectory, string virtualDirectory, Logger logger)
        {
            this.logger = logger;
            this.SourceDirectory = sourceDirectory;
            this.VirtualDirectory = virtualDirectory;

            this.CurrentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            this.AssemblyFiles = new List<FileInfo>();
            this.AssemblyFiles.AddRange(this.CurrentDirectory.GetFiles("*.dll"));

            this.IgnoreDirectories = new List<string>
                                         {
                                             ".svn", ".cvs"
                                         };

            this.DestinationDirectory = new DirectoryInfo(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Web");
        }

        public DirectoryInfo SourceDirectory { get; private set; }

        public string VirtualDirectory { get; private set; }

        public DirectoryInfo CurrentDirectory { get; private set; }

        public List<FileInfo> AssemblyFiles { get; private set; }

        public List<string> IgnoreDirectories { get; private set; }

        public DirectoryInfo DestinationDirectory
        {
            get
            {
                return this.destinationDirectory;
            }

            set
            {
                this.destinationDirectory = value;
                this.destinationBinDirectory = new DirectoryInfo(this.DestinationDirectory.FullName + Path.DirectorySeparatorChar + "bin");
            }
        }

        public static void CopyRecursive(DirectoryInfo sourceDirectory, DirectoryInfo destinationDirectory, ICollection<string> ignoreDirectories)
        {
            if (destinationDirectory.Exists)
            {
                foreach (var destinationFile in destinationDirectory.GetFiles())
                {
                    if (destinationFile.Exists)
                    {
                        destinationFile.Delete();
                    }
                }
            }

            foreach (var sourceChildDirectory in sourceDirectory.GetDirectories())
            {
                if (!ignoreDirectories.Contains(sourceChildDirectory.Name))
                {
                    var destinationChildDirectory = new DirectoryInfo(destinationDirectory.FullName + Path.DirectorySeparatorChar + sourceChildDirectory.Name);
                    for (var retryCounter = 0; retryCounter < 10; retryCounter++)
                    {
                        try
                        {
                            if (destinationChildDirectory.Exists)
                            {
                                destinationChildDirectory.Delete(true);
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            Thread.Sleep(1000);
                            destinationChildDirectory.Refresh();
                        }
                    }

                    destinationChildDirectory.Create();
                    CopyRecursive(sourceChildDirectory, destinationChildDirectory, ignoreDirectories);
                }
            }

            foreach (var sourceFile in sourceDirectory.GetFiles())
            {
                var destinationFile = new FileInfo(destinationDirectory.FullName + Path.DirectorySeparatorChar + sourceFile.Name);
                sourceFile.CopyTo(destinationFile.FullName);
            }
        }

        public Application Create()
        {
            CopyRecursive(this.SourceDirectory, this.DestinationDirectory, this.IgnoreDirectories);

            foreach (var fromDll in this.AssemblyFiles)
            {
                var toDll = new FileInfo(this.destinationBinDirectory.FullName + Path.DirectorySeparatorChar + fromDll.Name);
                if (!toDll.Exists)
                {
                    fromDll.CopyTo(toDll.FullName, true);
                }
            }

            var webConfigFileInfo = new FileInfo(this.DestinationDirectory.FullName + Path.DirectorySeparatorChar + "web.config");
            var webConfig = new XmlDocument();
            webConfig.Load(webConfigFileInfo.FullName);

            var httpModules = (XmlElement)webConfig.SelectSingleNode("/*/*[local-name()='system.web']/*[local-name()='httpModules']");

            if (httpModules == null)
            {
                var systemWeb = (XmlElement)webConfig.SelectSingleNode("/*/*[local-name()='system.web']");
                if (systemWeb == null)
                {
                    throw new Exception("System.Web element is missing in web.config");
                }

                httpModules = webConfig.CreateElement(systemWeb.Prefix, "httpModules", systemWeb.NamespaceURI);
                systemWeb.AppendChild(httpModules);
            }

            var add = webConfig.CreateElement(httpModules.Prefix, "add", httpModules.NamespaceURI);
            add.SetAttribute("name", "AllorsTestingModule");
            add.SetAttribute("type", "Allors.Testing.Webforms.HttpModule, Allors.Testing.Webforms");

            httpModules.AppendChild(add);

            var pages = (XmlElement)webConfig.SelectSingleNode("/*/*[local-name()='system.web']/*[local-name()='pages']");
            if (pages != null)
            {
                pages.SetAttribute("enableEventValidation", "false");
            }
            else
            {
                throw new Exception("pages element is missing in web.config");
            }
            
            webConfig.Save(webConfigFileInfo.FullName);

            var application = (Application)ApplicationHost.CreateApplicationHost(typeof(Application), this.VirtualDirectory, this.DestinationDirectory.FullName + "\\");

            application.Logger = this.logger;

            return application;
        }
    }
}