using PublishProfileContracts;

namespace PublishProfileManager.Models
{
    public class MSDeployPublishProfile : PublishProfile, IMSDeployPublishProfile
    {
        public const string PublishMethod = "MSDeploy";
        public MSDeployPublishProfile()
            : base(PublishMethod)
        {
            SkipExtraFilesOnServer = true;
            EnableMSDeployBackup = true;
            MSDeployPublishMethod = "WMSVC";
            _SavePWD = true;
        }

        public string SiteUrlToLaunchAfterPublish { get; set; }
        public bool LaunchSiteAfterPublish { get; set; }
        public string MSDeployServiceURL { get; set; }
        public string DeployIisAppPath { get; set; }
        public bool SkipExtraFilesOnServer { get; set; }
        public string MSDeployPublishMethod { get; set; }
        public bool EnableMSDeployBackup { get; set; }
        public string UserName { get; set; }
        public bool _SavePWD { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
