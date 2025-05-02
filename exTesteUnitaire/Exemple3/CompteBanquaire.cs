using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple3
{
    public class CompteBancaire
    {
        private readonly string m_NomClient;
        private double m_solde;

        private CompteBancaire() { }

        public CompteBancaire(string nomClient, double solde)
        {
            m_NomClient = nomClient;
            m_solde = solde;
        }

        public string NomClient
        {
            get { return m_NomClient; }
        }

        public double Solde
        {
            get { return m_solde; }
        }

        public void Debit(double montant)
        {
            if (montant > m_solde)
            {
                throw new ArgumentOutOfRangeException("montant");
            }

            if (montant < 0)
            {
                throw new ArgumentOutOfRangeException("montant");
            }

            m_solde -= montant;
        }

        public void Credit(double montant)
        {
            if (montant < 0)
            {
                throw new ArgumentOutOfRangeException("montant");
            }

            m_solde += montant;
        }

        public static void Main()
        {
            CompteBancaire ba = new CompteBancaire("Mr. Bryan Walton", 11.99);

            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine("Le solde actuelle du compte est " + ba.Solde);

            Console.ReadKey();
        }

    }
}