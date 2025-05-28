using System.Net.Http.Headers;
using System.Net.Http.Json;
using MauiAppMovil.Models;

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
     public async Task<bool> CreateCourseAsync(Course course, Stream imageStream, string imageName)
     {
         var content = new MultipartFormDataContent();

         content.Add(new StringContent(course.Name), "Name");
         content.Add(new StringContent(course.Description), "Description");
         content.Add(new StringContent(course.Schedule), "Schedule");
         content.Add(new StringContent(course.Professor), "Professor");

         var fileContent = new StreamContent(imageStream);
         fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/png"); // Cambiar si usas .jpg u otros
         content.Add(fileContent, "File", imageName);

         var response = await _httpClient.PostAsync(baseUrl, content);
         return response.IsSuccessStatusCode;
     }

     // Actualizar curso (sin imagen)
     public async Task<bool> UpdateCourseAsync(Course course)
     {
         var response = await _httpClient.PutAsJsonAsync($"{baseUrl}/{course.Id}", course);
         return response.IsSuccessStatusCode;
     }

     // Eliminar curso
     public async Task<bool> DeleteCourseAsync(int id)
     {
         var response = await _httpClient.DeleteAsync($"{baseUrl}/{id}");
         return response.IsSuccessStatusCode;
     }
 }
}
