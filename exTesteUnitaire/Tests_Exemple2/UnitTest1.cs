using NUnit.Framework;
using Exemple2;
using System.Globalization;
using System.ComponentModel;
namespace Tests_Exemple2
{
    [TestFixture]
    public class Tests
    {
        List<Employe> employes;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VerifierDetails()
        {
           Program pobj = new Program();
           
           employes = pobj.LesEmployes();
           foreach (var emp in employes)
            {
               Assert.Greater(emp.Id, 0);
               Assert.IsNotNull(emp.Nom);
               Assert.Greater(emp.Salaire, 0);
               Assert.IsNotNull(emp.Genre);
                    
           }

        }

        [Test]
        public void TesterLogin()
        {
            Program pobj = new Program();

            string x = pobj.Login("Ajit","1234");
            string y = pobj.Login("","");
            string z = pobj.Login("Admin","Admin");

            Assert.That(x, Is.EqualTo("Identifiant ou mot de passe incorrecte."));
            Assert.That(y, Is.EqualTo("L'identifiant ou le mot de passe ne doit pas être vide."));
            Assert.That(z, Is.EqualTo("Bienvenue Admin."));
            

        }


        [Test]
        public void VerifierDetailEmploye()
        {
            Program pobj = new Program();

            var p= pobj.GetDetails(100);
            foreach (var x in p)
            {
               Assert.That(x.Id, Is.EqualTo(100));
                Assert.That(x.Nom, Is.EqualTo("Jacques"));
                Assert.That(x.Salaire, Is.EqualTo(40000));
                Assert.That(x.Genre, Is.EqualTo("male"));
            }


        }
    }
}