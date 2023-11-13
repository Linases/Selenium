using OpenQA.Selenium;

namespace Authentication
{
    public class SecureAreaPage
    {
        private readonly IWebDriver _driver;
        private readonly By _validLoginMessage = By.Id("flash-messages");
        private IWebElement ValidLoginMessage => _driver.FindElement(_validLoginMessage);

        public SecureAreaPage (IWebDriver driver)
        {
            _driver = driver;
        }   

        public string GetLoginMessage() => ValidLoginMessage.Text;
    }
}
