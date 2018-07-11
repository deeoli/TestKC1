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
    public class RegisterPage : PageBase
    {
        public RegisterPage(IWebDriver driver) : base(driver)
        {
        }


        [FindsBy(How = How.XPath, Using = "//h1[contains(text(),'Sign up')]")]
        public IWebElement lbl_pageLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//fieldset//input[@placeholder='Username']")]
        public IWebElement txt_UserName { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='$ctrl.formData.email']")]
        public IWebElement txt_Email { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='$ctrl.formData.password']")]
        public IWebElement txt_Password { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Sign up')]")]
        public IWebElement btn_SignUp { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@class='error-messages']/div[2]/li")]
        public IWebElement lbl_UserNameValidationMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@class='error-messages']/div[1]/li")]
        public IWebElement lbl_EmailValidationMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@class='error-messages']/div//li")]
        public IList<IWebElement> lbl_ValidationMsgs { get; set; }


        public List<string> ValidationMessage()
        {
            WaitForAngular();
            List<string> messages = new List<string>();
            foreach (var element in lbl_ValidationMsgs)
            {
                messages.Add(element.Text);
            }

            return messages;
        }


        public void SignUp(string username, string password, string email)
        {
            EnterText(txt_UserName, username);
            EnterText(txt_Email, email);
            EnterText(txt_Password, password);
            btn_SignUp.Click();
        }

    }
}
