using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabRefacto
{
    public enum Session
    {
        principale,
        rattrapage
    }
    public class Etudiant
    {
        double noteInfo {  get; set; }
        double noteMaths {  get; set; }
        string? maticule {  get; set; }
        string? nom {  get; set; }
        Session session { get; set; }
        public Etudiant(double noteI, double noteM, string mat, string nm , Session ses) 
        {
            noteInfo = noteI;
            noteMaths = noteM;
            maticule = mat;
            nom = nm;
            session = ses;
        }
        private double MoyennePrincipale()
        {
            return ((noteMaths + noteInfo) + 1) / 2;
        }
        private double MoyenneRattrapage()
        {
            return (noteMaths + noteInfo) / 2;

        }
        public double CalculerMoyenne()
        {
            if(session == Session.principale) 
                return MoyennePrincipale();
            else return MoyenneRattrapage();
        }
    }
}
