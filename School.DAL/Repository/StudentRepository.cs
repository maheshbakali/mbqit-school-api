using School.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.DAL.Repository
{
    public class StudentRepository : IDataRepository<Student>
    {
        private readonly SchoolContext _context = null;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public Student Get(long id)
        {
            return _context.Students.FirstOrDefault(s => s.StudentId == id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public IEnumerable<Student> GetAllByClassId(long id)
        {
            return _context.Students.Where(s => s.ClassId == id).ToList();
        }

        public void Add(Student entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            _context.Students.Add(entity);
        }

        public void Delete(long id)
        {
            var existingEntity = _context.Students.Single(e => e.StudentId == id);
            _context.Students.Remove(existingEntity);
        }

        public void Update(long id, Student entity)
        {
            var existingEntity = _context.Students.Single(e => e.StudentId == id);

            existingEntity.Age = entity.Age;
            existingEntity.FirstName = entity.FirstName;
            existingEntity.LastName = entity.LastName;
            existingEntity.GPA = entity.GPA;
            existingEntity.UpdatedDate = DateTime.UtcNow;
        }
    }
}
