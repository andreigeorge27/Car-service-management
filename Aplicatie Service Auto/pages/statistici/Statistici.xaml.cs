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
    /// Interaction logic for Statistici.xaml
    /// </summary>
    public partial class Statistici : Page
    {
        AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();
        public Statistici()
        {
            InitializeComponent();
            nrAngajati.Text = nrAngajati.Text + " " + db.Angajat.Count();
            nrClienti.Text = nrClienti.Text + " " +  db.Client.Count();
            nrInterventii.Text = nrInterventii.Text + " " + db.Interventie.Count();
            nrMasini.Text = nrMasini.Text + " " + db.Masina.Count();

            DateTime ziCurenta = new DateTime();
            ziCurenta = DateTime.UtcNow;
            nrInerventiiNoi.Text = nrInerventiiNoi.Text + " " + db.Interventie.Where(x => x.data_finalizare.Value.Year == ziCurenta.Year).Count();

            var listaInterventiiCurente = db.Interventie_Serviciu.Where(x => x.Interventie.data_finalizare.Value.Year == ziCurenta.Year).ToList();
            double? sumaTotala = 0;
            foreach(var i in listaInterventiiCurente)
            {
                sumaTotala += i.pret;
            }
            incasari.Text = incasari.Text + " " + sumaTotala.ToString();

            nrInerventiiLunaCurenta.Text = nrInerventiiLunaCurenta.Text + " " + db.Interventie.Where(x => (x.data_finalizare.Value.Month == ziCurenta.Month) && (x.data_finalizare.Value.Year == ziCurenta.Year)).Count();

            var listaInterventiiLunaCurenta = db.Interventie_Serviciu.Where(x => (x.Interventie.data_finalizare.Value.Month == ziCurenta.Month) && (x.Interventie.data_finalizare.Value.Year == ziCurenta.Year)).ToList();
            double? sumaTotalaLunaCurenta = 0;
            foreach (var i in listaInterventiiLunaCurenta)
            {
                sumaTotalaLunaCurenta += i.pret;
            }
            incasariLunaCurenta.Text = incasariLunaCurenta.Text + " " + sumaTotalaLunaCurenta.ToString();
        }
    }
}
