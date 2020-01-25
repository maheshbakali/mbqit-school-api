using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace School.BLL.Dto
{
    public class SalutationDto
    {
        public long SalutationId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
