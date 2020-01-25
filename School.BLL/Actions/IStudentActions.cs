using School.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL.Actions
{
    public interface IStudentActions
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<IEnumerable<StudentDto>> GetAllByClassId(long id);
        Task<StudentDto> GetAsync(long id);
        long Add(StudentDto entity);
        void Update(long id, StudentDto entity);
        void Delete(long id);
    }
}
