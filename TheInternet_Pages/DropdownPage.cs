using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Dropdown
{
    public class DropdownPage
    {
        private readonly IWebDriver _driver;
        private readonly By _selectOptions = By.Id("dropdown");

        public DropdownPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string SelectOption(string option)
        {
            var select = new SelectElement(_driver.FindElement(_selectOptions));
            select.SelectByText(option);
            return select.SelectedOption.Text;
        }
        public List<string> ShowAllOptions()
        {
            var select = new SelectElement(_driver.FindElement(_selectOptions));
            var optionsList = select.Options.Skip(1).Select(x => x.Text).ToList();
            return optionsList;
        }

        public void SelectRandomOption()
        {
            var select = new SelectElement(_driver.FindElement(_selectOptions));
            var optionList = select.Options;
            Random randomOption = new Random();
            int number;
            do
            {
                number = randomOption.Next(optionList.Count);
            }
            while (number == 0);
            select.SelectByIndex(number);
            Console.WriteLine(number);
        }

        public IWebElement GetSelectRandomOption() => _driver.FindElement(By.Id("dropdown")).FindElement(By.XPath("//*[@selected='selected']"));
    }
}