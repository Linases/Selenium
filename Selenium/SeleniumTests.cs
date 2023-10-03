using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Selenium
{
    public class SeleniumTests
    {
        [Test]
        public void TestWebsiteTitle()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("")
            driver.Navigate().GoToUrl("https://www.example.com");

        }
    }
}