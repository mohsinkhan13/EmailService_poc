﻿using Email.Services;
using System;
using System.Collections.Generic;
using Email.QueueManager;
using Email.DomainModel;

namespace Email.Test.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var qm = QueueFactory.GetQueue<EmailMessage>())
            {
                var email = new EmailMessage
                {
                    From = "mohsin.khan@wolterskluwer.com",
                    To = new List<string> { "mohsink13@gmail.com" },
                    EmailContent = "Test content from console after adding generic Queue class",
                    ContentType = EmailContentType.TextHtml,
                    Subject = "Test email using QueueFactory Azure Queue!!!"
                };

                qm.Enqueue(email);
            }

            Console.Read();
        }
    }
}
