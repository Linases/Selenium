using OpenQA.Selenium;

namespace Checkboxes
{
    public class CheckboxesPage
    {
        private readonly IWebDriver _driver;
        private readonly By _allCheckboxes = By.XPath("//*[@type='checkbox']");
        private readonly By _firstCheckbox = By.XPath("//*[@id='checkboxes']/input[1]");

        public CheckboxesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public int CountCkeckboxes() => _driver.FindElements(_allCheckboxes).Count();
    
        public bool SelectFirstCheckbox()
        {
            _driver.FindElement(_firstCheckbox).Click();
            bool isChecked = _driver.FindElement(_firstCheckbox).Selected;
            return isChecked;
        }

        public bool SelectBothCheckboxes()
        {
            _driver.FindElement(_allCheckboxes).Click();
            bool allChecked = _driver.FindElement(_allCheckboxes).Selected;
            return allChecked;
        }
    }
}

