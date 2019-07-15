using Aplicatie_Service_Auto.models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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
    /// Interaction logic for ListaInterventie.xaml
    /// </summary>
    public partial class ListaInterventii : Page
    {
        private static Interventie interventieSelectat = null;

        private void btnAdaugareInterventie_Click(object sender, RoutedEventArgs e) => this.NavigationService.Navigate(new FormularInterventie());

        AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public ListaInterventii()
        {
            InitializeComponent();

            lvInterventii.ItemsSource = db.Interventie.ToList();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            interventieSelectat = lvInterventii.SelectedItem as Interventie;

            if (interventieSelectat != null)
            {
                this.NavigationService.Navigate(new FormularInterventie(interventieSelectat));
            }
        }

        private Interventie _interventieSlectata;
        private void btnFactura_Click(object sender, RoutedEventArgs e)
        {
            _interventieSlectata = lvInterventii.SelectedItem as Interventie;
            if (_interventieSlectata != null)
            {
                try
                {
                    var adresa = "Aleea Magnoliei, Nr 2";
                    var email = "serviceauto@gmail.com";
                    double? total = 0;
                    var data = DateTime.UtcNow;
                    var listaServicii = _interventieSlectata.Interventie_Serviciu.ToList();

                    var numarFactura = _interventieSlectata.id_interventie;
                    string docName = "factura-" + numarFactura.ToString() + ".pdf";
                    
                    //Create Doc
                    Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                    PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(docName, FileMode.Create));
                    doc.Open();
                    
                    //Title
                    iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph("Service Auto SRL");
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    doc.Add(paragraph);
                    doc.Add(new iTextSharp.text.Paragraph("\n"));
                    LineSeparator line1 = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 1);
                    doc.Add(line1);
                    iTextSharp.text.Paragraph paragraph2 = new iTextSharp.text.Paragraph("Factura Nr: " + numarFactura.ToString());
                    paragraph2.Alignment = Element.ALIGN_CENTER;
                    doc.Add(paragraph2);
                    doc.Add(new iTextSharp.text.Paragraph("\n"));
                    doc.Add(line1);
                    //Date Firma
                    doc.Add(new iTextSharp.text.Paragraph("\n"));
                    doc.Add(new iTextSharp.text.Paragraph(" Interventie: " + _interventieSlectata.id_interventie + "\n Client: " + _interventieSlectata.Client.nume + "\n Adresa: " + adresa + "\n Email: " + email + "\n Data: " + data));
                    //Creare Tabel Servicii
                    doc.Add(new iTextSharp.text.Paragraph("\n \n"));
                    PdfPTable table = new PdfPTable(4);

                    table.AddCell("Serviciu");
                    table.AddCell("Descriere");
                    table.AddCell("Durata Medie");
                    table.AddCell("Pret");
                    PdfPCell cell = new PdfPCell(new Phrase("Cell Text", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD, BaseColor.BLACK)));
                    cell.BackgroundColor = new BaseColor(0, 150, 0);
         
                    foreach (var s in listaServicii)
                    {
                        table.AddCell(s.Serviciu.denumire);
                        table.AddCell(s.Serviciu.descriere);
                        table.AddCell(s.Serviciu.durata_medie.ToString());
                        table.AddCell(s.pret.ToString() + " lei");
                        total = total + s.pret;
                    }

                    table.AddCell("");
                    table.AddCell("");                    
                    table.AddCell("Total: ");
                    table.AddCell(total.ToString() + " lei");
                    doc.Add(table);
                
                    doc.Close();
                    System.Diagnostics.Process.Start(docName);
                                                    
                    //Modificare in baza de date
                    _interventieSlectata.status = 1;
                    _interventieSlectata.data_finalizare = DateTime.UtcNow;
                    db.Interventie.AddOrUpdate(_interventieSlectata);
                    db.SaveChanges();

                    //Refres
                    this.NavigationService.Navigate(new ListaInterventii());
                }
                catch
                {
                    MessageBox.Show("A intervenit o problema in generarea facturii");
                }
            }
            else
            {
                MessageBox.Show("Mai intai va rog sa selectati o interventie activa");
            }

        }
    }
}