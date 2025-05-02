using System.Diagnostics;

namespace Tests_Exemple1
{
    // Attribut pour pr�ciser la classe de tests unitaires � faire
    [TestFixture]
    public class Tests
    {
        private const string ChaineAttendue = "Exemple de l'utilisation de NUnit";
        
        // Une m�thode qui utilise l'attribut SetUp, c'est la premi�re m�thode qui est ex�cut�e, avant les tests
        [SetUp]
        public void Setup()
        {
            Debug.WriteLine("Setup\n-----");
        }

        [Test]
        public void Test1()
        {
            var sw = new StringWriter();

            // Rediriger la sortie standard de la console vers le StringWriter (sw)
            Console.SetOut(sw);
            // Appeler la m�thode Main de la classe Program
            Exemple1.Program.Main();
            // R�cup�rer le contenu de la sortie standard
            var result = sw.ToString().Trim();
            Debug.WriteLine("r�sultat : "+result);
            Debug.WriteLine("r�sultat attendue : "+ChaineAttendue);

            // Comparer le contenu de la sortie standard avec la chaine attendue
            Assert.That(result, Is.EqualTo(ChaineAttendue));

        }
    }
}