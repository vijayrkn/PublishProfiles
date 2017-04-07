namespace PublishProfileContracts
{
    public interface IProjectPublishProfile : IPublishProfile
    {
        string WebPublishMethod { get; }

        string LastUsedBuildConfiguration { get; }

        string LastUsedPlatform { get; }

        string PublishFramework { get; }

        string RuntimeIdentifier { get; }
    }
}
