using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    [Table("Adresse")]
    public class Adresse
    {
        public Adresse()
        {
            
        }
        [Key,ForeignKey("Patient")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AdressId { get; set; }                     
        public Guid PatientId { get; set; }  
        [Required]     
        public string AddressDesignation { get; set; }                        
        public string City { get; set; }
        [Required]   
        public String CodePosatal { get; set; }
        [Required]
        public string Pays { get; set; }
        [Required]
        public int WillayaId { get; set; }
        [ForeignKey("WillayaId")]                
        public virtual Willaya Willaya { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
        
    }
}
