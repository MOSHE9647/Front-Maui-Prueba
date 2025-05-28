using MauiAppMovil.Models;
using MauiAppMovil.Services;

namespace tests
{
    public class CreateCourseTest
    {

        private readonly CourseService _courseService;
        private readonly Stream _testImageStream;

        public CreateCourseTest()
        {
            _courseService = TestHelpers.GetCourseService();
            _testImageStream = TestHelpers.GetLocalImageStream();
        }

        [Fact]
        public async Task CreateCourse_HappyPath_ReturnsIsSuccessStatusCode()
        {
            // Arrange: Create a new course object
            var newCourse = new Course
            {
                Name = "Test Course",
                Description = "This is a test course.",
                Schedule = "Monday to Friday, 10:00 AM - 12:00 PM",
                Professor = "John Doe"
            };

            // Act: Call the method to create the course
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.CreateCourseWithResponseAsync(newCourse, imageStream, imageName);

            // Assert: Check if the response is successful
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CreateCourse_InvalidName_ReturnsBadRequest()
        {
            // Arrange: Create a new course object with an empty name
            var newCourse = new Course
            {
                Name = string.Empty, // Invalid name
                Description = "This is a test course.",
                Schedule = "Monday to Friday, 10:00 AM - 12:00 PM",
                Professor = "John Doe"
            };

            // Act: Call the method to create the course
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.CreateCourseWithResponseAsync(newCourse, imageStream, imageName);
            
            // Assert: Check if the response is BadRequest
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task CreateCourse_InvalidDescription_ReturnsBadRequest()
        {
            // Arrange: Create a new course object with an empty description
            var newCourse = new Course
            {
                Name = "Test Course",
                Description = string.Empty, // Invalid description
                Schedule = "Monday to Friday, 10:00 AM - 12:00 PM",
                Professor = "John Doe"
            };
            
            // Act: Call the method to create the course
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.CreateCourseWithResponseAsync(newCourse, imageStream, imageName);
            
            // Assert: Check if the response is BadRequest
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task CreateCourse_InvalidFile_ReturnsBadRequest()
        {
            // Arrange: Create a new course object with valid data but an invalid file (null stream)
            var newCourse = new Course
            {
                Name = "Test Course",
                Description = "This is a test course.",
                Schedule = "Monday to Friday, 10:00 AM - 12:00 PM",
                Professor = "John Doe"
            };

            // Act: Call the method to create the course with a null image stream
            using var imageStream = null as Stream; // Invalid image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.CreateCourseWithResponseAsync(newCourse, imageStream, imageName);
            
            // Assert: Check if the response is BadRequest
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task CreateCourse_InvalidSchedule_ReturnsBadRequest()
        {
            // Arrange: Create a new course object with an invalid schedule (empty string)
            var newCourse = new Course
            {
                Name = "Test Course",
                Description = "This is a test course.",
                Schedule = string.Empty, // Invalid schedule
                Professor = "John Doe"
            };

            // Act: Call the method to create the course
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.CreateCourseWithResponseAsync(newCourse, imageStream, imageName);
            
            // Assert: Check if the response is BadRequest
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task CreateCourse_InvalidProfessor_ReturnsBadRequest()
        {
            // Arrange: Create a new course object with an invalid professor (empty string)
            var newCourse = new Course
            {
                Name = "Test Course",
                Description = "This is a test course.",
                Schedule = "Monday to Friday, 10:00 AM - 12:00 PM",
                Professor = string.Empty // Invalid professor
            };

            // Act: Call the method to create the course
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.CreateCourseWithResponseAsync(newCourse, imageStream, imageName);
            
            // Assert: Check if the response is BadRequest
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

    }
}
