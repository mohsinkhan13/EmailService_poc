﻿using System.Threading;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Configuration;
using System;
using Email.DomainModel;
using Email.QueueManager;
using Email.Services;
using ConfigurationManager;

namespace Email.CloudService.WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        Action<EmailMessage> _callback = new Action<EmailMessage>(emailMessage => {
            IMessageConsumer<EmailMessage> consumer = new SendEmailMessageConsumer<EmailMessage>();
            consumer.Consume(emailMessage);
        });

        public override void Run()
        {
            using (var manager = QueueFactory.GetQueue())
            {
                manager.DefineMessageCallback(_callback);
                CompletedEvent.WaitOne();
            }
        }

        public override void OnStop()
        {
            CompletedEvent.Set();
            base.OnStop();
        }
    }
}
