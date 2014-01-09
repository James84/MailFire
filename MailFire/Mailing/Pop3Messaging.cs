using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailFire.Mailing
{
    public class Pop3Messaging : IMessaging
    {
        public IEnumerable<DTOs.MessageDTO> GetMessages(string hostname, int port, bool useSsl, string username, string password, int? maxNumber = null)
        {
            throw new NotImplementedException();
        }
    }
}