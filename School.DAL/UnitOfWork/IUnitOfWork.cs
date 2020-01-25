using School.DAL.Models;
using School.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDataRepository<Class> ClassRepository { get; }
        IDataRepository<Student> StudentRepository { get; }
        IDataRepository<Teacher> TeacherRepository { get; }
        IDataRepository<Salutation> SalutationRepository { get; }
        void Save();
    }
}
