using OpenQA.Selenium;

namespace Checkboxes
{
    public class CheckboxesPage
    {
        private readonly IWebDriver _driver;
        private IList <IWebElement> AllCheckboxes => _driver.FindElements(By.XPath("//*[@type='checkbox']"));
        private IWebElement FirstCheckbox => _driver.FindElement(By.XPath("//*[@id='checkboxes']/input[1]"));
        private IWebElement SecondCheckox => _driver.FindElement(By.XPath("//*[@id='checkboxes']/input[2]"));

        public CheckboxesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public int GetCheckboxesCount() => AllCheckboxes.Count;
     
        public void SelectFirstCheckbox() => FirstCheckbox.Click();

        public void SelectSecondCheckbox() => SecondCheckox.Click();

        public bool IsFirstCheckboxChecked()
        {
            bool firstChecked = FirstCheckbox.Selected;
            return firstChecked;
        }

        public bool AreBothCheckboxesChecked()
        {
            bool allChecked = AllCheckboxes.All(x => x.Selected);
            return allChecked;
        }
    }
}

