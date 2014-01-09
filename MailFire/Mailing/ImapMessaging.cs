using System;
using System.Collections.Generic;
using MailFire.DTOs;

namespace MailFire.Mailing
{
    public class ImapMessaging : IMessaging
    {
        public IEnumerable<MessageDTO> GetMessages(string hostname, int port, bool useSsl, string username, string password, int? maxNumber = null)
        {
            throw new NotImplementedException();
        }
    }
}