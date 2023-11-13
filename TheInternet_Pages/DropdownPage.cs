using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Dropdown
{
    public class DropdownPage
    {
        private readonly IWebDriver _driver;
        private SelectElement Select => new(_driver.FindElement(By.Id("dropdown")));

        public DropdownPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ChooseOption(string option)
        {
            Select.SelectByText(option);
        }

        public string GetSelectedOption() => Select.SelectedOption.Text;
          
        public List<string> GetAllOptions() => Select.Options.Skip(1).Select(x => x.Text).ToList();

        public void SelectRandomOption()
        {
            var optionList = Select.Options;
            Random randomOption = new Random();
            int number;
            do
            {
                number = randomOption.Next(optionList.Count);
            }
            while (number == 0);
            Select.SelectByIndex(number);
            Console.WriteLine(number);
        }

        public bool IsRandomSelectedOptionDisplayed()
        {
            var isDispayed = GetRandomSelectedOption().Displayed;
            return isDispayed;
        }

        private IWebElement GetRandomSelectedOption()
        {
            var selectedOption = _driver.FindElement(By.Id("dropdown")).FindElement(By.XPath("//*[@selected='selected']"));
            return selectedOption;
        }
    }
}