using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Utilities;

namespace BookingPages
{
    public class LanguagePage
    {
        private readonly IWebDriver _driver;
        private By LanguageElements => By.XPath("//*[@class='cf67405157'and text()]");
        private IWebElement LanguagePictureButton => _driver.FindElement(By.XPath("//*[@data-testid='header-language-picker-trigger']"));

        public LanguagePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickLanguageButton() => LanguagePictureButton.Click();

        public string GetButtonLanguageName()
        {
            var buttonLanguage = LanguagePictureButton.GetAttribute("aria-label");
            return buttonLanguage;
        }

        public void SelectLanguge(string language)
        {
            var languageElements = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(LanguageElements));
            var selectedLanguage = languageElements.FirstOrDefault(x => x.Text.Contains(language));
            selectedLanguage.Click();
        }
    }
}
