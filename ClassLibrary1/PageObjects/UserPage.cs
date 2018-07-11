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
    public class UserPage : PageBase
    {

        public string _articleDescrition = "New Test article body, example of an article";

        public UserPage(IWebDriver driver) : base(driver)
        {
        }


        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Your Feed')]")]
        public IWebElement lnk_YourFeed { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[@class='nav-item']//a[contains(.,'New Article')]")]
        public IWebElement lnk_NewArticle { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='$ctrl.article.title']")]
        public IWebElement txt_ArtcleTitle { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='$ctrl.article.description']")]
        public IWebElement txt_ArtcleDesc { get; set; }

        [FindsBy(How = How.XPath, Using = "//textarea[@ng-model='$ctrl.article.body']")]
        public IWebElement txt_ArtcleBody { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@ng-model='$ctrl.tagField']")]
        public IWebElement txt_ArtcleTags { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-bind-html='::$ctrl.article.body']")]
        public IWebElement area_Artcle { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@ng-click='$ctrl.submit()']")]
        public IWebElement btn_PublishArticle { get; set; }

        [FindsBy(How = How.XPath, Using = "//textarea[@placeholder='Write a comment...']")]
        public IWebElement area_WriteAComment { get; set; }

        public bool IsUserNameDisplayed(string username)
        {
            return _driver.FindElement(By.XPath($"//li[@class='nav-item']//a[@href='#!/@{username}']")).Displayed;
        }


        public string GetUserName(string username)
        {
            return _driver.FindElement(By.XPath($"//li[@class='nav-item']//a[@href='#!/@{username}']")).Text;
        }

        public bool isArticleListEmpty()
        {
            return _driver.FindElements(By.XPath("//article-list//article-preview")).Count > 0;
        }

        public void CreateArticle()
        {
            WaitForAngular();
            lnk_NewArticle.Click();
            EnterText(txt_ArtcleTitle, "New Test article");
            EnterText(txt_ArtcleDesc, "New Test article description");
            EnterText(txt_ArtcleBody, _articleDescrition);
            EnterText(txt_ArtcleTags, "Test");

            btn_PublishArticle.Click();
        }
    }
}
