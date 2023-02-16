using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoLinqMultiTables.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string NameComment { get; set; }
        public int MarksId { get; set; }
    }
}
