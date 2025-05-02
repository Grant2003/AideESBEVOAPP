using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;

namespace BLL
{
    public class Film
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int Annee { get; set; }
        public string Pays { get; set; }
    }
}
