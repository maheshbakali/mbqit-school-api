using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using School.API.Controllers;
using School.BLL.Actions;
using School.BLL.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace School.XUnits
{
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentActions> _mockRepo;
        private readonly Mock<ILogger<StudentsController>> _mockLog;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _mockRepo = new Mock<IStudentActions>();
            _mockLog = new Mock<ILogger<StudentsController>>();
            _controller = new StudentsController(_mockLog.Object, _mockRepo.Object);
        }

        [Fact]
        public async void GetAllStudents_ReturnType()
        {
            // Arrange
            // Act
            var response = await _controller.GetAllStudents();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<StudentDto>>>(response);
        }

        [Fact]
        public async void GetAllStudents_ReturnStudents()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<StudentDto>() { new StudentDto(), new StudentDto() });

            // Act
            var response = await _controller.GetAllStudents();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var students = Assert.IsType<List<StudentDto>>(okResult.Value);
            Assert.Equal(2, students.Count);
        }

        [Fact]
        public async void GetAllStudentsByClassId_ReturnStudents()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllByClassId(It.IsAny<long>()))
                .ReturnsAsync(new List<StudentDto>() { new StudentDto(), new StudentDto() });

            // Act
            var response = await _controller.GetAllStudentsByClassId(It.IsAny<long>());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var students = Assert.IsType<List<StudentDto>>(okResult.Value);
            Assert.Equal(2, students.Count);
        }

        [Fact]
        public async void GetStudent_ReturnStudent()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new StudentDto() { Age = 30 });

            // Act
            var response = await _controller.GetStudent(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var student = Assert.IsType<StudentDto>(okResult.Value);
            Assert.Equal(30, student.Age);
        }

        [Fact]
        public async void GetStudent_NotFound()
        {
            // Arrange
            StudentDto dto = null;
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(dto);

            // Act
            var response = await _controller.GetStudent(It.IsAny<long>());

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(response.Result);

        }

        [Fact]
        public void CreateStudent_BadRequest()
        {
            // Arrange
            StudentDto dto = new StudentDto { Age = 1, ClassId = 1, GPA = 1, LastName = "aaa" };
            _controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var badResponse = _controller.CreateStudent(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void CreateStudent_CreatedResponse()
        {
            // Arrange
            StudentDto dto = new StudentDto { Age = 1, FirstName = "ppp", ClassId = 1, GPA = 1, LastName = "aaa" };

            // Act
            var createdResponse = _controller.CreateStudent(dto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void CreateStudent_ReturnResponse()
        {
            // Arrange
            StudentDto dto = new StudentDto { Age = 1, FirstName = "ppp", ClassId = 1, GPA = 1, LastName = "aaa" };

            // Act
            var createdResponse = _controller.CreateStudent(dto) as CreatedAtActionResult;
            var item = createdResponse.Value as StudentDto;

            // Assert
            Assert.IsType<StudentDto>(item);
            Assert.Equal("ppp", item.FirstName);
        }

        [Fact]
        public void DeleteStudent_NotFound()
        {
            // Arrange
            StudentDto dto = null;
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(dto);

            // Act
            var badResponse = _controller.DeleteStudent(It.IsAny<long>());

            // Assert
            Assert.IsType<NotFoundResult>(badResponse.Result);
        }

        [Fact]
        public void DeleteStudent_ReturnsResponse()
        {
            // Arrange            
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new StudentDto());

            // Act
            var response = _controller.DeleteStudent(It.IsAny<long>());

            // Assert
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public void UpdateStudent_BadRequest()
        {
            // Arrange
            StudentDto dto = new StudentDto { Age = 1, ClassId = 1, GPA = 1, LastName = "aaa" };
            _controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var badResponse = _controller.UpdateStudent(It.IsAny<long>(), dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        [Fact]
        public void UpdateStudent_NotFound()
        {
            // Arrange
            StudentDto dto = null;
            StudentDto updateDto = new StudentDto { Age = 1, ClassId = 1, FirstName = "aaa", GPA = 2.9M, LastName = "ttt", StudentId = 1 };
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(dto);

            // Act
            var badResponse = _controller.UpdateStudent(It.IsAny<long>(), updateDto);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse.Result);
        }

        [Fact]
        public void UpdateStudent_ReturnsResponse()
        {
            // Arrange            
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new StudentDto());

            // Act
            var response = _controller.UpdateStudent(It.IsAny<long>(), new StudentDto());

            // Assert
            Assert.IsType<NoContentResult>(response.Result);
        }

    }
}
