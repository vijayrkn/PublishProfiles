using System.Collections.Generic;
using PublishProfileContracts;

namespace PublishProfileManager.Models
{
    public class MSDeployPackagePublishProfile : PublishProfile, IMSDeployPackagePublishProfile
    {
        public const string PublishMethod = "Package";
        public MSDeployPackagePublishProfile()
            : base(PublishMethod)
        {
        }

        public string PackageLocation { get; set; }
        public string DeployIisAppPath { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
