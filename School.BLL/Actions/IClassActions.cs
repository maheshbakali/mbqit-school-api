using School.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL.Actions
{
    public interface IClassActions
    {
        Task<IEnumerable<ClassDto>> GetAllAsync();
        Task<ClassDto> GetAsync(long id);
        long Add(ClassDto entity);
        void Update(long id, ClassDto entity);
        void Delete(long id);
    }
}
