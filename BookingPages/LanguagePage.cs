using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Wrappers;

namespace BookingPages
{
    public class LanguagePage
    {
        private readonly IWebDriver _driver;
        private Button _button = new Button();
        private ReadOnlyCollection<IWebElement> LanguageElementsList => _driver.FindElements(By.XPath("//*[@class='cf67405157'and text()]"));
        private Button LanguagePictureButton => new Button(_driver.FindElement(By.XPath("//*[@data-testid='header-language-picker-trigger']")));

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

        public void SelectLanguge(string language) => _button.ClickFirstThatContainsText(LanguageElementsList, language);
    }
}
