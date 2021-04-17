using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Page_Backend.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string BrojIndeksa { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Godina { get; set; }
        [Column("StatusStudenta")]
        public StatusStudenta StatusStudenta { get; set; }
        public List<KursStudent> KursStudenti { get; set; }

    }
}
