using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serenada
{
    internal class Album
    {
        public int IdAlbuma { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public Izvodjac Izvodjac { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Album album &&
                   IdAlbuma == album.IdAlbuma;
        }

        public override int GetHashCode()
        {
            return -1155223109 + IdAlbuma.GetHashCode();
        }

        public override string ToString()
        {
            return Naziv;
        }
    }
}
