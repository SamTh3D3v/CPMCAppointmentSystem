using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    [Table("RendezVous")]
    public class RendezVous
    {
        public RendezVous()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RendezVousId { get; set; }
        [Required]
        public DateTime DateTimeRdv { get; set; }
        [Required]
        public String LieuRdv { get; set; }        

        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
    }
}
