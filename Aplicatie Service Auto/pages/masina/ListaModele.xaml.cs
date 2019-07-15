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
    /// Interaction logic for ListaModele.xaml
    /// </summary>
    public partial class ListaModele : Page
    {
        private static Model marcaSelectat = null;

        private void btnAdaugareModel_Click(object sender, RoutedEventArgs e) => this.NavigationService.Navigate(new FormularModel());

        AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public ListaModele()
        {
            InitializeComponent();

            lvModele.ItemsSource = db.Model.ToList();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            marcaSelectat = lvModele.SelectedItem as Model;

            if (marcaSelectat != null)
            {
                this.NavigationService.Navigate(new FormularModel(marcaSelectat));
            }
        }
    }
}
