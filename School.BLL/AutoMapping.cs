using AutoMapper;
using School.BLL.Dto;
using School.DAL.Models;
using System;

namespace School.BLL
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Class, ClassDto>();
            CreateMap<ClassDto, Class>();

            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();

            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto, Teacher>();

            CreateMap<Salutation, SalutationDto>();
            CreateMap<SalutationDto, Salutation>();
        }
    }
}
