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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void btnAcasa_Click(object sender, RoutedEventArgs e)       => frame.NavigationService.Navigate(new Statistici());
        private void btnAngajati_Click(object sender, RoutedEventArgs e)    => frame.NavigationService.Navigate(new ListaAngajati());
        private void btnClienti_Click(object sender, RoutedEventArgs e)     => frame.NavigationService.Navigate(new ListaClienti());
        private void btnMarci_Click(object sender, RoutedEventArgs e)       => frame.NavigationService.Navigate(new ListaMarci());
        private void btnModele_Click(object sender, RoutedEventArgs e)      => frame.NavigationService.Navigate(new ListaModele());
        private void btnServicii_Click(object sender, RoutedEventArgs e)    => frame.NavigationService.Navigate(new ListaServicii());
        private void btnInterventii_Click(object sender, RoutedEventArgs e) => frame.NavigationService.Navigate(new ListaInterventii());
        private void btnMasini_Click(object sender, RoutedEventArgs e)      => frame.NavigationService.Navigate(new ListaMasini());

        private void btnListe_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        public MainWindow()
        {
            InitializeComponent();

            frame.NavigationService.Navigate(new Statistici());
        }

        private void Frame_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
