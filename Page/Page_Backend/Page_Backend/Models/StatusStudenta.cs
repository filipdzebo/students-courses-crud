using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Page_Backend.Models
{
    public class StatusStudenta
    {
        [Key]
        public int StatusStudentaId { get; set; }
        public string NazivStatusa { get; set; }
        public List<Student> Studenti { get; set; }
    }
}
