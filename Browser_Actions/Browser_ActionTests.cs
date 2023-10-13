using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace Browser_Actions
{
    [TestFixture]
    public class Browser_ActionTests : BaseTest_BA
    {
        [Test]
        public void OpenNewWindowHandleTabs()
        {
            string originalWindow = Driver.CurrentWindowHandle;
            Assert.AreEqual(Driver.WindowHandles.Count, 1);

            var elementMultipleWindows = Driver.FindElement(By.CssSelector("[href*='windows']"));
            elementMultipleWindows.Click();
          
            var elementClickHere = Driver.FindElement(By.CssSelector("[href*='windows']"));
            elementClickHere.Click();
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(wd => wd.WindowHandles.Count == 2);
           
            foreach (string window in Driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    Driver.SwitchTo().Window(window);
                    break;
                }
            }
            wait.Until(wd => wd.Url == $"{MainUrl}/windows/new");
            var elementContent = Driver.FindElement(By.ClassName("example"));
            Assert.That(elementContent.Displayed);
        }
    }
}
