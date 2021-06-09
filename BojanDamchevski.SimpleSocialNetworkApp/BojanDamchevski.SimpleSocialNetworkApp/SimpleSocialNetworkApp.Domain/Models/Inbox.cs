using SimpleSocialNetworkApp.Domain.Interfaces;
using System;

namespace SimpleSocialNetworkApp.Domain.Models
{
    public class Inbox : IInbox
    {
        public string Message { get; set; }
        public User FromUser { get; set; }
        public DateTime DateAndTime { get; set; }
        public Inbox(string message, User from, DateTime dateAndTime)
        {
            Message = message;
            FromUser = from;
            DateAndTime = dateAndTime;
        }
        public string PrintMessages()
        {
            return $"From: {FromUser.PersonalInfo.FirstName} {FromUser.PersonalInfo.LastName}:\n{Message}\nSent on {DateAndTime}.\n";
        }
    }
}
