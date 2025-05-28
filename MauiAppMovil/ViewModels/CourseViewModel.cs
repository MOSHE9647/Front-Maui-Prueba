using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MauiAppMovil.Models;
using MauiAppMovil.Services;

namespace MauiAppMovil.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        private readonly CourseService _service = new();

        public ObservableCollection<Course> Courses { get; set; } = new();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task LoadCoursesAsync()
        {
            var list = await _service.GetCoursesAsync();
            Courses.Clear();
            foreach (var course in list)
            {
                Courses.Add(course);
            }
        }
    }
}
