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
    /// Interaction logic for ListaServicii.xaml
    /// </summary>
    public partial class ListaServicii : Page
    {
        private static Serviciu serviciuSelectat = null;

        private void btnAdaugareServiciu_Click(object sender, RoutedEventArgs e) => this.NavigationService.Navigate(new FormularServiciu());

        AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public ListaServicii()
        {
            InitializeComponent();

            lvServicii.ItemsSource = db.Serviciu.ToList();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            serviciuSelectat = lvServicii.SelectedItem as Serviciu;

            if (serviciuSelectat != null)
            {
                this.NavigationService.Navigate(new FormularServiciu(serviciuSelectat));
            }
        }
    }
}