using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Aplicatie_Service_Auto.models;

namespace Aplicatie_Service_Auto
{
    /// <summary>
    /// Interaction logic for FormularMasina.xaml
    /// </summary>
    public partial class FormularMasina : Page
    {
        private Masina masinaSelectat;
        private AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public FormularMasina(Masina masinaSelectat = null)
        {
            InitializeComponent();

            cbClient.ItemsSource = db.Client.ToList();
            cbMarca.ItemsSource = db.Marca.ToList();

            if (masinaSelectat != null)
            {
                this.masinaSelectat = masinaSelectat;
                _afisareMasina(masinaSelectat);

                btnAdaugareMasina.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnModificareMasina.Visibility = Visibility.Collapsed;
            }
        }

        private void cbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idMarcaSelectat = int.Parse(cbMarca.SelectedValue.ToString());
            cbModel.ItemsSource = db.Model.Where(x => x.id_marca == idMarcaSelectat).ToList();
        }

        private void btnAdaugareMasina_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Masina masina = new Masina();
                masina = _construireMasina(masina);

                db.Masina.Add(masina);
                db.SaveChanges();

                this.NavigationService.Navigate(new ListaMasini());
            }
            catch
            {
                MessageBox.Show("Masina nu a putut fi adăugată în baza de date.");
            }
        }

        private void btnModificareMasina_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Masina masina = db.Masina.Where(x => x.id_masina == masinaSelectat.id_masina).FirstOrDefault();
                masina = _construireMasina(masina);

                db.Masina.AddOrUpdate(masina);
                db.SaveChanges();

                this.NavigationService.Navigate(new ListaMasini());
            }
            catch
            {
                MessageBox.Show("Masina nu a putut fi modificată din baza de date.");
            }
        }

        private Masina _construireMasina(Masina masina)
        {
            masina.id_client = int.Parse(cbClient.SelectedValue.ToString());
            masina.serie_sasiu = tbSerieSasiu.Text;
            masina.nr_inmatriculare = tbNrInmatriculare.Text;
            masina.id_marca = int.Parse(cbMarca.SelectedValue.ToString());
            masina.id_model = int.Parse(cbModel.SelectedValue.ToString());
            masina.rulaj = int.Parse(tbRulaj.Text);
            masina.an_fabricatie = int.Parse(tbAnFabricatie.Text);
            masina.motorizare = tbMotorizare.Text;

            return masina;
        }

        private void _afisareMasina(Masina masina)
        {
            cbClient.SelectedValue = masina.id_client;
            tbSerieSasiu.Text = masina.serie_sasiu;
            tbNrInmatriculare.Text = masina.nr_inmatriculare;
            cbMarca.SelectedValue = masina.id_marca;
            cbModel.SelectedValue = masina.id_model;
            tbRulaj.Text = masina.rulaj.ToString();
            tbAnFabricatie.Text = masina.an_fabricatie.ToString();
            tbMotorizare.Text = masina.motorizare;
        }
    }
}
