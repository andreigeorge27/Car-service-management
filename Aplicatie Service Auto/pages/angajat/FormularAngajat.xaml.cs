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
    /// Interaction logic for FormularAngajat.xaml
    /// </summary>
    public partial class FormularAngajat: Page
    {
        private Angajat angajatSelectat;
        private AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public FormularAngajat(Angajat angajatSelectat = null)
        {
            InitializeComponent();

            if (angajatSelectat != null)
            {
                this.angajatSelectat = angajatSelectat;
                _afisareAngajat(angajatSelectat);

                btnAdaugareAngajat.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnModificareAngajat.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdaugareAngajat_Click(object sender, RoutedEventArgs e)
        {
            try
            {                                
                Angajat angajat = new Angajat();
                angajat = _construireAngajat(angajat);
                if (angajat.CNP.Length == 13)
                {
                    db.Angajat.Add(angajat);
                    db.SaveChanges();

                    this.NavigationService.Navigate(new ListaAngajati());
                }
                else
                {
                    MessageBox.Show("Var rog introduceti date valide.");
                }
                
            }
            catch
            {
                MessageBox.Show("Angajatul nu a putut fi adăugat în baza de date.");
            }
        }

        private void btnModificareAngajat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Angajat angajat = db.Angajat.Where(x => x.id_angajat == angajatSelectat.id_angajat).FirstOrDefault();
                angajat = _construireAngajat(angajat);
                if (angajat.CNP.Length == 13)
                {
                    db.Angajat.AddOrUpdate(angajat);
                    db.SaveChanges();

                    this.NavigationService.Navigate(new ListaAngajati());
                }
                else
                {
                    MessageBox.Show("Var rog introduceti date valide.");
                }
               
            }
            catch
            {
                MessageBox.Show("Angajatul nu a putut fi modificat din baza de date.");
            }
        }

        private Angajat _construireAngajat(Angajat angajat)
        {
            angajat.nume = tbNume.Text;
            angajat.CNP = tbCNP.Text;
            angajat.serieCI = tbSerieCI.Text;
            angajat.numarCI = int.Parse(tbNumarCI.Text.ToString());
            angajat.data_nasterii = dpDataNasterii.SelectedDate;
            angajat.data_angajare = dpDataAngajarii.SelectedDate;
            angajat.adresa = tbAdresa.Text;
            angajat.telefon = tbTelefon.Text;
            angajat.email = tbEmail.Text;
            angajat.salariu = int.Parse(tbSalariu.Text.ToString());

            return angajat;
        }

        private void _afisareAngajat(Angajat angajat)
        {
            tbNume.Text = angajat.nume.Trim();
            tbCNP.Text = angajat.CNP.Trim();
            tbSerieCI.Text = angajat.serieCI.Trim();
            tbNumarCI.Text = angajat.numarCI.ToString().Trim();
            dpDataNasterii.SelectedDate = angajat.data_nasterii;
            dpDataAngajarii.SelectedDate = angajat.data_angajare;
            tbAdresa.Text = angajat.adresa.Trim();
            tbTelefon.Text = angajat.telefon.Trim();
            tbEmail.Text = angajat.email.Trim();
            tbSalariu.Text = angajat.salariu.ToString().Trim();
        }
    }
}