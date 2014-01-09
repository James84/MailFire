using System.Collections.Generic;
using MailFire.DTOs;

namespace MailFire
{
    public interface IMessaging
    {
        IEnumerable<MessageDTO> GetMessages(string hostname, int port, bool useSsl, string username, string password, int? maxNumber = null);
    }
}
