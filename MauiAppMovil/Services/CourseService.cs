using MauiAppMovil.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MauiAppMovil.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        private readonly string baseUrl = $"{AppConstants.ApiBaseUrl}/course";

        public CourseService()
        {
            _httpClient = new HttpClient();
        }

        // Obtener todos los cursos
        public async Task<List<Course>> GetCoursesAsync()
        {
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Course>>() ?? new();
        }

        // Crear curso con imagen (multipart/form-data)
        public async Task<HttpResponseMessage> CreateCourseWithResponseAsync(Course course, Stream imageStream, string imageName)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(course.Name), "Name");
            content.Add(new StringContent(course.Description), "Description");
            content.Add(new StringContent(course.Schedule), "Schedule");
            content.Add(new StringContent(course.Professor), "Professor");

            if (imageStream == null || string.IsNullOrEmpty(imageName))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Image stream or image name cannot be null or empty.")
                };
            }

            var fileContent = new StreamContent(imageStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            content.Add(fileContent, "File", imageName);

            return await _httpClient.PostAsync(baseUrl, content);
        }

        // Actualizar curso con imagen (multipart/form-data)
        public async Task<HttpResponseMessage> UpdateCourseWithImageAsync(Course course, Stream? imageStream = null, string? imageName = null)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(course.Name), "Name");
            content.Add(new StringContent(course.Description), "Description");
            content.Add(new StringContent(course.Schedule), "Schedule");
            content.Add(new StringContent(course.Professor), "Professor");

            if (imageStream != null && imageName != null)
            {
                var fileContent = new StreamContent(imageStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                content.Add(fileContent, "File", imageName);
            }

            return await _httpClient.PutAsync($"{baseUrl}/{course.Id}", content);
        }


        // Eliminar curso
        public async Task<HttpResponseMessage> DeleteCourseAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{baseUrl}/{id}");
        }
    }
}
