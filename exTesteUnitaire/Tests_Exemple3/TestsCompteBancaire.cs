using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Exemple3;

namespace Tests_Exemple3
{
    [TestFixture]
    public class TestsCompteBancaire
    {
        [Test]
        public void Debit_MontantValide_MAJSolde()
        {


            // Arrange --> Préparer les données
            CompteBancaire ba = new CompteBancaire("Mr. Bryan Walton", 11.99);
           double soldeDebut = 11.99;
           double montantDebit = 4.55;
           double soldeAttendu = 7.44;


            // Act --> Exécuter la méthode à tester
            ba.Debit(montantDebit);
            double actual = ba.Solde;

            // Assert --> Vérifier le résultat
            Assert.That(actual, Is.EqualTo(soldeAttendu), "Compte non débité correctement");
        }

        [Test]
       public void Debit_MontantNegatif_LeverException()
        {
            // Arrange --> Préparer les données

            double soldeDebut = 11.99;
            double montantDebit = -100;

            CompteBancaire ba = new CompteBancaire("Mr. Bryan Walton", soldeDebut);
            // Act --> Exécuter la méthode à tester
            Assert.Throws<ArgumentOutOfRangeException>(() => ba.Debit(montantDebit));
        }
    }
}
