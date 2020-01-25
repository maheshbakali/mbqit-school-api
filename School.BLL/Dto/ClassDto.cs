using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace School.BLL.Dto
{
    public class ClassDto
    {
        public long ClassId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Location { get; set; }

        [Required]
        public long TeacherId { get; set; }
        public TeacherDto Teacher { get; set; }
    }
}
