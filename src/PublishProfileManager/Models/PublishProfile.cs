using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Build.Evaluation;
using PublishProfileContracts;

namespace PublishProfileManager.Models
{
    public abstract class PublishProfile : PublishProfileBase, IPublishProfile
    {
        protected PublishProfile(string webPublishMethod)
        {
            WebPublishMethod = webPublishMethod;
            LastUsedBuildConfiguration = "Release";
            LastUsedPlatform = "Any CPU";
        }

        [Display(Order = -1)]
        public string WebPublishMethod { get; set; }

        [Display(Order = -1)]
        public string LastUsedBuildConfiguration { get; set; }

        [Display(Order = -1)]
        public string LastUsedPlatform { get; set; }

        [Display(Order = -1)]
        public string PublishFramework { get; set; }

        [Display(Order = -1)]
        public string RuntimeIdentifier { get; set; }
    }
}
