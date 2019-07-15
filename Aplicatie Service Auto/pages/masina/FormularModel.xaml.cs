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
    /// Interaction logic for FormularModel.xaml
    /// </summary>
    public partial class FormularModel : Page
    {
        private Model modelSelectat;
        private AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public FormularModel(Model modelSelectat = null)
        {
            InitializeComponent();

            cbMarca.ItemsSource = db.Marca.ToList();

            if (modelSelectat != null)
            {
                this.modelSelectat = modelSelectat;
                _afisareModel(modelSelectat);

                btnAdaugareModel.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnModificareModel.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdaugareModel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model model = new Model();
                model = _construireModel(model);

                db.Model.Add(model);
                db.SaveChanges();

                this.NavigationService.Navigate(new ListaModele());
            }
            catch
            {
                MessageBox.Show("Modelul nu a putut fi adăugat în baza de date.");
            }
        }

        private void btnModificareModel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model model = db.Model.Where(x => x.id_model == modelSelectat.id_model).FirstOrDefault();
                model = _construireModel(model);

                db.Model.AddOrUpdate(model);
                db.SaveChanges();

                this.NavigationService.Navigate(new ListaModele());
            }
            catch
            {
                MessageBox.Show("Modelul nu a putut fi modificat din baza de date.");
            }
        }

        private Model _construireModel(Model model)
        {
            model.denumire = tbDenumire.Text;
            model.id_marca = int.Parse(cbMarca.SelectedValue.ToString());

            return model;
        }

        private void _afisareModel(Model model)
        {
            tbDenumire.Text = model.denumire.Trim();
            cbMarca.SelectedValue = model.id_marca;
        }
    }
}