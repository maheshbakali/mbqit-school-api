using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace School.BLL.Dto
{
    public class TeacherDto
    {
        public long TeacherId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public long SalutationId { get; set; }

        [Required]
        [MaxLength(10)]
        public string SalutationType { get; set; }
    }
}
