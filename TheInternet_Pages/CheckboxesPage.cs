using OpenQA.Selenium;

namespace Checkboxes
{
    public class CheckboxesPage
    {
        private readonly IWebDriver _driver;
        private IList<IWebElement> AllCheckboxes => _driver.FindElements(By.XPath("//*[@type='checkbox']"));
        private IWebElement FirstCheckbox => _driver.FindElement(By.XPath("//*[@id='checkboxes']/input[1]"));
        private IWebElement SecondCheckox => _driver.FindElement(By.XPath("//*[@id='checkboxes']/input[2]"));

        public CheckboxesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public int GetCheckboxesCount() => AllCheckboxes.Count;

        public void SelectFirstCheckbox()
        {
            if (!FirstCheckbox.Selected)
            {
                FirstCheckbox.Click();
            }
        }

        public void SelectSecondCheckbox()
        {
            if (!SecondCheckox.Selected)
            {
                SecondCheckox.Click();
            }
        }

        public bool IsFirstCheckboxChecked()
        {
            bool firstChecked = FirstCheckbox.Selected;
            return firstChecked;
        }

        public void SelectBothCheckboxes()
        {
            SelectFirstCheckbox();
            SelectSecondCheckbox();
        }

        public void UnselectBothCheckboxes()
        {
            UnselectFirstCheckboxe();
            UnselectSecondCheckboxe();
        }

        public void UnselectFirstCheckboxe()
        {
            if (FirstCheckbox.Selected)
            {
                FirstCheckbox.Click();
            }
        }

        public void UnselectSecondCheckboxe()
        {
            if (SecondCheckox.Selected)
            {
                SecondCheckox.Click();
            }
        }

        public bool IsSecondCheckboxChecked()
        {
            bool secondChecked = SecondCheckox.Selected;
            return secondChecked;
        }
        public bool AreBothCheckboxesChecked()
        {
            bool allChecked = AllCheckboxes.All(x => x.Selected);
            return allChecked;
        }
    }
}

