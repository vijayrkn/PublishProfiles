using PublishProfileContracts;

namespace PublishProfileManager.Models
{
    public class FileSystemPublishProfile : PublishProfile, IFileSystemPublishProfile
    {

        public const string PublishMethod = "FileSystem";
        public FileSystemPublishProfile()
            : base(PublishMethod)
        {
            DeleteExistingFiles = false;
        }

        public string PublishUrl { get; set; }

        public bool DeleteExistingFiles { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
