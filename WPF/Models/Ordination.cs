using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF.Models
{
    public class Ordination : INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }

        private string _lægemiddel = "";
        public string Lægemiddel
        {
            get => _lægemiddel;
            set
            {
                if (_lægemiddel != value)
                {
                    _lægemiddel = value;
                    OnPropertyChanged(nameof(Lægemiddel));
                }
            }
        }

        private string _dosis = "";
        public string Dosis
        {
            get => _dosis;
            set
            {
                if (_dosis != value)
                {
                    _dosis = value;
                    OnPropertyChanged(nameof(Dosis));
                }
            }
        }

        private int _udleveringer;
        public int Udleveringer
        {
            get => _udleveringer;
            set
            {
                if (_udleveringer != value)
                {
                    _udleveringer = value;
                    OnPropertyChanged(nameof(Udleveringer));
                }
            }
        }

        private int _fortagneUdleveringer;
        public int FortagneUdleveringer
        {
            get => _fortagneUdleveringer;
            set
            {
                if (_fortagneUdleveringer != value)
                {
                    _fortagneUdleveringer = value;
                    OnPropertyChanged(nameof(FortagneUdleveringer));

                    if (_fortagneUdleveringer >= Udleveringer)
                        Lukket = true;
                }
            }
        }

        private bool _lukket = false;
        public bool Lukket {
            get => _lukket;
            set
            {
                if (_lukket != value)
                {
                    _lukket = value;
                    OnPropertyChanged(nameof(Lukket));
                }
            }
        }
        public int ReceptId { get; set; }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                return columnName switch
                {
                    nameof(Lægemiddel) =>
                        string.IsNullOrWhiteSpace(Lægemiddel) ? "Lægemiddel må ikke være tomt." : null,

                    nameof(Dosis) =>
                        string.IsNullOrWhiteSpace(Dosis) ? "Dosis må ikke være tom." : null,

                    nameof(Udleveringer) =>
                        Udleveringer <= 0 ? "Udleveringer skal være større end 0." : null,

                    nameof(FortagneUdleveringer) =>
                        FortagneUdleveringer < 0 ? "Fortagne udleveringer kan ikke være negative."
                        : (FortagneUdleveringer > Udleveringer
                            ? "Fortagne udleveringer kan ikke være større end udleveringer."
                            : null),

                    _ => null
                };
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

