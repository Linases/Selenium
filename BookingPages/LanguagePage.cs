using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BookingPages
{
    public class LanguagePage
    {
        private readonly IWebDriver _driver;
        private IWebElement LanguagePictureButton => _driver.NoSuchElementExceptionWait(By.XPath("//*[@data-testid='header-language-picker-trigger']"),30,500);
        private IWebElement LanguageElement => _driver.NoSuchElementExceptionWait(By.XPath("//*[text()='Nederlands']"), 20,500);
        
        public LanguagePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickLanguageButton() => LanguagePictureButton.Click();

        public string GetButtonLanguageName(string language)
        {
            var buttonLanguage = LanguagePictureButton.GetAttribute("aria-label");
            if (buttonLanguage.Contains(language))
            {
            }
           return language;
        }

        public void SelectLanguge() => LanguageElement.Click();
    }
}
