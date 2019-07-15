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
    /// Interaction logic for FormularClient.xaml
    /// </summary>
    public partial class FormularClient : Page
    {
        private Client clientSelectat;
        private AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public FormularClient(Client clientSelectat = null)
        {
            InitializeComponent();

            if (clientSelectat != null)
            {
                this.clientSelectat = clientSelectat;
                _afisareClient(clientSelectat);

                btnAdaugareClient.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnModificareClient.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdaugareClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client client = new Client();
                client = _construireClient(client);
                if (client.CNP.Length == 13)
                {
                    db.Client.Add(client);
                    db.SaveChanges();

                    this.NavigationService.Navigate(new ListaClienti());
                }
                else
                {
                    MessageBox.Show("Var rog introduceti date valide.");
                }
            }
            catch
            {
                MessageBox.Show("Clientul nu a putut fi adăugat în baza de date.");
            }
        }

        private void btnModificareClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client client = db.Client.Where(x => x.id_client == clientSelectat.id_client).FirstOrDefault();
                client = _construireClient(client);
                if (client.CNP.Length == 13)
                {
                    db.Client.AddOrUpdate(client);
                    db.SaveChanges();

                    this.NavigationService.Navigate(new ListaClienti());
                }
                else
                {
                    MessageBox.Show("Var rog introduceti date valide.");
                }            
            }
            catch
            {
                MessageBox.Show("Clientul nu a putut fi modificat din baza de date.");
            }
        }

        private Client _construireClient(Client client)
        {
            client.nume = tbNume.Text;
            client.CNP = tbCNP.Text;
            client.serieCI = tbSerieCI.Text;
            client.numarCI = int.Parse(tbNumarCI.Text.ToString());
            client.data_nasterii = dpDataNasterii.SelectedDate;
            client.adresa = tbAdresa.Text;
            client.telefon = tbTelefon.Text;
            client.email = tbEmail.Text;

            return client;
        }

        private void _afisareClient(Client client)
        {
            tbNume.Text = client.nume.Trim();
            tbCNP.Text = client.CNP.Trim();
            tbSerieCI.Text = client.serieCI.Trim();
            tbNumarCI.Text = client.numarCI.ToString().Trim();
            dpDataNasterii.SelectedDate = client.data_nasterii;
            if (client.adresa != null)
            {
                tbAdresa.Text = client.adresa.Trim();
            }
            if (client.telefon != null)
            {
                tbTelefon.Text = client.telefon.Trim();
            }
            if(client.email != null)
            {
                tbEmail.Text = client.email.Trim();
            }
        }
    }
}