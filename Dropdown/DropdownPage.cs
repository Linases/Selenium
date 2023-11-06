using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
//using SeleniumExtras.WaitHelpers;

namespace Dropdown
{
    public class DropdownPage
    {
        private readonly IWebDriver _driver;
        private readonly By selectOptions = By.Id("dropdown");

        public DropdownPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string SelectOption(string option)
        {
            var select = new SelectElement(_driver.FindElement(selectOptions));
            select.SelectByText(option);
            return select.SelectedOption.Text;
        }
        public List<string> ShowAllOptions()
        {
            var select = new SelectElement(_driver.FindElement(selectOptions));
            var optionsList = select.Options.Skip(1).Select(x => x.Text).ToList();
            return optionsList;
        }

        public void SelectRandomOption()
        {
            var select = new SelectElement(_driver.FindElement(selectOptions));
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


        public IWebElement GetSelectRandomOption()
        {
            return _driver.FindElement(By.Id("dropdown")).FindElement(By.XPath("//*[@selected='selected']"));
        }
    }
}