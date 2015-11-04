using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    //todo farouk
    public class Trace
    {        
        public Guid Id { get; set; }
        public String Machine { get; set; }
        public AuditAction Action { get; set; }

        public string Message { get; set; }

        public string ActionName { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public Guid EntityId { get; set; }

        public string EntitySet { get; set; }

        public Guid? ParentEntityId { get; set; }

        public string ParentEntitySet { get; set; }
    }  
}
