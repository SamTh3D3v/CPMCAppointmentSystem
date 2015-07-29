using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    [Table("Pathology")]
    public class Pathology
    {
        public Pathology()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PathologyId { get; set; }
        [Required]
        public String CodePathology { get; set; }
        [Required]
        public String NomPathology { get; set; }
        public virtual ICollection<Medecin> Medecins { get; set; }
    }
}
