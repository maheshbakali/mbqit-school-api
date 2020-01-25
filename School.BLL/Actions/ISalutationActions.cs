using School.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL.Actions
{
    public interface ISalutationActions
    {
        Task<IEnumerable<SalutationDto>> GetAllAsync();
    }
}
