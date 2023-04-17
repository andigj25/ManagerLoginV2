using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;

namespace SeleniumTests
{
    [TestFixture]
    public class CookieTest_V2
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void firstLogin()
        {
            // Navigate to the login page and login
            driver.Navigate().GoToUrl("https://qawa2.telecomsoftware.com/");
            driver.FindElement(By.CssSelector("input[name='username']")).SendKeys("andi");
            driver.FindElement(By.CssSelector("button[role='button']")).Click();
            IWebElement logout = driver.FindElement(By.CssSelector("[class] [class] [showon='none']:nth-of-type(3)"));
            Assert.IsTrue(logout.Displayed);
            CookieHelper_V2.SaveCookieToFile(driver, "Qawa2Cookies1.json");
        }

        [Test]
        public void secondLogin()
        {
            // Navigate to the login page
            driver.Navigate().GoToUrl("https://qawa2.telecomsoftware.com/");

            // Read cookies from the first test
            CookieHelper_V2.LoadCookieFromFile(driver, "Qawa2Cookies1.json");

            // Refresh the page to apply cookies
            driver.Navigate().Refresh();

            // Check if login was successful
            IWebElement logout = driver.FindElement(By.CssSelector("[class] [class] [showon='none']:nth-of-type(3)"));
            Assert.IsTrue(logout.Displayed);
        }

        [TearDown]
        public void TeardownTest()
        {
            driver.Quit();
        }
    }
}
