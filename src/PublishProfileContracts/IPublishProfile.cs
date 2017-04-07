using System.Collections.Generic;

namespace PublishProfileContracts
{
    public interface IPublishProfile
    {
        IReadOnlyList<KeyValuePair<string, object>> GetProfileProperties();
    }
}