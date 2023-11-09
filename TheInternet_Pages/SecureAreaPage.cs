using OpenQA.Selenium;

namespace Authentication
{
    public class SecureAreaPage
    {
        private readonly IWebDriver _driver;
        private readonly By _validLoginMessage = By.Id("flash-messages");
       
        public SecureAreaPage (IWebDriver driver)
        {
            _driver = driver;
        }   

        public string GetLoginMessage() =>_driver.FindElement(_validLoginMessage).Text;
    }
}
