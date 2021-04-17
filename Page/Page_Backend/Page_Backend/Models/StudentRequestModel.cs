using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Page_Backend.Models
{
    public class StudentRequestModel
    {
        [Key]
        public int StudentId { get; set; }
        public string BrojIndeksa { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Godina { get; set; }
        public int StatusStudenta { get; set; }
    }
}
