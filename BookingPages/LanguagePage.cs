using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Utilities;
using Wrappers;
using static System.Net.Mime.MediaTypeNames;

namespace BookingPages
{
    public class LanguagePage
    {
        private readonly IWebDriver _driver;
        private By LanguageElementsList => (By.XPath("//*[@class='cf67405157'and text()]"));
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

        public void SelectLanguge(string language)
        {
            var list = _driver.WaitForElementsVisible(LanguageElementsList);
            var element = list.FirstOrDefault(element => element.Text.Contains($"{language}"));
            element.Click();
        }
    }
}
