using Aplicatie_Service_Auto.models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Interaction logic for FormularMarca.xaml
    /// </summary>
    public partial class FormularMarca : Page
    {
        private Marca marcaSelectat;
        private AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public FormularMarca(Marca marcaSelectat = null)
        {
            InitializeComponent();

            if (marcaSelectat != null)
            {
                this.marcaSelectat = marcaSelectat;
                _afisareMarca(marcaSelectat);

                btnAdaugareMarca.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnModificareMarca.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdaugareMarca_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Marca marca = new Marca();
                marca = _construireMarca(marca);

                db.Marca.Add(marca);
                db.SaveChanges();

                this.NavigationService.Navigate(new ListaMarci());
            }
            catch
            {
                MessageBox.Show("Marcaul nu a putut fi adăugat în baza de date.");
            }
        }

        private void btnModificareMarca_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Marca marca = db.Marca.Where(x => x.id_marca == marcaSelectat.id_marca).FirstOrDefault();
                marca = _construireMarca(marca);

                db.Marca.AddOrUpdate(marca);
                db.SaveChanges();

                this.NavigationService.Navigate(new ListaMarci());
            }
            catch
            {
                MessageBox.Show("Marcaul nu a putut fi modificat din baza de date.");
            }
        }

        private Marca _construireMarca(Marca marca)
        {
            marca.denumire = tbDenumire.Text;

            return marca;
        }

        private void _afisareMarca(Marca marca)
        {
            tbDenumire.Text = marca.denumire.Trim();
        }
    }
}