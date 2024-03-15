using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class RelationshipSenderReceiverTable
    {
        internal bool IsActive { get; set; }
        internal int SenderReceiverRelId { get; set; }
        internal int SenderId { get; set; }
        internal int ReceiverId { get; set; }
        internal int RelationshipTypeId { get; set; }
    }
}
