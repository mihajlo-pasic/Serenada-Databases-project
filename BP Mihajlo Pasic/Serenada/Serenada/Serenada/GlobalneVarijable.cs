using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serenada
{
    internal class GlobalneVarijable
    {
        public static Start start { get; set; }
        public static Korisnik korisnik = new Korisnik();
        public static Pjesma pjesma = new Pjesma();
        public static Plejlista plej = new Plejlista();
    }
}
