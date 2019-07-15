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
using System.Collections.ObjectModel;
using Aplicatie_Service_Auto.models;

namespace Aplicatie_Service_Auto
{
    /// <summary>
    /// Interaction logic for FormularInterventie.xaml
    /// </summary>
    public partial class FormularInterventie : Page
    {
        private Interventie interventieSelectat;
        private AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();
        
        public FormularInterventie(Interventie interventieSelectat = null)
        {
            InitializeComponent();

            cbClient.ItemsSource = db.Client.ToList();

            cbServiciu.ItemsSource = db.Serviciu.ToList();
            cbAngajat.ItemsSource = db.Angajat.ToList();

            if (interventieSelectat != null)
            {
                this.interventieSelectat = interventieSelectat;
                _afisareInterventie(interventieSelectat);

                btnAdaugareInterventie.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnModificareInterventie.Visibility = Visibility.Collapsed;
            }
        }

        private void cbClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idClientSelectat = int.Parse(cbClient.SelectedValue.ToString());
            cbMasina.ItemsSource = db.Masina.Where(x => x.id_client == idClientSelectat).ToList();
        }

        private void btnAdaugareInterventie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Interventie interventie = new Interventie();
                interventie = _construireInterventie(interventie);
                if(interventie.data_primire > interventie.data_finalizare)
                {
                    throw new System.ArgumentException("Data de finalizare trebuie sa fie mai mare decat data de primire", "original");
                }
                db.Interventie.Add(interventie);
                db.SaveChanges();

                var servicii = db.Interventie_Serviciu.Where(x => x.id_interventie == null).ToList();

                foreach(var serviciu in servicii)
                {
                    Interventie_Serviciu serviciudb = db.Interventie_Serviciu.Where(x => x.id_int_serv == serviciu.id_int_serv).FirstOrDefault();
                    serviciudb.id_interventie = interventie.id_interventie;

                    db.Interventie_Serviciu.AddOrUpdate();
                    db.SaveChanges();
                }

                this.NavigationService.Navigate(new ListaInterventii());
            }
            catch
            {
                MessageBox.Show("Intervenția nu a putut fi adăugată în baza de date.");
            }
        }

        private void btnModificareInterventie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                Interventie interventie = db.Interventie.Where(x => x.id_interventie == interventieSelectat.id_interventie).FirstOrDefault();
                interventie = _construireInterventie(interventie);
                if (interventie.data_primire > interventie.data_finalizare)
                {
                    throw new System.ArgumentException("Data de finalizare trebuie sa fie mai mare decat data de primire", "original");
                }
                db.Interventie.AddOrUpdate(interventie);
                db.SaveChanges();

                this.NavigationService.Navigate(new ListaInterventii());
            }
            catch
            {
                MessageBox.Show("Intervenția nu a putut fi modificată din baza de date.");
            }
        }

        private void btnAdaugareServiciu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Interventie_Serviciu interventie_serviciu = new Interventie_Serviciu();

                if (interventieSelectat == null)
                {
                    interventie_serviciu.id_interventie = null;
                }
                else
                {
                    interventie_serviciu.id_interventie = interventieSelectat.id_interventie;
                }
                
                interventie_serviciu.id_serviciu = int.Parse(cbServiciu.SelectedValue.ToString());
                interventie_serviciu.id_angajat = int.Parse(cbAngajat.SelectedValue.ToString());
                interventie_serviciu.pret = int.Parse(tbPret.Text.ToString());

                db.Interventie_Serviciu.Add(interventie_serviciu);
                db.SaveChanges();

                lvServicii.ClearValue(ItemsControl.ItemsSourceProperty);
                lvServicii.ItemsSource = db.Interventie_Serviciu.Where(x => x.id_interventie == interventie_serviciu.id_interventie).ToList();
            }
            catch
            {
               MessageBox.Show("Serviciul nu a putut fi adăugat în această intervenție.");
            }
        }

        private void btnModificareServiciu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStergereServiciu_Click(object sender, RoutedEventArgs e)
        {

        }

        private Interventie _construireInterventie(Interventie interventie)
        {
            interventie.id_client = int.Parse(cbClient.SelectedValue.ToString());
            interventie.id_masina = int.Parse(cbMasina.SelectedValue.ToString());
            interventie.data_primire = dpDataPrimire.SelectedDate;
            interventie.data_finalizare = dpDataFinalizare.SelectedDate;
            interventie.status = int.Parse(cbStatus.SelectedValue.ToString());

            return interventie;
        }

        private void _afisareInterventie(Interventie interventie)
        {
            cbClient.SelectedValue = interventie.id_client;
            cbMasina.SelectedValue = interventie.id_masina;
            dpDataPrimire.SelectedDate = interventie.data_primire;
            dpDataFinalizare.SelectedDate = interventie.data_finalizare;
            cbStatus.SelectedValue = interventie.status;

            lvServicii.ItemsSource = db.Interventie_Serviciu.Where(x => x.id_interventie == interventie.id_interventie).ToList();
        }
    }
}