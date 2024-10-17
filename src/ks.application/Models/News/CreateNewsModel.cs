using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Models.News
{
    public class CreateNewsModel
    {
        public string Title { get; set; } 
        public string Content { get; set; } 
        public List<string> ImageUrl { get; set; } = [];
    }
}
