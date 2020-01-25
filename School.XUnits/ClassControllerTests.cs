using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using School.API.Controllers;
using School.BLL.Actions;
using School.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace School.XUnits
{
    public class ClassControllerTests
    {
        private readonly Mock<IClassActions> _mockRepo;
        private readonly Mock<ILogger<ClassesController>> _mockLog;
        private readonly ClassesController _controller;

        public ClassControllerTests()
        {
            _mockRepo = new Mock<IClassActions>();
            _mockLog = new Mock<ILogger<ClassesController>>();
            _controller = new ClassesController(_mockLog.Object, _mockRepo.Object);
        }

        [Fact]
        public async void GetAllClasses_ReturnType()
        {
            // Arrange
            // Act
            var response = await _controller.GetAllClasses();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<ClassDto>>>(response);
        }

        [Fact]
        public async void GetAllClasses_ReturnClasses()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<ClassDto>() { new ClassDto(), new ClassDto() });

            // Act
            var response = await _controller.GetAllClasses();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var classes = Assert.IsType<List<ClassDto>>(okResult.Value);
            Assert.Equal(2, classes.Count);
        }

        [Fact]
        public async void GetClass_ReturnClass()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new ClassDto() { Location = "Aus" });

            // Act
            var response = await _controller.GetClass(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var classObj = Assert.IsType<ClassDto>(okResult.Value);
            Assert.Equal("Aus", classObj.Location);
        }

        [Fact]
        public async void GetClass_NotFound()
        {
            // Arrange
            ClassDto dto = null;
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(dto);

            // Act
            var response = await _controller.GetClass(It.IsAny<long>());

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(response.Result);

        }

        [Fact]
        public void CreateClass_BadRequest()
        {
            // Arrange
            ClassDto dto = new ClassDto { Location = "Aus", TeacherId = 1 };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.CreateClass(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void CreateClass_CreatedResponse()
        {
            // Arrange
            ClassDto dto = new ClassDto { Location = "Aus", TeacherId = 1, Name = "Maths" };

            // Act
            var createdResponse = _controller.CreateClass(dto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void CreateClass_ReturnResponse()
        {
            // Arrange
            ClassDto dto = new ClassDto { Location = "Aus", TeacherId = 1, Name = "Maths" };

            // Act
            var createdResponse = _controller.CreateClass(dto) as CreatedAtActionResult;
            var item = createdResponse.Value as ClassDto;

            // Assert
            Assert.IsType<ClassDto>(item);
            Assert.Equal("Aus", item.Location);
        }

        [Fact]
        public void DeleteClass_NotFound()
        {
            // Arrange
            ClassDto dto = null;
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(dto);

            // Act
            var badResponse = _controller.DeleteClass(It.IsAny<long>());

            // Assert
            Assert.IsType<NotFoundResult>(badResponse.Result);
        }

        [Fact]
        public void DeleteClass_ReturnsResponse()
        {
            // Arrange
            //ClassDto dto = null;
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new ClassDto());

            // Act
            var response = _controller.DeleteClass(It.IsAny<long>());

            // Assert
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public void UpdateClass_BadRequest()
        {
            // Arrange
            ClassDto dto = new ClassDto { Location = "Aus", TeacherId = 1 };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.UpdateClass(It.IsAny<long>(), dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        [Fact]
        public void UpdateClass_NotFound()
        {
            // Arrange
            ClassDto dto = null;
            ClassDto updateDto = new ClassDto { Location = "Aus", TeacherId = 1, Name = "Maths" };
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(dto);

            // Act
            var badResponse = _controller.UpdateClass(It.IsAny<long>(), updateDto);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse.Result);
        }

        [Fact]
        public void UpdateClass_ReturnsResponse()
        {
            // Arrange            
            _mockRepo.Setup(repo => repo.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new ClassDto());

            // Act
            var response = _controller.UpdateClass(It.IsAny<long>(), new ClassDto());

            // Assert
            Assert.IsType<NoContentResult>(response.Result);
        }

    }
}
