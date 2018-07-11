using CodifyTestFWork;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace ClassLibrary1.PageObjects
{
    public class HomePage : PageBase
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }


        [FindsBy(How = How.XPath, Using = "//title[contains(text(),'Home — Conduit')]")]
        public IWebElement lbl_HomePage { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sign in')]")]
        public IWebElement lnk_SignIn { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sign up')]")]
        public IWebElement lnk_SignUp { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Global Feed')]")]
        public IWebElement lnk_GlobalFeed { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='sidebar']//p[contains(text(),'Populat Tags')]")]
        public IWebElement lbl_PopularTags { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='article-preview']//a[2]")]
        public IWebElement lnk_Article { get; set; }

        public bool ArticlesExist()
        {
            return _driver.FindElements(By.XPath("//article-list//article-preview")).Count > 0;
        }

        public bool PopularTagisPopulated()
        {
            return _driver.FindElements(By.XPath("//div[@class='tag-list']//a")).Count > 0;
        }

        public void LikeArticle()
        {
           IList<IWebElement> elements= _driver.FindElements(By.XPath("//div[@class='article-preview']//favorite-btn//button"));
            foreach (var ele in elements)
            {
                WaitForAngular();
                ele.Click();
                break;
            }
        }


    }
}
