using OpenQA.Selenium;
using Utilities;

namespace BookingPages
{
    public class LanguagePage
    {
        private readonly IWebDriver _driver;
        private IWebElement LanguagePictureButton => _driver.WaitForElementClicable(By.XPath("//*[@data-testid='header-language-picker-trigger']"));
        private IWebElement LanguageElement => _driver.WaitForElementVisible(By.XPath("//*[text()='Nederlands']"));
        
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
