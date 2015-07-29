using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    [Table("User")]
    public class User
    {
        public User()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        [Required]
        public String UserName   { get; set; }
        [Required]
        public String UserPass   { get; set; }

        public Guid MedecinId { get; set; }
        [ForeignKey("MedecinId")]
        public virtual Medecin Medecin { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
