using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Exemple4
{
    [TestFixture]
    public class Tests_Exemple4
    {
        [Test]
        [Apartment(ApartmentState.STA)]
        public void VerifierContenubouton()
        {
            // Arrange

            MainWindow mw = new MainWindow();
            Button button = mw.MonBouton;

            // Act

            button.RaiseEvent(new System.Windows.RoutedEventArgs(Button.ClickEvent));
            // Assert
            Assert.That(button.Content, Is.EqualTo("Vous m'avez cliqué!"));
        }
    }
}
