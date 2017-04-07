using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublishProfileContracts;

namespace PublishProfileContracts
{
    public interface IMSDeployPublishProfile: IAuthenticationPublishProfile, IProjectPublishProfile
    {
        string MSDeployServiceURL { get; }
        string DeployIisAppPath { get; }
        string SiteUrlToLaunchAfterPublish { get; }
        bool LaunchSiteAfterPublish { get; }
        bool SkipExtraFilesOnServer { get; }
        string MSDeployPublishMethod { get; }
        bool EnableMSDeployBackup { get; }
    }
}
