using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublishProfileContracts;

namespace PublishProfileManager.Models
{
    public class WebJobPublishProfile : MSDeployPublishProfile, IWebJobPublishProfile
    {
        public string WebJobType { get; set; }

        public string WebJobName { get; set; }
    }
}
