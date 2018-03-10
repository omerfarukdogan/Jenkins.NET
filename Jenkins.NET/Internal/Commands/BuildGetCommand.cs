﻿using JenkinsNET.Models;
using System;
using System.Xml.Linq;

namespace JenkinsNET.Internal.Commands
{
    internal class BuildGetCommand : JenkinsHttpCommand
    {
        public JenkinsBuild Result {get; private set;}


        public BuildGetCommand(IJenkinsContext context, string jobName, string buildNumber)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            if (string.IsNullOrEmpty(buildNumber))
                throw new ArgumentException("'buildNumber' cannot be empty!");

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, buildNumber, "api/xml");
            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnWrite = request => {
                request.Method = "POST";
            };

            OnRead = response => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var document = XDocument.Load(stream);
                    if (document.Root == null) throw new ApplicationException("An empty response was returned!");

                    Result = new JenkinsBuild(document.Root);
                }
            };
        }
    }
}
