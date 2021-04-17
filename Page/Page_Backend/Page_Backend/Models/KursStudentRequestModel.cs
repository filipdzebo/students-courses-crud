using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Page_Backend.Models
{
    public class KursStudentRequestModel
    {
        public int KursId { get; set; }
        public string Naziv { get; set; }
        public List<int> OdabraniStudenti { get; set; }
    }
}
