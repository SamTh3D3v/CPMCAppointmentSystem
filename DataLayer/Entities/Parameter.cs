using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Parameter
    {
        public Guid Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Title { get; set; }
        [MaxLength(256)]
        public string Value { get; set; }

        public bool SyncOnChange { get; set; }
    }
}
