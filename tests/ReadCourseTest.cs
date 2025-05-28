using MauiAppMovil.Services;

namespace tests
{
    public class ReadCourseTest
    {

        private readonly CourseService _courseService;

        public ReadCourseTest()
        {
            _courseService = TestHelpers.GetCourseService();
        }

        [Fact]
        public async Task GetAllCourses_ReturnsAllCourses()
        {
            // Act: Call the method to get all courses
            var courses = await _courseService.GetCoursesAsync();

            // Assert: Check that the result is not null and contains courses
            Assert.NotNull(courses);
        }
    }
}