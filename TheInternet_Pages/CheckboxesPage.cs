using OpenQA.Selenium;

namespace Checkboxes
{
    public class CheckboxesPage
    {
        private readonly IWebDriver _driver;
        private readonly By _allCheckboxes = By.XPath("//*[@type='checkbox']");
        private readonly By _firstCheckbox = By.XPath("//*[@id='checkboxes']/input[1]");
        private readonly By _secondCheckox = By.XPath("//*[@id='checkboxes']/input[2]");

        public CheckboxesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public int GetCheckboxesCount() => _driver.FindElements(_allCheckboxes).Count();

        public void SelectFirstCheckbox() => _driver.FindElement(_firstCheckbox).Click();

        public void SelectSecondCheckbox() => _driver.FindElement(_secondCheckox).Click();

        public bool IsFirstCheckboxChecked()
        {
            bool firstChecked = _driver.FindElement(_firstCheckbox).Selected;
            return firstChecked;
        }

        public bool AreBothCheckboxesChecked()
        {
            bool allChecked = _driver.FindElement(_allCheckboxes).Selected;
            return allChecked;
        }
    }
}

