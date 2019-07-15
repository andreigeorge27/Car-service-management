using Aplicatie_Service_Auto.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aplicatie_Service_Auto
{
    /// <summary>
    /// Interaction logic for ListaAngajati.xaml
    /// </summary>
    public partial class ListaAngajati : Page
    {
        private static Angajat angajatSelectat = null;

        private void btnAdaugareAngajat_Click(object sender, RoutedEventArgs e) => this.NavigationService.Navigate(new FormularAngajat());

        AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public ListaAngajati()
        {
            InitializeComponent();

            lvAngajati.ItemsSource = db.Angajat.ToList();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            angajatSelectat = lvAngajati.SelectedItem as Angajat;

            if (angajatSelectat != null)
            {
                this.NavigationService.Navigate(new FormularAngajat(angajatSelectat));
            }
        }
    }
}
