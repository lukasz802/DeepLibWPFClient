using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepLib.Models
{
    public class Ksiazka
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public int RokWydania { get; set; }
        //public int RodzajNosnika { get; set; }
        //public int Autor { get; set; }
        public string Okladka { get; set; }
        public bool Status { get; set; }
        public string KtoZmienilStatus { get; set; }
        public string Komentarz { get; set; }
        public DateTime KiedyPozyczyl { get; set; }
        
        public virtual int Autor { get; set; }
        public virtual int RodzajNosnika { get; set; }


    }
}
