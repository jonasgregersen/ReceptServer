using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOLægehus = DataTransferLayer.Models.Lægehus;

namespace WPF.Services
{
    public class AppState
    {
        private static AppState _instance;
        public static AppState Instance => _instance ??= new AppState();

        // Den lægehus du er logget ind som
        public DTOLægehus? CurrentLægehus { get; private set; }

        private AppState() { }

        public void SetLægehus(DTOLægehus lægehus)
        {
            CurrentLægehus = lægehus;
        }
    }

}
