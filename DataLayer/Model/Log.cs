using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    //todo farouk
    public class Log
    {
        public Log()
        {
            
        }

        public Guid LogId { get; set; }
        public String MachineId { get; set; }
        public OperationType Operation { get; set; }
    }

    public enum OperationType
    {
        
    }
}
