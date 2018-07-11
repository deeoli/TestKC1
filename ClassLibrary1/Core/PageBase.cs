using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodifyTestFWork
{
    public class PageBase
    {
        protected IWebDriver _driver;

        public string _url = "http://localhost:4000/#!/";

        public PageBase(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Goto(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void EnterText(IWebElement element, string text)
        {
            element.Click();
            element.Clear();
            element.SendKeys(text);

        }

        public void WaitForSecs(int secs = 3)
        {
            var timeout = DateTime.Now.AddSeconds(secs);
            try
            {
                while (DateTime.Now < timeout)
                {
                    Thread.Sleep(1000);
                }
            }
            catch (ThreadInterruptedException e) { Console.WriteLine(e.Message); }
        }

        public PageBase WaitForAngular(double timeout=20)
        {
            try
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout)).Until(
                    driver =>
                    {
                        var javaScriptExecutor = driver as IJavaScriptExecutor;
                        return javaScriptExecutor != null
                               &&
                               (bool)javaScriptExecutor.ExecuteScript(
                                   "return window.angular != undefined && window.angular.element(document.body).injector().get('$http').pendingRequests.length == 0");
                    });
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Wait for angular invalid operation exception.");
                Console.WriteLine(e.Message);
            }
            return this;
        }



        public bool isElementPresentAndDisplayed(IWebElement ele)
        {
            try
            {
                return ele.Displayed;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

    }
}
