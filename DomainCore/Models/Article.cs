using System;
using System.Collections.Generic;
using System.Text;

namespace DomainCore.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
    }
}
