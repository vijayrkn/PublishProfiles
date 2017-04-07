using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using PublishProfileContracts;
using PublishProfileManager.Models;

namespace PublishProfileManager
{
    public class PublishProfileCreatorFactory
    {
        public static KeyValuePair<string, IMSDeployPublishProfile> CreateMSDeployPublishProfileFromPublishSettings(string publishSettingsContents)
        {
            using (XmlTextReader reader = new XmlTextReader(new StringReader(publishSettingsContents)))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(reader);

                XmlNode root = xmldoc.DocumentElement;
                XmlNode profileNode = root.FirstChild;
                for (; profileNode != null; profileNode = profileNode.NextSibling)
                {
                    XmlAttribute publishMethodAttr = null;
                    if (profileNode.Attributes != null)
                    {
                        publishMethodAttr = profileNode.Attributes["publishMethod"];
                    }

                    if (publishMethodAttr != null && !string.IsNullOrEmpty(publishMethodAttr.Value) && string.Equals(publishMethodAttr.Value, MSDeployPublishProfile.PublishMethod, StringComparison.Ordinal))
                    {
                        IMSDeployPublishProfile msDeployPublishProfile = new MSDeployPublishProfile()
                        {
                            
                            MSDeployServiceURL = profileNode.Attributes["publishUrl"]?.Value,
                            DeployIisAppPath = profileNode.Attributes["msdeploySite"]?.Value,
                            SiteUrlToLaunchAfterPublish = profileNode.Attributes["destinationAppUrl"]?.Value,
                            LaunchSiteAfterPublish = true,
                            UserName = profileNode.Attributes["userName"]?.Value
                        };

                        string profileName = profileNode.Attributes["profileName"]?.Value ?? "msDeployProfile";
                        return new KeyValuePair<string, IMSDeployPublishProfile>(profileName, msDeployPublishProfile);
                    }
                }
            }

            return new KeyValuePair<string, IMSDeployPublishProfile>();
        }

        public static KeyValuePair<string, IUserPublishProfile> CreateUserPublishProfileFromPublishSettings(string publishSettingsContents)
        {
            using (XmlTextReader reader = new XmlTextReader(new StringReader(publishSettingsContents)))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(reader);

                XmlNode root = xmldoc.DocumentElement;
                XmlNode profileNode = root.FirstChild;
                for (; profileNode != null; profileNode = profileNode.NextSibling)
                {
                    XmlAttribute plainTextPassword = null;
                    if (profileNode.Attributes != null)
                    {
                        plainTextPassword = profileNode.Attributes["userPWD"];
                    }

                    if (plainTextPassword != null && !string.IsNullOrEmpty(plainTextPassword.Value))
                    {
                        var protectedPassword = DataProtection.GetProtectedPassword(plainTextPassword.Value);
                        IUserPublishProfile userPublishProfile = new UserPublishProfile()
                        {
                            EncryptedPassword = Convert.ToBase64String(protectedPassword)
                        };

                        string profileName = profileNode.Attributes["profileName"]?.Value ?? "msDeployProfile";
                        return new KeyValuePair<string, IUserPublishProfile>(profileName, userPublishProfile);
                    }
                }
            }

            return new KeyValuePair<string, IUserPublishProfile>();
        }

        public static KeyValuePair<string, IWebJobPublishProfile> CreateWebJobsPublishProfileFromPublishSettings(string publishSettingsContents, string webJobName, string webJobType)
        {
            var msdeployProfile = CreateMSDeployPublishProfileFromPublishSettings(publishSettingsContents);
            var msDeployPublishModel = msdeployProfile.Value;

            WebJobPublishProfile profile = new WebJobPublishProfile();
            profile.LoadModel(msDeployPublishModel.ToString());

            profile.WebJobName = webJobName;
            profile.WebJobType = webJobType;

            return new KeyValuePair<string, IWebJobPublishProfile>(msdeployProfile.Key, profile); 
        }
    }
}
