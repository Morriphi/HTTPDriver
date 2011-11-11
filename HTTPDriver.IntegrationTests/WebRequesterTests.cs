using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HTTPDriver.IntegrationTests
{
    public class WebRequesterTests
    {
        [Test]
        public void ShouldReturnWebResponder()
        {
            //Given
            var requester = new WebRequester();
            //When
            var webResponder = requester.Request("http://www.google.com");
            
            //Then
            Assert.That(webResponder, Is.Not.Null);
            Assert.That(webResponder, Is.InstanceOf<WebResponder>());
            
        }

    }
}
