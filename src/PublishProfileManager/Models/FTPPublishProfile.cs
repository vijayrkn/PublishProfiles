using System;
using PublishProfileContracts;

namespace PublishProfileManager.Models
{
    public class FTPPublishProfile : PublishProfile, IFTPPublishProfile
    {

        public const string PublishMethod = "FTP";
        public FTPPublishProfile()
            : base(PublishMethod)
        {
            DeleteExistingFiles = false;
        }

        public string PublishUrl { get; set; }
        public bool DeleteExistingFiles { get; set; }
        public bool FtpPassiveMode { get; set; } 
        public string FtpSitePath { get; set; }
        public bool LaunchSiteAfterPublish { get; set; }
        public string SiteUrlToLaunchAfterPublish { get; set; }
        public string UserName { get; set; }
        public bool _SavePWD { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
