using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;

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

            Assert.AreEqual("7", calculatorPage.GetResult());
        }

        [Test]
        public void SubtractionTest()
        {
            calculatorPage.EnterValueA("5");
            calculatorPage.DecrementA();
            calculatorPage.SelectOperation("-");
            calculatorPage.EnterValueB("3");
            calculatorPage.DecrementB();

            Assert.AreEqual("2", calculatorPage.GetResult());
        }

        [Test]
        public void MultiplicationTest()
        {
            calculatorPage.EnterValueA("4");
            calculatorPage.IncrementA();
            calculatorPage.SelectOperation("*");
            calculatorPage.EnterValueB("3");
            calculatorPage.IncrementB();
            Assert.AreEqual("20", calculatorPage.GetResult());
        }

        [Test]
        public void DivisionTest()
        {
            calculatorPage.EnterValueA("5");
            calculatorPage.IncrementA();
            calculatorPage.EnterValueB("2");
            calculatorPage.SelectOperation("/");

            Assert.AreEqual("3", calculatorPage.GetResult());
        }
        [Test]
        public void NullTest()
        {
            calculatorPage.EnterValueA("5");
            calculatorPage.EnterValueB("0");
            calculatorPage.SelectOperation("/");

            Assert.AreEqual("null", calculatorPage.GetResult());
        }
        [Test]
        public void NegativeTest()
        {
            calculatorPage.EnterValueA("5");
            calculatorPage.EnterValueB("-5");
            calculatorPage.DecrementB();
            calculatorPage.SelectOperation("+");

            Assert.AreEqual("-1", calculatorPage.GetResult());
        }
        [Test]
        public void NegativeTest2()
        {
            calculatorPage.EnterValueA("-10");
            calculatorPage.DecrementA();
            calculatorPage.EnterValueB("5");
            calculatorPage.SelectOperation("+");

            Assert.AreEqual("-6", calculatorPage.GetResult());
        }
        [Test]
        public void DecimalDivisionTest()
        {
            calculatorPage.EnterValueA("7");
            calculatorPage.SelectOperation("/");
            calculatorPage.EnterValueB("3");

            Assert.AreEqual("2.3333333333333335", calculatorPage.GetResult());
        }
        [Test]
        public void doubleTest()
        {
            calculatorPage.EnterValueA("5");
            calculatorPage.SelectOperation("/");
            calculatorPage.EnterValueB("2");

            Assert.AreEqual("2.5", calculatorPage.GetResult());
        }
        [Test]
        public void doubleTest2()
        {
            calculatorPage.EnterValueA("10.5");
            calculatorPage.SelectOperation("/");
            calculatorPage.EnterValueB("1.2");

            Assert.AreEqual("8.75", calculatorPage.GetResult());
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
        private IWebElement Result => driver.FindElement(By.XPath("//b[contains(@class, 'result')]"));

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


        public string GetResult()
        {
            string resultText = Result.Text;
            string[] parts = resultText.Split('=');
            string numericResult = parts[1].Trim();
            return numericResult;
        }
    }
}
