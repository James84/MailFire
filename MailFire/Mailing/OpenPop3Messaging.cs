using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MailFire.DTOs;
using OpenPop.Pop3;

namespace MailFire.Mailing
{
    public class OpenPop3Messaging : IMessaging
    {
        public IEnumerable<MessageDTO> GetMessages(string hostname, int port, bool useSsl, string username,
                                                   string password, int? maxNumber = null)
        {
            var allMessages = new List<MessageDTO>();

            try
            {
                // The client disconnects from the server when being disposed
                using (var client = new Pop3Client())
                {
                    client.Connect(hostname, port, useSsl);

                    client.Authenticate(username, password, AuthenticationMethod.UsernameAndPassword);

                    var maxCount = client.GetMessageCount();

                    //work out subset to take from messages
                    var messageCount = (maxNumber.HasValue && maxNumber < maxCount) ? (maxCount - maxNumber.Value) : 0;


                    //message list is 1 based
                    //loop written backwards so messages are in correct order
                    for (var i = maxCount; i > messageCount; i--)
                    {
                        var currentMessage = client.GetMessage(i);
                        var html = currentMessage.FindFirstHtmlVersion();

                        allMessages.Add(new MessageDTO
                            {
                                Body =
                                    html != null && html.Body != null
                                        ? Encoding.UTF8.GetString(html.Body)
                                        : string.Empty,
                                DateSent = currentMessage.Headers.DateSent,
                                DisplayName = currentMessage.Headers.From.DisplayName,
                                From = currentMessage.Headers.From.Address,
                                Id = currentMessage.Headers.MessageId,
                                Subject = currentMessage.Headers.Subject,
                                Uid = currentMessage.Headers.MessageId
                            });


                    }

                }
            }
            catch (Exception ex)
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