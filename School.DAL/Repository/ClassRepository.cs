using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.DAL.Models;

namespace School.DAL.Repository
{
    public class ClassRepository : IDataRepository<Class>
    {
        private readonly SchoolContext _context = null;

        public ClassRepository(SchoolContext context)
        {
            _context = context;
        }

        public IEnumerable<Class> GetAll()
        {
            return _context.Classes
                .Include(c => c.Teacher)
                .ThenInclude(t => t.Salutation)
                .ToList();
        }

        public Class Get(long id)
        {
            return _context.Classes.FirstOrDefault(e => e.ClassId == id);
        }

        public void Add(Class entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            _context.Classes.Add(entity);
        }

        public void Update(long id, Class entity)
        {
            var existingEntity = _context.Classes.Single(e => e.ClassId == id);            

            existingEntity.Location = entity.Location;
            existingEntity.Name = entity.Name;
            existingEntity.Teacher = entity.Teacher;
            existingEntity.UpdatedDate = DateTime.UtcNow;
        }

        public void Delete(long id)
        {
            var existingEntity = _context.Classes.Single(e => e.ClassId == id);
            _context.Classes.Remove(existingEntity);
        }

        public IEnumerable<Class> GetAllByClassId(long id)
        {
            throw new NotImplementedException();
        }
    }
}
