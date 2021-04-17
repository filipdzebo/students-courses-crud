using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Page_Backend.Models
{
    public class KursStudent
    {
        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
