using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class Ordination
    {
        public int Id { get; set; }
        public String Lægemiddel { get; set; }
        public String Dosis { get; set; }
        public int Udleveringer { get; set; }
        public int FortagneUdleveringer { get; set; }
        public bool Lukket { get; set; }
        public int ReceptId { get; set; }
    }
}
