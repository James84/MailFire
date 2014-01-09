using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailFire.DTOs;
using MailFire.Mailing;
using NUnit.Framework;

namespace MailFire.Tests
{
    [TestFixture]
    public class OpenPop3UnitTests
    {
        /*james_test@outlook.com*/

        const string username = "james_test@outlook.com";
        const string password = "drainp1pe";
        const string hostname = "pop3.live.com";
        const int port        = 995;
        const bool useSSL     = true;

        private OpenPop3Messaging client;

        [SetUp]
        public void SetUp()
        {
            client = new OpenPop3Messaging();
        }

        [Test]
        public void  GetMessagesCalled_IEnumerable_MessageDTO_Returned()
        {
            var messages = client.GetMessages(hostname, port, useSSL, username, password, 5);

            Assert.IsInstanceOf<IEnumerable<MessageDTO>>(messages, "IEnumerable<MessageDTO> was not returned");
        }

        [Test]
        public void GetMessagesCalled_MaxLimitPassedIn_CorrectAmountReturned()
        {
            const int limit = 5;
            var messages = client.GetMessages(hostname, port, useSSL, username, password, limit).ToList();

            Assert.AreEqual(messages.Count(), limit, string.Format("Incorrect number of emails: Should have been {0} but {1} returned.", limit, messages.Count()));
        }

        [Test]
        public void GetMessagesCalled_NoLimitPassedIn_MaxOf100Returned()
        {
            var messages = client.GetMessages(hostname, port, useSSL, username, password).ToList();

            Assert.IsTrue(messages.Count() <= 100, "More than 100 results returned");
        }

        [TearDown]
        public void TearDown()
        {
            client = null;
        }
    }
}
