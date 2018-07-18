using CodifyTestFWork;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Test0507.Core;

namespace ClassLibrary1.Core
{
    [Binding]
    public class TestContext
    {

        public static IWebDriver driver;
        private static string browser = ConfigurationSettings.AppSettings["browser"];
        private static int timeout = Convert.ToInt32(ConfigurationSettings.AppSettings["browserTimeOut"]); 
        private static readonly object LockOb = new object();
        private static string exeDir = Assembly.GetExecutingAssembly().Location;
        private static string DriverServerFolder = GetDriverServersDirectory();
        private static string ScreenShotFolder = GetScreenShotDirectory();


        public static void GetDriver()
        {
            if (driver != null)
                return;
            lock (LockOb)
            {
                switch (browser)
                {
                    case "Chrome":
                        System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", DriverServerFolder + "chromedriver.exe");
                        driver = new ChromeDriver(DriverServerFolder);
                        break;
                    case "Firefox":
                        FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(DriverServerFolder, "geckodriver.exe");
                        driver = new FirefoxDriver(service);
                        break;
                    case "InternetExplorer":
                        System.Environment.SetEnvironmentVariable("webdriver.ie.driver", DriverServerFolder + "IEDriverServer.exe");
                        var internetExplorerOptions = new InternetExplorerOptions();
                        internetExplorerOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                        driver = new InternetExplorerDriver(DriverServerFolder, internetExplorerOptions);
                        break;
                    default:
                        throw new InvalidProgramException("Invalid value for Browser configuration.");
                }
            }
        }

        [BeforeScenario()]
        public static void Setup()
        {
            Console.WriteLine("\r\n\r\nFeature: " + FeatureContext.Current.FeatureInfo.Title);
            Console.WriteLine(FeatureContext.Current.FeatureInfo.Description);
            Console.WriteLine("\r\nScenario: " + ScenarioContext.Current.ScenarioInfo.Title);

            if (driver == null)
            {
                GetDriver();
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);
            driver.Manage().Window.Maximize();

            DirectoryHelper.DeleteAllFiles(GetScreenShotDirectory());

        }

        [AfterScenario()]
        public static void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }


        [AfterStep()]
        public static void TakeScreenShot()
        {
            string name = ScenarioContext.Current.StepContext.StepInfo.Text;

            //
            name = name.Replace(' ', '_');

            TakeScreenShot(name, ScreenShotFolder);
            name = null;
            name = null;
        }



        #region Methods
        #endregion

        public static void TakeScreenShot(string fileName, string pathToFolder)
        {
            fileName = pathToFolder + fileName + DateTime.Now.ToString("HHmmss") + ".png";
            Screenshot cp = ((ITakesScreenshot)driver).GetScreenshot();
            cp.SaveAsFile(fileName, ScreenshotImageFormat.Png);
        }

        public static string GetProjectDirectory()
        {
            string bin = Directory.GetParent(exeDir).ToString();
            string projectDir = Directory.GetParent(bin).ToString();
            return Directory.GetParent(projectDir).ToString();
        }


        public static string GetDriverServersDirectory()
        {
            return GetProjectDirectory() + "\\DriverServers\\";
        }

        public static string GetScreenShotDirectory()
        {
            return GetProjectDirectory() + "\\Screenshots\\";
        }


        public T GetPage<T>() where T : PageBase
        {
            T page = Activator.CreateInstance(typeof(T), driver) as T;
            PageFactory.InitElements(driver, page);
            return page;
        }

        public string AddRandomNumber(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException();
            }
            Random ran = new Random();
            return ran.Next(1000).ToString();
        }

    }
}
