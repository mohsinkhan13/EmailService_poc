﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Email.DomainModel
{
    [DataContract]
    public class EmailMessage : Message
    {
        //public EmailMessage()
        //{
        //    ContentType = "text/plain";
        //}
        [DataMember]
        public string From { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public List<string> To { get; set; }
        

    }

    [DataContract]
    public class Message
    {
        [DataMember]
        public string EmailContent { get; set; }
        [DataMember]
        public string ContentType { get; set; }

    }
}