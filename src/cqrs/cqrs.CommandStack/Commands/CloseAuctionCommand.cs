﻿using cqrs.Messaging.Interfaces;

namespace cqrs.CommandStack.Commands
{
    public class CloseAuctionCommand : ICommand
    {
        public CloseAuctionCommand(string auctionId)
        {
            AuctionId = auctionId;
        }

        public string AuctionId { get; }
    }
}
