using OpenQA.Selenium;

namespace Authentication
{
    public class SecureAreaPage
    {
        private readonly IWebDriver _driver;
        private IWebElement ValidLoginMessage => _driver.FindElement(By.Id("flash-messages"));

        public SecureAreaPage (IWebDriver driver)
        {
            _driver = driver;
        }   

        public string GetLoginMessage() => ValidLoginMessage.Text;
    }
}
