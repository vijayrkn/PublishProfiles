namespace PublishProfileContracts
{
    public interface IFileSystemPublishProfile: IProjectPublishProfile
    {
        string PublishUrl { get; }
        bool DeleteExistingFiles { get; }
    }
}
