namespace PublishProfileContracts
{
    public interface IMSDeployPackagePublishProfile: IProjectPublishProfile
    {
        string PackageLocation { get; }
        string DeployIisAppPath { get; }
    }
}
