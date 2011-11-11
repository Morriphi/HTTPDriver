using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;

namespace HTTPDriver.FunctionalTests
{
    public class NavigationTests
    {
        [Test]
        public void ShouldBeAbleToNavigateToWebSite()
        {
            //Given
            var driver = new HttpDriver(new WebRequester());

            //When
            driver.Navigate().GoToUrl("http://www.google.com");
            //Then
            Assert.That(driver.Title, Is.EqualTo("Google"));
        }
    }
}
