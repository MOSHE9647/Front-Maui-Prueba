using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using MauiAppMovil.Models;
using MauiAppMovil.Services;
using System.Net.Http.Headers;
using System.Net.Http;

namespace MauiAppMovil.Views
{
    public partial class CourseFormPage : ContentPage
    {
        private FileResult _selectedImage;
        private readonly Course _editingCourse;
        private readonly CourseService _courseService = new();

        public CourseFormPage(Course course = null)
        {
            InitializeComponent();

            _editingCourse = course ?? new Course();

            if (_editingCourse.Id != 0)
            {
                Title = "Editar Curso";
                nameEntry.Text = _editingCourse.Name;
                descEntry.Text = _editingCourse.Description;
                schedEntry.Text = _editingCourse.Schedule;
                profEntry.Text = _editingCourse.Professor;
                previewImage.Source = _editingCourse.FullImageUrl;
            }
            else
            {
                Title = "Nuevo Curso";
            }
        }

        private async void OnPickImage(object sender, EventArgs e)
        {
#pragma warning disable CS8601 // Posible asignaci n de referencia nula
            _selectedImage = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Selecciona una imagen"
            });
#pragma warning restore CS8601 // Posible asignaci n de referencia nula

            if (_selectedImage != null)
            {
                previewImage.Source = ImageSource.FromFile(_selectedImage.FullPath);
            }
        }

        private async void OnSaveCourse(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(nameEntry.Text) ||
                string.IsNullOrWhiteSpace(descEntry.Text) ||
                string.IsNullOrWhiteSpace(schedEntry.Text) ||
                string.IsNullOrWhiteSpace(profEntry.Text))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            // Asignar valores al objeto
            _editingCourse.Name = nameEntry.Text;
            _editingCourse.Description = descEntry.Text;
            _editingCourse.Schedule = schedEntry.Text;
            _editingCourse.Professor = profEntry.Text;

            HttpResponseMessage response;

            if (_editingCourse.Id == 0)
            {
                using var stream = await _selectedImage.OpenReadAsync();
                response = await _courseService.CreateCourseWithResponseAsync(_editingCourse, stream, _selectedImage.FileName);
            }
            else
            {
                Stream? stream = null;
                string? imageName = null;

                if (_selectedImage != null)
                {
                    stream = await _selectedImage.OpenReadAsync();
                    imageName = _selectedImage.FileName;
                }

                response = await _courseService.UpdateCourseWithImageAsync(_editingCourse, stream, imageName);
            }


            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert(" xito", _editingCourse.Id == 0 ? "Curso creado." : "Curso actualizado.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"No se pudo guardar el curso:\n{error}", "OK");
            }
        }
    }
}