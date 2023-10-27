using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Drawing;
using Functionality_Tests_Suit.Constants;
using Functionality_Tests_Suit.FactoryPattern;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Internal;
using File = System.IO.File;
using OpenQA.Selenium.Interactions;

namespace Browser_Actions
{
    [TestFixture]
    public class Browser_ActionTests
    {
        private static IWebDriver _driver;
        private readonly string _mainUrl;

        public Browser_ActionTests()
        {
            _mainUrl = "http://the-internet.herokuapp.com";
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _driver = BrowserFactory.GetDriver(BrowserType.Chrome);
        }

        [SetUp]
        public void Setup()
        {
            _driver.Navigate().GoToUrl(_mainUrl);
            var mainPageUrl = _driver.Url;
            Assert.That(mainPageUrl, Is.EqualTo($"{_mainUrl}/"), "Website did not load successfully");
        }

        [Test]
        public void OpenNewWindowHandleTabs()
        {
            var originalWindow = _driver.CurrentWindowHandle;
            Assert.That(_driver.WindowHandles.Count, Is.EqualTo(1), "Opened window is not the first");
            var elementMultipleWindows = _driver.FindElement(By.CssSelector("[href*='windows']"));
            elementMultipleWindows.Click();
            var elementClickHere = _driver.FindElement(By.XPath("//*[@class='example']//a"));
            elementClickHere.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(wd => wd.WindowHandles.Count == 2);
            foreach (var window in _driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    _driver.SwitchTo().Window(window);
                    break;
                }
            }
            wait.Until(wd => wd.Url == $"{_mainUrl}/windows/new");
            var elementContent = _driver.FindElement(By.ClassName("example"));
            Assert.That(elementContent.Displayed, " Content 'New Window' is not displayed");
        }

        [Test]
        public void NavigateBackForward()
        {
            var elementFormAuthentication = _driver.FindElement(By.CssSelector("[href*='login']"));
            elementFormAuthentication.Click();
            var elementUsername = _driver.FindElement(By.Id("username"));
            elementUsername.SendKeys("tomsmith");
            var elemenPassword = _driver.FindElement(By.Id("password"));
            elemenPassword.SendKeys("SuperSecretPassword!");
            var elementLoginButton = _driver.FindElement(By.XPath("//*[@class='radius']"));
            elementLoginButton.Click();
            var elementLogoutButton = _driver.FindElement(By.CssSelector("[href*='logout']"));
            elementLogoutButton.Click();
            _driver.Navigate().Back();
            var pageUrlBack = _driver.Url;
            Assert.That(pageUrlBack, Is.EqualTo($"{_mainUrl}/secure"));
            _driver.Navigate().Forward();
            var pageUrlForward = _driver.Url;
            Assert.That(pageUrlForward, Is.EqualTo($"{_mainUrl}/login"));
            var elementContentLogin = _driver.FindElement(By.Id("content"));
            Assert.That(elementContentLogin.Text.Contains("Login Page"));
        }

        [Test]
        public void NavigateToUrlAndRefresh()
        {
            var elementDynamicLoading = _driver.FindElement(By.CssSelector("[href*='dynamic_loading']"));
            elementDynamicLoading.Click();
            var elementExample = _driver.FindElement(By.XPath("//*[@id='content']//a"));
            elementExample.Click();
            var elementStartButton = _driver.FindElement(By.TagName("button"));
            elementStartButton.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finish"))).Displayed);
            var currentUrl = _driver.Url;
            _driver.Navigate().GoToUrl(_mainUrl);
            var homePageUrl = _driver.Url;

            Assert.False(homePageUrl.Equals(currentUrl));
            Assert.That(homePageUrl, Is.EqualTo($"{_mainUrl}/"));
            _driver.Navigate().Refresh();
        }

        [Test]
        public void MaximizeWindowChangeWindowSize()
        {
            var elementLargeDeepDom = _driver.FindElement(By.CssSelector("[href*='large']"));
            elementLargeDeepDom.Click();

            var elementLastDom = _driver.FindElement(By.XPath("//*[@id='large-table']//*[text()='50.50']"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", elementLastDom);
            Assert.That(elementLastDom.Displayed, "Last DOM '50.50' is not displayed");
            _driver.Manage().Window.Maximize();

            var elementFirstLine = _driver.FindElement(By.XPath("//*[@id='content']/div/h3"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", elementFirstLine);
            Assert.That(elementFirstLine.Displayed, "First Line 'Large & Deep DOM' is not displayed");

            var newSize = new Size(1000, 800);
            _driver.Manage().Window.Size = newSize;
            var currentSize = _driver.Manage().Window.Size;
            Assert.That(currentSize.Width, Is.EqualTo(newSize.Width), "Current window width is not 1000");
            Assert.That(currentSize.Height, Is.EqualTo(newSize.Height), "Current window height is not 800");
        }

        [Test]
        public void HeadlessMode()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless=new");
            using var headlessDriver = new ChromeDriver(options);

            headlessDriver.Navigate().GoToUrl(_mainUrl);
            var elementCheckboxes = headlessDriver.FindElement(By.CssSelector("[href*='checkboxes']"));
            elementCheckboxes.Click();
            var elementChecked = headlessDriver.FindElement(By.XPath("//*[@id='checkboxes']/input[1]"));
            elementChecked.Click();
            Assert.That(elementChecked.Selected, Is.True, "checkbox 1 is not selected");
            var elementUnchecked = headlessDriver.FindElement(By.XPath("//*[@id='checkboxes']/input[2]"));
            elementUnchecked.Click();
            Assert.That(elementUnchecked.Selected, Is.False, "checkbox 2 is selected");
            headlessDriver.Close();
        }

        [Test]
        public void SimpleAlert()
        {
            OpenJavaScriptAlertsLink();
            var allertButton = _driver.FindElement(By.XPath("//*[@onclick='jsAlert()']"));
            allertButton.Click();
            var simpleAlert = _driver.SwitchTo().Alert();
            Assert.That(simpleAlert.Text, Is.EqualTo("I am a JS Alert"), "Alert dialog with text 'I am a JS Alert' is not displayed");
            simpleAlert.Accept();
            var acceptanceMessage = _driver.FindElement(By.Id("result"));
            Console.WriteLine(acceptanceMessage.Text);
            Assert.That(acceptanceMessage.Text, Is.EqualTo("You successfully clicked an alert"), "Alert acceptance message 'You successfully clicked an alert' is not shown");
        }

        [Test]
        public void ConfirmationAlert()
        {
            OpenJavaScriptAlertsLink();
            var allertButton = _driver.FindElement(By.XPath("//*[@onclick='jsConfirm()']"));
            allertButton.Click();
            var confirmationAlert = _driver.SwitchTo().Alert();
            Assert.That(confirmationAlert.Text, Is.EqualTo("I am a JS Confirm"), "Alert dialog with text 'I am a JS Confirm' is not displayed");
            confirmationAlert.Dismiss();
            var dismissedMessage = _driver.FindElement(By.XPath("//*[text()='You clicked: Cancel']"));
            Console.WriteLine(dismissedMessage.Text);
            Assert.That(dismissedMessage.Text, Is.EqualTo("You clicked: Cancel"), "Alert dismissal message 'You clicked: Cancel' is not shown");
        }

        [Test]
        public void PromptAlert()
        {
            OpenJavaScriptAlertsLink();
            var allertButton = _driver.FindElement(By.XPath("//*[@onclick='jsPrompt()']"));
            allertButton.Click();
            var promptAlert = _driver.SwitchTo().Alert();
            Assert.That(promptAlert.Text, Is.EqualTo("I am a JS prompt"), "Alert dialog with text 'I am a JS prompt' is not displayed");
            promptAlert.SendKeys("ok");
            promptAlert.Accept();
            var enteredMessage = _driver.FindElement(By.Id("result"));
            Assert.That(enteredMessage.Text, Is.EqualTo("You entered: ok"), "Entered text message 'You entered: ok' is not shown");
        }

        [Test]
        public void SwitchingToAnIframe()
        {
            var jelementFrames = _driver.FindElement(By.XPath("//*[@id='content']//*[text()='Frames']"));
            jelementFrames.Click();
            var framesUrl = _driver.Url;
            Assert.That(framesUrl, Is.EqualTo($"{_mainUrl}/frames"));
            var elementIframe = _driver.FindElement(By.XPath("//*[text()='iFrame']"));
            elementIframe.Click();
            var iFramePage = _driver.Url;
            Assert.That(iFramePage, Is.EqualTo($"{_mainUrl}/iframe"));
            var iFrameElement = _driver.FindElement(By.XPath("//*[@id='mce_0_ifr']"));
            _driver.SwitchTo().Frame(iFrameElement);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var iFrameParagraph = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p")));
            Assert.That(iFrameParagraph.Text.Contains("Your content goes here."), "Paragraph text 'Your content goes here.' is not visible ");
            Assert.That(iFrameParagraph.Enabled, "Input is not enabled.");
        }

        [Test]
        public void SelectElement()
        {
            var elementDropdown = _driver.FindElement(By.XPath("//*[@id='content']//*[text()='Dropdown']"));
            elementDropdown.Click();
            var dropdownUrl = _driver.Url;
            Assert.That(dropdownUrl, Is.EqualTo($"{_mainUrl}/dropdown"));
            var select = new SelectElement(_driver.FindElement(By.Id("dropdown")));
            select.SelectByText("Option 1");
            var selectedOption = select.SelectedOption.Text;
            Assert.That(selectedOption, Is.EqualTo("Option 1"), "Selected option is not 'Option 1'");
        }

        [Test]
        public void CheckboxElement()
        {
            var elementCheckboxes = _driver.FindElement(By.XPath("//*[@id='content']//*[text()='Checkboxes']"));
            elementCheckboxes.Click();
            var checkboxesUrl = _driver.Url;
            Assert.That(checkboxesUrl, Is.EqualTo($"{_mainUrl}/checkboxes"));
            var checkboxSelect = _driver.FindElement(By.XPath("//*[@id='checkboxes']/input[1]"));
            checkboxSelect.Click();
            bool isChecked = checkboxSelect.Selected;
            Assert.That(isChecked, Is.True, "'Checkbox 1' is ot selected");
        }

        [Test]
        public void RangeElement()
        {
            var elementHorizontalSlider = _driver.FindElement(By.XPath("//*[@id='content']//*[text()='Horizontal Slider']"));
            elementHorizontalSlider.Click();
            var elementHorizontalSliderUrl = _driver.Url;
            Assert.That(elementHorizontalSliderUrl, Is.EqualTo($"{_mainUrl}/horizontal_slider"));
            var inputRange = _driver.FindElement(By.XPath("//*[@type='range']"));
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].value = '5';", inputRange);
            var rangeValue = inputRange.GetAttribute("value");
            Assert.That(rangeValue, Is.EqualTo("5"), "Range input value is not equal to 5");
        }

        [Test]
        public void TextInputElement()
        {
            var elementInputs = _driver.FindElement(By.XPath("//*[@id='content']//*[text()='Inputs']"));
            elementInputs.Click();
            var InputsUrl = _driver.Url;
            Assert.That(InputsUrl, Is.EqualTo($"{_mainUrl}/inputs"));
            var elementInputNumber = _driver.FindElement(By.XPath("//*[@type='number']"));
            elementInputNumber.SendKeys("1");
            var inputNumber = elementInputNumber.GetAttribute("value");
            Assert.That(inputNumber, Is.EqualTo("1"));
        }

        [Test]
        public void BasicAuth()
        {
            var elementBasicAuth = _driver.FindElement(By.XPath("//*[@id='content']//*[text()='Basic Auth']"));
            elementBasicAuth.Click();
            var BasicAuthUrl = _driver.Url;
            Assert.That(BasicAuthUrl, Is.EqualTo($"{_mainUrl}/basic_auth"));

            var authWindowLink = "https://" + "admin:admin@" +
            "the-internet.herokuapp.com/basic_auth";
            _driver.Navigate().GoToUrl(authWindowLink);

            var validationMessage = _driver.FindElement(By.XPath("//*[@class='example']//p")).Text;
            Console.WriteLine("Text is: " + validationMessage);
            Assert.That(validationMessage, Is.EqualTo("Congratulations! You must have the proper credentials."));
        }

        [Test]
        public void FileDownload()
        {
            var myDownloadFolder = "SeleniumDownload";
            var downloadDirectory = Path.Combine(Directory.GetCurrentDirectory(), myDownloadFolder);
            if (!Directory.Exists(downloadDirectory))
            {
                Directory.CreateDirectory(downloadDirectory);
            }
            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", downloadDirectory);
            using var _newDriver = new ChromeDriver(options);

            _newDriver.Navigate().GoToUrl(_mainUrl);
            var elementFileDownLoad = _newDriver.FindElement(By.XPath("//*[@id='content']//*[text()='File Download']"));
            elementFileDownLoad.Click();
            var FileDownloadUrl = _newDriver.Url;
            Assert.That(FileDownloadUrl, Is.EqualTo($"{_mainUrl}/download"));
            var fileDownLoad = _newDriver.FindElement(By.XPath("//*[text()='some-file.txt']"));
            fileDownLoad.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(d => Directory.GetFiles(myDownloadFolder).Length > 0);
            Assert.That(File.Exists($"{myDownloadFolder}/some-file.txt"), Is.True, "File 'some-file.txt' did not download");
            _newDriver.Close();
        }

        [Test]
        public void ValidateSortByFirstName()
        {
            OpenSortableTadaTables();
            SortByFirstName();
        }

        [Test]
        public void ToggleLastName()
        {
            OpenSortableTadaTables();
            var act = new Actions(_driver);
            var lastNameHeader = _driver.FindElement(By.XPath("//*[@id='table1']//*[text()='Last Name']"));
            act.DoubleClick(lastNameHeader).Perform();
            var toggleDLastNames = _driver.FindElements(By.XPath("//*[@id='table1']//tbody/tr//td[1]"));
            foreach (var name in toggleDLastNames)
            {
                Console.WriteLine(name.Text);
            }
            Assert.That(toggleDLastNames.OrderByDescending(x => x.Text), Is.EqualTo(toggleDLastNames));
        }

        [Test]
        public void ValidateRowData()
        {
            OpenSortableTadaTables();
            var table = _driver.FindElement(By.XPath("//*[@id='table1']"));
            var firstRow = table.FindElement(By.XPath(".//tr"));
            var cells = firstRow.FindElements(By.XPath("//*[@id='table1']//tr[1]//td"));

            Assert.That(cells[0].Text, Is.EqualTo("Smith"));
            Assert.That(cells[1].Text, Is.EqualTo("John"));
            Assert.That(cells[2].Text, Is.EqualTo("jsmith@gmail.com"));
            Assert.That(cells[3].Text, Is.EqualTo("$50.00"));
            Assert.That(cells[4].Text, Is.EqualTo("http://www.jsmith.com"));
            Assert.That(cells[5].Text, Is.EqualTo("edit delete"));
        }

        [Test]
        public void NavigationAndReturnToPage()
        {
            OpenSortableTadaTables();
            SortByFirstName();
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var newPageUrl = _driver.Url;
            Assert.That(newPageUrl, Is.EqualTo("https://www.saucedemo.com/"));
            _driver.Navigate().Back();
            var oldPageUrl = _driver.Url;
            Assert.That(oldPageUrl, Is.EqualTo($"{_mainUrl}/tables"));
            var allNamesList = _driver.FindElements(By.XPath("//*[@id='table1']//tbody/tr//td[2]"));
            foreach (var element in allNamesList)
            {
                Console.WriteLine(element.Text);
            }
            var sorted = allNamesList.OrderBy(x => x.Text);
            Assert.AreNotSame(allNamesList, sorted);
        }

        [Test]
        public void SortedByDueColumn()
        {
            OpenSortableTadaTables();
            var elementDue = _driver.FindElement(By.XPath("//*[@id='table1']//*[text()='Due']"));
            elementDue.Click();
            var allDues = _driver.FindElements(By.XPath("//*[@id='table1']//tbody/tr//td[4]"));
            var orderedDues = allDues.OrderBy(x => float.Parse(x.Text[1..]));
            Assert.That(orderedDues, Is.EqualTo(allDues), "The data in the 'Due' column is not sorted in ascending order");
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            BrowserFactory.CloseDriver();
        }

        private void OpenJavaScriptAlertsLink()
        {
            var javaScriptAlert = _driver.FindElement(By.XPath("//*[@id='content']//*[text()='JavaScript Alerts']"));
            javaScriptAlert.Click();
            var pageUrl = _driver.Url;
            Assert.That(pageUrl, Is.EqualTo($"{_mainUrl}/javascript_alerts"), "'JavaScript Alerts' page is not shown");
        }

        private void OpenSortableTadaTables()
        {
            var elementSortableDataTables = _driver.FindElement(By.XPath("//*[@id='content']//*[text()='Sortable Data Tables']"));
            elementSortableDataTables.Click();
            var sortableDataTableUrl = _driver.Url;
            Assert.That(sortableDataTableUrl, Is.EqualTo($"{_mainUrl}/tables"));
        }

        public void SortByFirstName()
        {
            var firstNameHeader = _driver.FindElement(By.XPath("//*[@id='table1']//*[text()='First Name']"));
            firstNameHeader.Click();
            var allNamesList = _driver.FindElements(By.XPath("//*[@id='table1']//tbody/tr//td[2]"));
            Assert.That(allNamesList.OrderBy(x => x.Text), Is.EqualTo(allNamesList), "'First Name' column is not sorted in ascending order");
        }
    }
}
