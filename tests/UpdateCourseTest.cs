using MauiAppMovil.Models;
using MauiAppMovil.Services;

namespace tests
{
    public class UpdateCourseTest
    {
        private readonly CourseService _courseService;
        private readonly Stream _testImageStream;

        public UpdateCourseTest()
        {
            _courseService = TestHelpers.GetCourseService();
            _testImageStream = TestHelpers.GetLocalImageStream();
        }

        [Fact]
        public async Task UpdateCourse_HappyPath_ReturnsIsSuccessStatusCode()
        {
            // Arrange: Get the list of courses
            var courses = await _courseService.GetCoursesAsync(); 
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image

            // If there are no courses, create one for testing
            if (courses == null || courses.Count == 0)
            {
                var newCourse = new Course
                {
                    Name = "Curso de prueba",
                    Description = "Descripción de prueba",
                    Schedule = "Lunes 10:00",
                    Professor = "Profesor Prueba"
                };

                var createResult = await _courseService.CreateCourseWithResponseAsync(newCourse, imageStream, imageName);

                // Verify that the course was created successfully
                Assert.True(createResult.IsSuccessStatusCode);

                // Get the updated list of courses
                courses = await _courseService.GetCoursesAsync();
            }

            Assert.NotEmpty(courses); // Now we should have at least one course
            var courseToUpdate = new Course
            {
                Id = courses.First().Id, // Assuming this ID exists in the test database
                Name = "Updated Course",
                Description = "This is an updated test course.",
                Schedule = "Monday to Friday, 1:00 PM - 3:00 PM",
                Professor = "Jane Doe"
            };

            // Act: Call the method to update the course
            var result = await _courseService.UpdateCourseWithImageAsync(courseToUpdate, imageStream, imageName);
            
            // Assert: Check if the response is successful
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdateCourse_InvalidId_ReturnsNotFound()
        {
            // Arrange: Create a course object with an invalid ID
            var courseToUpdate = new Course
            {
                Id = int.MaxValue,
                Name = "Non-existent Course",
                Description = "This course does not exist.",
                Schedule = "Monday to Friday, 4:00 PM - 6:00 PM",
                Professor = "Unknown"
            };

            // Act: Call the method to update the course
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.UpdateCourseWithImageAsync(courseToUpdate, imageStream, imageName);
            
            // Assert: Check if the response is NotFound
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task UpdateCourse_InvalidName_ReturnsBadRequest()
        {
            // Arrange: Create a course object with an invalid name
            var courseToUpdate = new Course
            {
                Id = 1, // Assuming this ID exists in the test database
                Name = string.Empty, // Invalid name
                Description = "This is an updated test course with an invalid name.",
                Schedule = "Monday to Friday, 1:00 PM - 3:00 PM",
                Professor = "Jane Doe"
            };

            // Act: Call the method to update the course
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.UpdateCourseWithImageAsync(courseToUpdate, imageStream, imageName);
            
            // Assert: Check if the response is BadRequest
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task UpdateCourse_InvalidDescription_ReturnsBadRequest()
        {
            // Arrange: Create a course object with an invalid description
            var courseToUpdate = new Course
            {
                Id = 1, // Assuming this ID exists in the test database
                Name = "Valid Course Name",
                Description = string.Empty, // Invalid description
                Schedule = "Monday to Friday, 1:00 PM - 3:00 PM",
                Professor = "Jane Doe"
            };

            // Act: Call the method to update the course
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.UpdateCourseWithImageAsync(courseToUpdate, imageStream, imageName);
            
            // Assert: Check if the response is BadRequest
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task UpdateCourse_NoFile_ReturnsIsSuccessStatusCode()
        {
            // Arrange: Get the list of courses
            var courses = await _courseService.GetCoursesAsync();
            var imageStream = TestHelpers.GetLocalImageStream(); // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image

            // If there are no courses, create one for testing
            if (courses == null || courses.Count == 0)
            {
                var newCourse = new Course
                {
                    Name = "Curso de prueba",
                    Description = "Descripción de prueba",
                    Schedule = "Lunes 10:00",
                    Professor = "Profesor Prueba"
                };

                var createResult = await _courseService.CreateCourseWithResponseAsync(newCourse, imageStream, imageName);

                // Verify that the course was created successfully
                Assert.True(createResult.IsSuccessStatusCode);

                // Get the updated list of courses
                courses = await _courseService.GetCoursesAsync();
            }

            Assert.NotEmpty(courses); // Now we should have at least one course
            var courseToUpdate = new Course
            {
                Id = courses.First().Id, // Assuming this ID exists in the test database
                Name = "Updated Course Without Image",
                Description = "This is an updated test course without an image.",
                Schedule = "Monday to Friday, 1:00 PM - 3:00 PM",
                Professor = "Jane Doe"
            };

            // Act: Call the method to update the course without an image
            var result = await _courseService.UpdateCourseWithImageAsync(courseToUpdate, null, null);

            // Assert: Check if the response is successful
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine($"StatusCode: {result.StatusCode}");
                Console.WriteLine(await result.Content.ReadAsStringAsync());
            }
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdateCourse_InvalidSchedule_ReturnsBadRequest()
        {
            // Arrange: Create a course object with an invalid schedule
            var courseToUpdate = new Course
            {
                Id = 1, // Assuming this ID exists in the test database
                Name = "Valid Course Name",
                Description = "This is an updated test course with an invalid schedule.",
                Schedule = string.Empty, // Invalid schedule
                Professor = "Jane Doe"
            };

            // Act: Call the method to update the course
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.UpdateCourseWithImageAsync(courseToUpdate, imageStream, imageName);
            
            // Assert: Check if the response is BadRequest
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task UpdateCourse_InvalidProfessor_ReturnsBadRequest()
        {
            // Arrange: Create a course object with an invalid professor name
            var courseToUpdate = new Course
            {
                Id = 1, // Assuming this ID exists in the test database
                Name = "Valid Course Name",
                Description = "This is an updated test course with an invalid professor.",
                Schedule = "Monday to Friday, 1:00 PM - 3:00 PM",
                Professor = string.Empty // Invalid professor name
            };

            // Act: Call the method to update the course
            using var imageStream = _testImageStream; // Use the test image stream
            var imageName = TestHelpers.ImageName; // Name for the test image
            var result = await _courseService.UpdateCourseWithImageAsync(courseToUpdate, imageStream, imageName);
            
            // Assert: Check if the response is BadRequest
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }

    }
}
