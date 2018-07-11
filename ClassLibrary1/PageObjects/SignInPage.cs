using CodifyTestFWork;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.PageObjects
{
    class SignInPage : PageBase
    {
        public SignInPage(IWebDriver driver) : base(driver)
        {
        }


        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sign in')]")]
        public IWebElement lbl_pageName { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='$ctrl.formData.email']")]
        public IWebElement txt_Email { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='$ctrl.formData.password']")]
        public IWebElement txt_Password { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Sign in')]")]
        public IWebElement btn_SignIn { get; set; }

        public void SignIn(string password, string email)
        {
            WaitForAngular();
            EnterText(txt_Email, email);
            EnterText(txt_Password, password);
            btn_SignIn.Click();
        }

    }
}
