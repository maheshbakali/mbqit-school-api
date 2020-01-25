using School.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL.Repository
{
    public class TeacherRepository : IDataRepository<Teacher>
    {
        private readonly SchoolContext _context = null;

        public TeacherRepository(SchoolContext context)
        {
            _context = context;
        }

        public void Add(Teacher entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            _context.Teachers.Add(entity);
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Teacher Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Teacher> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Teacher> GetAllByClassId(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(long id, Teacher entity)
        {
            throw new NotImplementedException();
        }
    }
}
