using Aplicatie_Service_Auto.models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ListaMasini.xaml
    /// </summary>
    public partial class ListaMasini : Page
    {
        private static Masina masinaSelectat = null;

        private void btnAdaugareMasina_Click(object sender, RoutedEventArgs e) => this.NavigationService.Navigate(new FormularMasina());

        AplicatieServiceAutoEntities db = new AplicatieServiceAutoEntities();

        public ListaMasini()
        {
            InitializeComponent();

            lvMasini.ItemsSource = db.Masina.ToList();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            masinaSelectat = lvMasini.SelectedItem as Masina;

            if (masinaSelectat != null)
            {
                this.NavigationService.Navigate(new FormularMasina(masinaSelectat));
            }
        }

        private void btnFisaMasina_Click(object sender, RoutedEventArgs e)
        {
            masinaSelectat = lvMasini.SelectedItem as Masina;
            if (masinaSelectat != null)
            {
                try
                {      
                    var data = DateTime.UtcNow;
                    var listaInterventii = db.Interventie_Serviciu.Where(x => x.Interventie.Masina.id_masina == masinaSelectat.id_masina);
                    string docName = "fisaMasina-" + masinaSelectat.nr_inmatriculare.ToString() + ".pdf";

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

                    //Date Firma
                    doc.Add(new iTextSharp.text.Paragraph("\n"));
                    doc.Add(new iTextSharp.text.Paragraph(" Fisa Masinii"));
                    doc.Add(new iTextSharp.text.Paragraph("\n Numar inmatriculare: " + masinaSelectat.nr_inmatriculare + "\n Proprietar: " + masinaSelectat.Client.nume + "\n Marca: " + masinaSelectat.Marca.denumire + "\n Model: " + masinaSelectat.Model.denumire + "\n An Fabricatie: " + masinaSelectat.an_fabricatie + "\n Motorizare: " + masinaSelectat.motorizare + "\n Data: " + data));
                    //Creare Tabel Servicii
                    doc.Add(new iTextSharp.text.Paragraph("\n \n"));
                    PdfPTable table = new PdfPTable(4);

                    table.AddCell("Serviciu");
                    table.AddCell("Descriere");
                    table.AddCell("Durata Medie");
                    table.AddCell("Data");
                    PdfPCell cell = new PdfPCell(new Phrase("Cell Text", new Font(Font.FontFamily.HELVETICA, 8f, Font.BOLD, BaseColor.BLACK)));
                    cell.BackgroundColor = new BaseColor(0, 150, 0);

                    foreach (var s in listaInterventii)
                    {
                        table.AddCell(s.Serviciu.denumire);
                        table.AddCell(s.Serviciu.descriere);
                        table.AddCell(s.Serviciu.durata_medie.ToString());
                        table.AddCell(s.Interventie.data_finalizare.ToString());
                    }
                    
                    doc.Add(table);

                    doc.Close();
                    System.Diagnostics.Process.Start(docName);

                }
                catch
                {
                    MessageBox.Show("Nu s-a putut realiza fisa masinii selectate");
                }
            }
            else
            {
                MessageBox.Show("Nu s-a putut realiza fisa masinii selectate");
            }
        }
    }
}
