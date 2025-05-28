using MauiAppMovil.Services;
using System.Reflection;

namespace tests
{
    public static class TestHelpers
    {

        public readonly static string ImageName = "test_image.png";

        public static CourseService GetCourseService()
        {
            return new CourseService();
        }

        public static Stream GetLocalImageStream()
        {
            // Obtains the base directory of the application
            var basePath = AppContext.BaseDirectory;

            // Up one level to the project root
            var projectRoot = Path.Combine(basePath, "..", "..", "..", "..");

            // Navigate to the Resources/Embedded directory and combine with the image name
            var fullPath = Path.GetFullPath(Path.Combine(projectRoot, $"MauiAppMovil/Resources/Embedded/{ImageName}"));

            // Check if the file exists and return a stream to it
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"No se encontró la imagen en la ruta: {fullPath}");

            return File.OpenRead(fullPath);
        }
    }
}
