using AutoMapper;
using Microsoft.Extensions.Logging;
using School.BLL.Dto;
using School.DAL.Models;
using School.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL.Actions
{
    public class ClassActions : IClassActions
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ClassActions(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ClassDto>> GetAllAsync()
        {
            var classes = await Task.Run(() => _unitOfWork.ClassRepository.GetAll());
            var classDtoList = _mapper.Map<IEnumerable<ClassDto>>(classes);

            return classDtoList;
        }

        public async Task<ClassDto> GetAsync(long id)
        {
            var classObj = await Task.Run(() => _unitOfWork.ClassRepository.Get(id));
            var classDto = _mapper.Map<ClassDto>(classObj);

            return classDto;
        }

        public long Add(ClassDto entity)
        {
            var classObj = _mapper.Map<Class>(entity);
            var teacherObj = _mapper.Map<Teacher>(entity.Teacher);

            _unitOfWork.TeacherRepository.Add(teacherObj);
            _unitOfWork.Save();

            classObj.TeacherId = teacherObj.TeacherId;
            classObj.Teacher = null;

            _unitOfWork.ClassRepository.Add(classObj);
            _unitOfWork.Save();

            entity.ClassId = classObj.ClassId;
            entity.TeacherId = teacherObj.TeacherId;
            entity.Teacher.TeacherId = teacherObj.TeacherId;

            return classObj.ClassId;
        }

        public void Delete(long id)
        {
            _unitOfWork.ClassRepository.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(long id, ClassDto entity)
        {
            var classObj = _mapper.Map<Class>(entity);
            _unitOfWork.ClassRepository.Update(id, classObj);
            _unitOfWork.Save();
        }
    }
}
