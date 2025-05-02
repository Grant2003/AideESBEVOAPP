using System.Diagnostics;

namespace Tests_Exemple1
{
    // Attribut pour préciser la classe de tests unitaires à faire
    [TestFixture]
    public class Tests
    {
        private const string ChaineAttendue = "Exemple de l'utilisation de NUnit";
        
        // Une méthode qui utilise l'attribut SetUp, c'est la première méthode qui est exécutée, avant les tests
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
            // Appeler la méthode Main de la classe Program
            Exemple1.Program.Main();
            // Récupérer le contenu de la sortie standard
            var result = sw.ToString().Trim();
            Debug.WriteLine("résultat : "+result);
            Debug.WriteLine("résultat attendue : "+ChaineAttendue);

            // Comparer le contenu de la sortie standard avec la chaine attendue
            Assert.That(result, Is.EqualTo(ChaineAttendue));

        }
    }
}