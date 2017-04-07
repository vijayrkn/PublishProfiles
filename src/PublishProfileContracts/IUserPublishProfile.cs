using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishProfileContracts
{
    public interface IUserPublishProfile: IPublishProfile
    {
        string EncryptedPassword { get; }
    }
}
