using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Dropdown
{
    public class DropdownPage
    {
        private readonly IWebDriver _driver;

        public DropdownPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public SelectElement GetDropdown()
        {
            var selectElement = _driver.FindElement(By.Id("dropdown"));
            return new SelectElement(selectElement);
        }

        public string SelectOption(string option)
        {
            GetDropdown().SelectByText(option);
            return GetDropdown().SelectedOption.Text;
        }

        public List<string> GetAllOptions() => GetDropdown().Options.Skip(1).Select(x => x.Text).ToList();

        public void SelectRandomOption()
        {
            var optionList = GetDropdown().Options;
            Random randomOption = new Random();
            int number;
            do
            {
                number = randomOption.Next(optionList.Count);
            }
            while (number == 0);
            GetDropdown().SelectByIndex(number);
            Console.WriteLine(number);
        }

        public IWebElement GetSelectedOption()
        {
            SelectRandomOption();
            var selectedOption = _driver.FindElement(By.Id("dropdown")).FindElement(By.XPath("//*[@selected='selected']"));
            return selectedOption;
        }

        public bool IsSelectedOptionDisplayed() => GetSelectedOption().Displayed;
    }
}