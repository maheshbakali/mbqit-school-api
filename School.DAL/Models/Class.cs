using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace School.DAL.Models
{
    [Table("Class", Schema = "School")]
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ClassId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Location { get; set; }
        
        public ICollection<Student> Students { get; set; }

        public long? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Required]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
