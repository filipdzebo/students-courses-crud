using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Page_Backend.Models
{
    public class Kurs
    {
        [Key]
        public int KursId { get; set; }
        public string Naziv { get; set; }
        public List<KursStudent> KursStudenti { get; set; }
    }
}
