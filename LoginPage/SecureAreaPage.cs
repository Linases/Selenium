using OpenQA.Selenium;

namespace Authentication
{
    public class SecureAreaPage
    {
        private readonly IWebDriver _driver;
        private readonly By validLoginMessage = By.Id("flash-messages");
       
        public SecureAreaPage (IWebDriver driver)
        {
            _driver = driver;
        }   

        public string GetLoginMessage()
        {
            return _driver.FindElement(validLoginMessage).Text;
        }
    }
}
