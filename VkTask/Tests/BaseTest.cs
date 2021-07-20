using Aquality.Selenium.Browsers;
using NUnit.Framework;

namespace VkTask.Tests
{
    [TestFixture]
    public class BaseTest
    {
        [SetUp]
        public void Before()
        {
            AqualityServices.Browser.Maximize();
        }

        [TearDown]
        public void CleanUp()
        {
            if (AqualityServices.IsBrowserStarted)
            {
                AqualityServices.Browser.Quit();
            }
        }
    }
}
