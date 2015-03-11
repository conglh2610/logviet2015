using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class MessageHistory
    {
        [Key]
        public Guid Id { get; set; }

        public string Message { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }

        [ForeignKey("FromUserId")]
        public User FromUser { get; set; }

        [ForeignKey("ToUserId")]
        public User ToUser { get; set; }

        [DefaultValue(MessageStatus.Unread)]
        public MessageStatus Status { get; set; } 
         
        public DateTime CreateDate { get; set; }
    }
}
