using MediatR;
using System;

namespace eShopEnterprise.Core.Messages
{
    public class Event : Message, INotification
    {
        public Event()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}
