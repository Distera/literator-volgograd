using System.Collections.Generic;

namespace LiteratorVolgograd.Models
{
    public class ViewMain
    {
        public List<Project> Project { get; set; }
        public List<News> News { get; set; }
        public List<Author> Author { get; set; }
        public List<Publication> Publication { get; set; }
    }
}
