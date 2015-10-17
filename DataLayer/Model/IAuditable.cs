using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public interface IAuditable
    {
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
        Guid ModifiedBy { get; set; }
        Guid CreatedBy { get; set; }
        String MachineId { get; set; }
    }
}
