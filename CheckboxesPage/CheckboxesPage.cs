using OpenQA.Selenium;

namespace Checkboxes
{
    public class CheckboxesPage
    {
        private readonly IWebDriver _driver;
        private readonly By allCheckboxes = By.XPath("//*[@type='checkbox']");
        private readonly By firstCheckbox = By.XPath("//*[@id='checkboxes']/input[1]");

        public CheckboxesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public int CountCkeckboxes()
        {
            return _driver.FindElements(allCheckboxes).Count();
        }

        public bool SelectFirstCheckbox()
        {
            _driver.FindElement(firstCheckbox).Click();
            bool isChecked = _driver.FindElement(firstCheckbox).Selected;
            return isChecked;
        }

        public bool SelectBothCheckboxes()
        {
            _driver.FindElement(allCheckboxes).Click();
            bool allChecked = _driver.FindElement(allCheckboxes).Selected;
            return allChecked;
        }
    }
}

