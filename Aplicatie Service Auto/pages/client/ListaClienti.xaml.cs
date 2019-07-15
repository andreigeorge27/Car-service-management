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
    /// Interaction logic for ListaClienti.xaml
    /// </summary>
    public partial class ListaClienti : Page
    {
        private static Client clientSelectat = null;

        private void btnAdaugareClient_Click(object sender, RoutedEventArgs e) => this.NavigationService.Navigate(new FormularClient());

        AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public ListaClienti()
        {
            InitializeComponent();

            lvClienti.ItemsSource = db.Client.ToList();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            clientSelectat = lvClienti.SelectedItem as Client;

            if (clientSelectat != null)
            {
                this.NavigationService.Navigate(new FormularClient(clientSelectat));
            }
        }
    }
}
