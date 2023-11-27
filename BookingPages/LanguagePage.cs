using OpenQA.Selenium;
using Utilities;

namespace BookingPages
{
    public class LanguagePage
    {
        private readonly IWebDriver _driver;
        private By LanguageElement => By.XPath("//*[text()='Nederlands']");
        private IWebElement LanguagePictureButton => _driver.FindElement(By.XPath("//*[@data-testid='header-language-picker-trigger']"));

        public LanguagePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickLanguageButton() => LanguagePictureButton.Click();

        public string GetButtonLanguageName(string language)
        {
            var buttonLanguage = LanguagePictureButton.GetAttribute("aria-label");
            return buttonLanguage;
        }

        public void SelectLanguge()
        {
            var languageElement = _driver.WaitForElementClicable(LanguageElement);
            languageElement.Click();
        }
    }
}
