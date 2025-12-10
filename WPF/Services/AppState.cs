using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Models;

namespace WPF.Services
{
    public class AppState
    {
        private static AppState _instance;
        public static AppState Instance => _instance ??= new AppState();

        // Den lægehus du er logget ind som
        public Lægehus? CurrentLægehus { get; private set; }

        private AppState() { }

        public void SetLægehus(Lægehus lægehus)
        {
            CurrentLægehus = lægehus;
        }
    }

}
