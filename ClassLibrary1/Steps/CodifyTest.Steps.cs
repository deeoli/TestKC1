using ClassLibrary1.PageObjects;
using CodifyTestFWork;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace ClassLibrary1.Steps
{
    /* TODO: create Table extraction and data randomization methods/classes, put in a helper folder 
     */
    [Binding]
    public sealed class CodifyTest : Core.TestContext
    {
        string _username = string.Empty;
        string _email = string.Empty;
        string _password = string.Empty;
        string _url = ConfigurationSettings.AppSettings["url"];

        [Given(@"a user is not logged in")]
        public void GivenAUserIsNotLoggedIn()
        {
            GetPage<HomePage>().Goto(_url);
            //GetPage<PageBase>().WaitForAngular();
            Assert.True(GetPage<HomePage>().lnk_SignIn.Displayed);
        }

        [When(@"the user signs up")]
        public void WhenTheUserSignsUp()
        {
            GetPage<PageBase>().WaitForAngular();
            GetPage<HomePage>().lnk_SignIn.Click();

        }

        [Then(@"the user is automatically logged in")]
        public void ThenTheUserIsAutomaticallyLoggedIn()
        {
            GetPage<PageBase>().WaitForAngular();
            Assert.AreEqual(GetPage<UserPage>().GetUserName(_username), _username);

        }

        [When(@"the user signs up with the following details")]
        public void WhenTheUserSignsUpWithTheFollowingDetails(Table table)
        {
   
            foreach (TableRow row in table.Rows)
            {
                _username= row["username"].ToString();
                _email = row["email"].ToString();
                _password = row["password"].ToString();
            }

            //Randomize 
            string[] tempEmail = _email.Split('@');
            tempEmail[0] = tempEmail[0] + AddRandomNumber("1000");
            _email = tempEmail[0] + "@" + tempEmail[1].ToString();

          
            _username = _username + AddRandomNumber("1000");
            _password = _username + AddRandomNumber("1000");

            GetPage<HomePage>().lnk_SignUp.Click();
            GetPage<PageBase>().WaitForAngular();
            GetPage<RegisterPage>()
                .SignUp(_username,_password, _email);
        }

        [When(@"the user signs in using")]
        public void WhenTheUserSignsInUsing(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _username = row["username"].ToString();
                _email = row["email"].ToString();
                _password = row["password"].ToString();
            }

            GetPage<HomePage>().lnk_SignIn.Click();
            GetPage<PageBase>().WaitForAngular();
            GetPage<SignInPage>()
                .SignIn(_password, _email);

            //Add assert here
           
        }



        [Then(@"the users name is displayed")]
        public void ThenTheUsersNameIsDisplayed()
        {
            GetPage<PageBase>().WaitForAngular();
            Assert.True(GetPage<UserPage>().IsUserNameDisplayed(_username));
        }
        

        [Then(@"the user is logged in")]
        public void ThenTheUserIsLoggedIn()
        {
            GetPage<PageBase>().WaitForAngular();
            Assert.AreEqual(GetPage<UserPage>().GetUserName(_username), _username);
        }

        [When(@"the home page is shown")]
        public void WhenTheHomePageIsShown()
        {
            GetPage<HomePage>().WaitForSecs();
            Assert.True(GetPage<HomePage>().lbl_HomePage.Enabled);
        }

        [Then(@"the global feeds and popular tags are displayed")]
        public void ThenTheGlobalFeedsAndPopularTagsAreDisplayed()
        {
            GetPage<PageBase>().WaitForAngular();
            Assert.True(GetPage<HomePage>().lnk_GlobalFeed.GetAttribute("class").Contains("active"));

            Assert.True(GetPage<HomePage>().ArticlesExist());
            Assert.True(GetPage<HomePage>().PopularTagisPopulated());
        }

        [When(@"a user tries to like an article")]
        public void WhenAUserTriesToLikeAnArticle()
        {
            GetPage<PageBase>().WaitForAngular();
            GetPage<HomePage>().LikeArticle();
        }

        [Then(@"the user is directed to the Sign up area")]
        public void ThenTheUserIsDirectedToTheSignUpArea()
        {
            GetPage<PageBase>().WaitForAngular();
            Assert.True(GetPage<RegisterPage>().lbl_pageLabel.Displayed, "");
        }

        [When(@"the user tries to sign up with a username that already exists")]
        public void WhenTheUserTriesToSignUpWithAUsernameThatAlreadyExists(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _username = row["username"].ToString();
                _email = row["email"].ToString();
                _password = row["password"].ToString();
            }

            GetPage<HomePage>().lnk_SignUp.Click();
            GetPage<PageBase>().WaitForAngular();
            GetPage<RegisterPage>()
                .SignUp(_username, _password, _email);
        }

        [Then(@"a validation error is displayed with message ""(.*)""")]
        public void ThenAValidationErrorIsDisplayedWithMessage(string message)
        {
            GetPage<PageBase>().WaitForAngular();
            bool messageExist = false;
            foreach (var msg in GetPage<RegisterPage>().ValidationMessage())
            {
                if (msg.Equals(message))
                {
                    messageExist = true;
                    break;
                }
                else { messageExist = false; }

            }

            Assert.True(messageExist);
        }


        [When(@"the user tries to sign up with an email address that already exists")]
        public void WhenTheUserTriesToSignUpWithAnEmailAddressThatAlreadyExists(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _username = row["username"].ToString();
                _email = row["email"].ToString();
                _password = row["password"].ToString();
            }

            GetPage<HomePage>().lnk_SignUp.Click();
            GetPage<PageBase>().WaitForAngular();
            GetPage<RegisterPage>()
                .SignUp(_username, _password, _email);
        }

        [When(@"the user tries to sign up with a password that is too short")]
        public void WhenTheUserTriesToSignUpWithAPasswordThatIsTooShort(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _username = row["username"].ToString();
                _email = row["email"].ToString();
                _password = row["password"].ToString();
            }

            GetPage<HomePage>().lnk_SignUp.Click();
            GetPage<PageBase>().WaitForAngular();
            GetPage<RegisterPage>()
                .SignUp(_username, _password, _email);
        }


        [Then(@"the Your Feeds section should be displayed by default")]
        public void ThenTheYourFeedsSectionShouldBeDisplayedByDefault()
        {
            GetPage<PageBase>().WaitForAngular();
            Assert.True(GetPage<UserPage>().lnk_YourFeed.GetAttribute("class").Contains("active"));
        }

        [Then(@"the Your Feeds section should be empty")]
        public void ThenTheYourFeedsSectionShouldBeEmpty()
        {
            //Site is buggy, your feeds link has to be clicked first
            GetPage<PageBase>().WaitForAngular();
            GetPage<UserPage>().lnk_YourFeed.Click();

        }

        [Given(@"a user is logged in as")]
        public void GivenAUserIsLoggedInAs(Table table)
        {
            GivenAUserIsNotLoggedIn();
            WhenTheUserSignsInUsing(table);
        }


        [When(@"the user creates a new article")]
        public void WhenTheUserCreatesANewArticle()
        {
            GetPage<PageBase>().WaitForAngular();
            GetPage<UserPage>().CreateArticle();
        }

        [Then(@"the Article section is displayed with the article information")]
        public void ThenTheArticleSectionIsDisplayedWithTheArticleInformation()
        {
            GetPage<PageBase>().WaitForAngular();
            Assert.True(GetPage<UserPage>().area_Artcle.Text
                 .Equals(GetPage<UserPage>()._articleDescrition));
        }

        [Then(@"the Comment section is displayed")]
        public void ThenTheCommentSectionIsDisplayed()
        {
            GetPage<PageBase>().WaitForAngular();
            Assert.True(GetPage<UserPage>().area_WriteAComment.Displayed);
        }

    }
}
