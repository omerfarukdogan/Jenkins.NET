﻿using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsAction
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        public IEnumerable<JenkinsParameter> Parameters => Node.WrapGroup("parameter", n => new JenkinsParameter(n));

        public string Class => Node?.TryGetValue<string>("@_class");


        internal JenkinsAction(XNode node)
        {
            this.Node = node;
        }
    }
}
