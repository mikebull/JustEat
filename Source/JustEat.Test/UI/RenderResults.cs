using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace JustEat.Test.UI
{
    [TestFixture]
    public class RenderResults
    {
        private IWebDriver _driver;
        private StringBuilder _verificationErrors;
        private string _baseUrl;
        private bool _acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            _driver = new FirefoxDriver();
            _baseUrl = "http://localhost:60314";
            _verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                _driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", _verificationErrors.ToString());
        }
        
        [Test]
        public void TheRenderResultsTest()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/");
            _driver.FindElement(By.Id("Outcode")).Clear();
            _driver.FindElement(By.Id("Outcode")).SendKeys("SE11");
            _driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            Assert.AreEqual("Results", _driver.FindElement(By.CssSelector("h2")).Text);
            Assert.IsTrue(IsElementPresent(By.CssSelector("div.result")));
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                _driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = _driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (_acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                _acceptNextAlert = true;
            }
        }
    }
}
