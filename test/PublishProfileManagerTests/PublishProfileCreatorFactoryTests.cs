using System;
using System.IO;
using System.Xml;
using Microsoft.Build.Evaluation;
using PublishProfileManager;
using PublishProfileManager.Models;
using PublishProfileManagerTests.Properties;
using Xunit;

namespace PublishProfileManagerTests
{
    public class PublishProfileCreatorFactoryTests
    {
        [Fact]
        public void MsDeployProfile_FromPublishSettings()
        {
            var msDeployProfile = PublishProfileCreatorFactory.CreateMSDeployPublishProfileFromPublishSettings(TestResources.PublishSettings);
            Assert.Equal(TestResources.MsDeployFromPublishSettings, msDeployProfile.Value.ToString());
        }

        [Fact]
        public void UserProfile_ReturnsEncryptedUserProfile()
        {
            var userProfile = PublishProfileCreatorFactory.CreateUserPublishProfileFromPublishSettings(TestResources.PublishSettings);
            using (XmlTextReader reader = new XmlTextReader(new StringReader(userProfile.Value.ToString())))
            {
                Project userProject = new Project(reader);
                Assert.NotNull(userProject.GetProperty("EncryptedPassword").EvaluatedValue);
            }
        }

        [Fact]
        public void MsDeployProfile_ReturnsCorrectMsDeployPublishProfile()
        {
            MSDeployPublishProfile msDeployProfile = new MSDeployPublishProfile()
            {
                SiteUrlToLaunchAfterPublish = "https://webappwithdb.azurewebsites.net",
                LaunchSiteAfterPublish = true,
                MSDeployServiceURL = "webappwithdb.scm.azurewebsites.net:443",
                DeployIisAppPath = "webappwithdb",
                UserName = "$webappwithdb"
            };
            Assert.Equal(TestResources.MSDeployPublishProfile, msDeployProfile.ToString());
        }

        [Fact]
        public void MsDeployPackageProfile_ReturnsCorrectMsDeployPackagePublishProfile()
        {
            MSDeployPackagePublishProfile packageProfile = new MSDeployPackagePublishProfile()
            {
                PackageLocation = @"bin\Release\PackageOutput.zip",
                DeployIisAppPath=@"Default Web Site"
            };
            Assert.Equal(TestResources.MSDeployPackagePublishProfile, packageProfile.ToString());
        }

        [Fact]
        public void FileSystemProfile_ReturnsCorrectFileSystemProfile()
        {
            FileSystemPublishProfile fileSystemProfile = new FileSystemPublishProfile()
            {
                PublishUrl=@"bin\Release\Publish"
            };
            Assert.Equal(TestResources.FileSystemPublishProfile, fileSystemProfile.ToString());
        }

    }
}
