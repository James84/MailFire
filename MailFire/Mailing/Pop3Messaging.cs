using System;
using System.Collections.Generic;
using System.IO;
using MailFire.DTOs;

namespace MailFire.Mailing
{
    public class Pop3Messaging : IMessaging
    {
        public IEnumerable<MessageDTO> GetMessages(string hostname, int port, bool useSsl, string username, string password, int? maxNumber = 100)
        {
            var allMessages = new List<MessageDTO>(); 

            try
            {
                using (var pop3Client = new AE.Net.Mail.Pop3Client(hostname, username, password, port, useSsl))
                {
                    var maxCount = pop3Client.GetMessageCount();
                    var messageCount = (maxNumber.HasValue && maxNumber < maxCount) ? (maxCount - maxNumber.Value) : 0;
                    
                    for (var i = (maxCount - 1); i > messageCount; i--)
                    {
                        var message = pop3Client.GetMessage(i);
                        allMessages.Add(new MessageDTO
                            {
                                Body = message.Body,
                                DateSent = message.Date,
                                DisplayName = message.From.DisplayName,
                                From = message.From.Address,
                                Id = message.MessageID,
                                Subject = message.Subject,
                                Uid = message.Uid
                            });
                    }

                }
            }
            catch(Exception ex)
            {
                var path = Path.GetTempPath();
                var stream = File.OpenWrite(Path.Combine(path, "mailFireErrorLog.log"));

                using (var fileWriter = new StreamWriter(stream))
                {
                    fileWriter.Write(string.Format("Exception occurred: {0} - at {1}", ex.Message, DateTime.Now));
                }
            }

            return allMessages;
        }
    }
}