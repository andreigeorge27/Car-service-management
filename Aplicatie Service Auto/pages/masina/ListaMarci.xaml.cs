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
    /// Interaction logic for ListaMarci.xaml
    /// </summary>
    public partial class ListaMarci : Page
    {
        private static Marca marcaSelectat = null;

        private void btnAdaugareMarca_Click(object sender, RoutedEventArgs e) => this.NavigationService.Navigate(new FormularMarca());

        AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public ListaMarci()
        {
            InitializeComponent();

            lvMarci.ItemsSource = db.Marca.ToList();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            marcaSelectat = lvMarci.SelectedItem as Marca;

            if (marcaSelectat != null)
            {
                this.NavigationService.Navigate(new FormularMarca(marcaSelectat));
            }
        }
    }
}
