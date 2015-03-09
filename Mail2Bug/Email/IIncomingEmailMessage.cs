﻿using System;
using System.Collections.Generic;

namespace Mail2Bug.Email
{
    /// <summary>
    /// This interface is an adapter for incoming email messages, exposing properties and actions relevant to the
    /// operation of Mail2Bug.
    /// This is useful for abstracting away the underlying layer that actually handles the email messages, allowing us to
    /// change it over time without major rewrites of the actual logic, which should be mostly decoupled from the details of
    /// how email messages are retrieved, parsed etc.
    /// 
    /// I am introducing this to abstract away the current usage of DotMAPI, which is very unstable, and allow me to replace
    /// it with usage of EWS (Exchange Web Services).
    /// </summary>
    public interface IIncomingEmailMessage
    {
        string Subject { get; }
        string RawBody { get; }
        string PlainTextBody { get; }
        string ConversationIndex { get; }
        string ConversationTopic { get; }
        
        string SenderName { get; }
        string SenderAlias { get; }
        string SenderAddress { get; }
        IEnumerable<string> To { get; }
        IEnumerable<string> Cc { get; }
        DateTime SentOn { get; }

        bool IsHtmlBody { get; }

        string Location { get; }
        DateTime? StartTime { get; }
        DateTime? EndTime { get; }

        string SaveToFile();
        string SaveToFile(string filename);

        /// <summary>
        /// Retrieves the text of the latest message in the email thread.
        /// This is different from the 'Body', because it is only the text of the latest message, without the text of previous
        /// messages dropped, whereas the body includes all of that "history".
        /// </summary>
        /// <returns></returns>
        string GetLastMessageText();

        /// <summary>
        /// Deletes the message
        /// </summary>
        void Delete();

        IEnumerable<IIncomingEmailAttachment> Attachments { get; }
    }
}