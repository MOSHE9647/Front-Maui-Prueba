using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppMovil.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty;
        public string Professor { get; set; } = string.Empty;
        public string ImageUrl { get; set; }

        // public string FullImageUrl => $"http://10.0.2.2:5275:5275{ImageUrl ?? ""}";
        public string FullImageUrl => $"http://localhost:5275{ImageUrl ?? ""}";


    }
}
