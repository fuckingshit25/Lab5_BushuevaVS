using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;

namespace Лаб5
{
    [TestFixture]
    public class CalculatorTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private CalculatorPage calculatorPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            calculatorPage = new CalculatorPage(driver);
            driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/SimpleCalculator/");
        }

        [Test]
        public void AdditionTest()
        {
            calculatorPage.EnterValueA("2");
            calculatorPage.IncrementA();
            calculatorPage.SelectOperation("+");
            calculatorPage.EnterValueB("3");
            calculatorPage.IncrementB();
            calculatorPage.ClickCalculateButton();

            Assert.AreEqual("6", calculatorPage.GetResult());
        }

        [Test]
        public void SubtractionTest()
        {
            calculatorPage.EnterValueA("5");
            calculatorPage.DecrementA();
            calculatorPage.SelectOperation("-");
            calculatorPage.EnterValueB("3");
            calculatorPage.DecrementB();
            calculatorPage.ClickCalculateButton();

            Assert.AreEqual("1", calculatorPage.GetResult());
        }

        [Test]
        public void MultiplicationTest()
        {
            calculatorPage.EnterValueA("4");
            calculatorPage.IncrementA();
            calculatorPage.SelectOperation("*");
            calculatorPage.EnterValueB("3");
            calculatorPage.IncrementB();
            calculatorPage.ClickCalculateButton();

            Assert.AreEqual("15", calculatorPage.GetResult());
        }

        [Test]
        public void DivisionTest()
        {
            calculatorPage.EnterValueA("10");
            calculatorPage.DecrementA();
            calculatorPage.SelectOperation("/");
            calculatorPage.EnterValueB("2");
            calculatorPage.DecrementB();
            calculatorPage.ClickCalculateButton();

            Assert.AreEqual("4", calculatorPage.GetResult());
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }

    public class CalculatorPage
    {
        private IWebDriver driver;
        private IWebElement ValueAInput => driver.FindElement(By.XPath("//input[@ng-model='a']"));
        private IWebElement ValueBInput => driver.FindElement(By.XPath("//input[@ng-model='b']"));
        private IWebElement IncrementAButton => driver.FindElement(By.XPath("//button[@ng-click='inca()']"));
        private IWebElement DecrementAButton => driver.FindElement(By.XPath("//button[@ng-click='deca()']"));
        private IWebElement IncrementBButton => driver.FindElement(By.XPath("//button[@ng-click='incb()']"));
        private IWebElement DecrementBButton => driver.FindElement(By.XPath("//button[@ng-click='decb()']"));
        private IWebElement OperationSelect => driver.FindElement(By.XPath("//select[@ng-model='operation']"));
        private IWebElement CalculateButton => driver.FindElement(By.XPath("//button[@class='command']"));
        private IWebElement Result => driver.FindElement(By.XPath("//b[@class='result']"));

        public CalculatorPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void EnterValueA(string value)
        {
            ValueAInput.Clear();
            ValueAInput.SendKeys(value);
        }

        public void EnterValueB(string value)
        {
            ValueBInput.Clear();
            ValueBInput.SendKeys(value);
        }

        public void IncrementA()
        {
            IncrementAButton.Click();
        }

        public void DecrementA()
        {
            DecrementAButton.Click();
        }

        public void IncrementB()
        {
            IncrementBButton.Click();
        }

        public void DecrementB()
        {
            DecrementBButton.Click();
        }

        public void SelectOperation(string operation)
        {
            var select = new SelectElement(OperationSelect);
            select.SelectByText(operation);
        }

        public void ClickCalculateButton()
        {
            CalculateButton.Click();
        }

        public string GetResult()
        {
            return Result.Text;
        }
    }
}