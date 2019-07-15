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
    /// Interaction logic for FormularServiciu.xaml
    /// </summary>
    public partial class FormularServiciu : Page
    {
        private Serviciu serviciuSelectat;
        private AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public FormularServiciu(Serviciu serviciuSelectat = null)
        {
            InitializeComponent();

            if (serviciuSelectat != null)
            {
                this.serviciuSelectat = serviciuSelectat;
                _afisareServiciu(serviciuSelectat);

                btnAdaugareServiciu.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnModificareServiciu.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdaugareServiciu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Serviciu serviciu = new Serviciu();
                serviciu = _construireServiciu(serviciu);

                db.Serviciu.Add(serviciu);
                db.SaveChanges();

                this.NavigationService.Navigate(new ListaServicii());
            }
            catch
            {
                MessageBox.Show("Serviciul nu a putut fi adăugat în baza de date.");
            }
        }

        private void btnModificareServiciu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Serviciu serviciu = db.Serviciu.Where(x => x.id_serviciu == serviciuSelectat.id_serviciu).FirstOrDefault();
                serviciu = _construireServiciu(serviciu);

                db.Serviciu.AddOrUpdate(serviciu);
                db.SaveChanges();

                this.NavigationService.Navigate(new ListaServicii());
            }
            catch
            {
                MessageBox.Show("Serviciul nu a putut fi modificat din baza de date.");
            }
        }

        private Serviciu _construireServiciu(Serviciu serviciu)
        {
            serviciu.denumire = tbDenumire.Text;
            serviciu.descriere = tbDescriere.Text;
            serviciu.durata_medie = int.Parse(tbDurataMedie.Text.ToString());

            return serviciu;
        }

        private void _afisareServiciu(Serviciu serviciu)
        {
            tbDenumire.Text = serviciu.denumire.Trim();
            tbDescriere.Text = serviciu.descriere.Trim();
            tbDurataMedie.Text = serviciu.durata_medie.ToString().Trim();
        }
    }
}