using School.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.DAL.Repository
{
    public class SalutationRepository : IDataRepository<Salutation>
    {
        private readonly SchoolContext _context = null;

        public SalutationRepository(SchoolContext context)
        {
            _context = context;
        }

        public IEnumerable<Salutation> GetAll()
        {
            return _context.Salutations.ToList();
        }

        public void Add(Salutation entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Salutation Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Salutation> GetAllByClassId(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(long id, Salutation entity)
        {
            throw new NotImplementedException();
        }
    }
}
