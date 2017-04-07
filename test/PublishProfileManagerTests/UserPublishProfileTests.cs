using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublishProfileManager.Models;
using PublishProfileManagerTests.Properties;
using Xunit;

namespace PublishProfileManagerTests
{
    public class UserPublishProfileTests
    {
        [Fact]
        public void UserPublishProfile_ReturnsValidPublishProfile()
        {
            string userPublishProfile = new UserPublishProfile().ToString();
            Assert.Equal(TestResources.userpublishprofile, userPublishProfile);
        }
    }
}
