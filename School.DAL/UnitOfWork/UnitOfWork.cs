using School.DAL.Models;
using School.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SchoolContext _context = null;
        private bool disposed = false;

        public UnitOfWork(SchoolContext context,
            IDataRepository<Class> classRepository,
            IDataRepository<Student> studentRepository,
            IDataRepository<Teacher> teacherRepository,
            IDataRepository<Salutation> salutationRepository
            )
        {
            _context = context;
            ClassRepository = classRepository;
            StudentRepository = studentRepository;
            TeacherRepository = teacherRepository;
            SalutationRepository = salutationRepository;
        }

        public IDataRepository<Class> ClassRepository { get; }

        public IDataRepository<Student> StudentRepository { get; }

        public IDataRepository<Teacher> TeacherRepository { get; }

        public IDataRepository<Salutation> SalutationRepository { get; }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
