using AutoMapper;
using School.BLL.Dto;
using School.DAL.Models;
using School.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL.Actions
{
    public class StudentActions : IStudentActions
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentActions(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await Task.Run(() => _unitOfWork.StudentRepository.GetAll());
            var studentDtoList = _mapper.Map<IEnumerable<StudentDto>>(students);

            return studentDtoList;
        }

        public async Task<IEnumerable<StudentDto>> GetAllByClassId(long id)
        {
            var students = await Task.Run(() => _unitOfWork.StudentRepository.GetAllByClassId(id));
            var studentDtoList = _mapper.Map<IEnumerable<StudentDto>>(students);

            return studentDtoList;
        }

        public async Task<StudentDto> GetAsync(long id)
        {
            var studentObj = await Task.Run(() => _unitOfWork.StudentRepository.Get(id));
            var studentDto = _mapper.Map<StudentDto>(studentObj);

            return studentDto;
        }

        public long Add(StudentDto entity)
        {
            var students = _unitOfWork.StudentRepository.GetAllByClassId(entity.ClassId).ToList();
            if(students.Any(e => e.LastName.Equals(entity.LastName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Surname must be unique within class.");
            }
            
            var studentObj = _mapper.Map<Student>(entity);
            _unitOfWork.StudentRepository.Add(studentObj);
            _unitOfWork.Save();

            return studentObj.ClassId;
        }

        public void Delete(long id)
        {
            _unitOfWork.StudentRepository.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(long id, StudentDto entity)
        {
            var studentObj = _mapper.Map<Student>(entity);
            _unitOfWork.StudentRepository.Update(id, studentObj);
            _unitOfWork.Save();
        }


    }
}
