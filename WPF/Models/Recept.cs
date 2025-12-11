using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class Recept
    {
        public int Id { get; set; }
        public string YderNummer { get; set; }
        public string CprNummer { get; set; }
        public DateTime OprettelsesDato { get; set; }
        public int LægehusId { get; set; }
        public bool Lukket { get; set; }
        public ObservableCollection<Ordination> Ordinationer { get; set; }
    }
}
