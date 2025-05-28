using MauiAppMovil.Models;
using MauiAppMovil.Services;

namespace tests
{
    public class DeleteCourseTest
    {
        private readonly CourseService _courseService;
        private readonly Stream _testImageStream;

        public DeleteCourseTest()
        {
            _courseService = TestHelpers.GetCourseService();
            _testImageStream = TestHelpers.GetLocalImageStream();
        }

        [Fact]
        public async Task DeleteCourse_HappyPath_ReturnsNoContent()
        {
            // Arrange: Get the list of courses
            var courses = await _courseService.GetCoursesAsync();

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

                using var imageStream = _testImageStream; // Use the test image stream
                var imageName = TestHelpers.ImageName; // Name for the test image
                var createResult = await _courseService.CreateCourseWithResponseAsync(newCourse, imageStream, imageName);

                // Verify that the course was created successfully
                Assert.True(createResult.IsSuccessStatusCode);

                // Get the updated list of courses
                courses = await _courseService.GetCoursesAsync();
            }

            Assert.NotEmpty(courses); // Now we should have at least one course
            var courseToDelete = courses.First();

            // Act: Call the method to delete the course
            var result = await _courseService.DeleteCourseAsync(courseToDelete.Id);

            // Assert: Verify that the response is NoContent
            Assert.Equal(System.Net.HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task DeleteCourse_NonExistentId_ReturnsNotFound()
        {
            // Arrange: Use a non-existent course ID
            var nonExistentId = int.MaxValue; // Assuming this ID does not exist in the database

            // Act: Call the method to delete the course
            var result = await _courseService.DeleteCourseAsync(nonExistentId);
            
            // Assert: Verify that the response is NotFound
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
