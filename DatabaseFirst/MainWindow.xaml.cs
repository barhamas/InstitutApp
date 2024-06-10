using DatabaseFirst.Models;
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


namespace DatabaseFirst
{
    public partial class MainWindow : Window
    {
        private DBLIAGEEntities db;

        public MainWindow()
        {
            InitializeComponent();
            chargerTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db = new DBLIAGEEntities();
            Institut institut = new Institut
            {
                nom = cNom.Text,
                Adresse = cAdresse.Text,
                email = cEmail.Text,
                site_web = cSiteWeb.Text
            };
            db.Instituts.Add(institut);
            db.SaveChanges();
            MessageBox.Show("Institut enregistré avec succès");
            clearfields();
            chargerTable();
        }

        private void Button_Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (institutDataGrid.SelectedItem is Institut selectedInstitut)
            {
                db = new DBLIAGEEntities();
                var institutToUpdate = db.Instituts.Find(selectedInstitut.id);

                if (institutToUpdate != null)
                {
                    institutToUpdate.nom = cNom.Text;
                    institutToUpdate.Adresse = cAdresse.Text;
                    institutToUpdate.email = cEmail.Text;
                    institutToUpdate.site_web = cSiteWeb.Text;

                    db.SaveChanges();
                    MessageBox.Show("Institut modifié avec succès");
                    clearfields();
                    chargerTable();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un institut à modifier.");
            }
        }

        private void Button_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (institutDataGrid.SelectedItem is Institut selectedInstitut)
            {
                db = new DBLIAGEEntities();
                var institutToDelete = db.Instituts.Find(selectedInstitut.id);

                if (institutToDelete != null)
                {
                    db.Instituts.Remove(institutToDelete);
                    db.SaveChanges();
                    MessageBox.Show("Institut supprimé avec succès");
                    clearfields();
                    chargerTable();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un institut à supprimer.");
            }
        }

        private void Button_Annuler_Click(object sender, RoutedEventArgs e)
        {
            clearfields();
        }

        private void clearfields()
        {
            cNom.Text = "";
            cAdresse.Text = "";
            cEmail.Text = "";
            cSiteWeb.Text = "";
        }

        private void chargerTable()
        {
            db = new DBLIAGEEntities();
            var list = db.Instituts.ToList();
            institutDataGrid.ItemsSource = list;
        }

        private void institutDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (institutDataGrid.SelectedItem is Institut selectedInstitut)
            {
                cNom.Text = selectedInstitut.nom;
                cAdresse.Text = selectedInstitut.Adresse;
                cEmail.Text = selectedInstitut.email;
                cSiteWeb.Text = selectedInstitut.site_web;
            }
        }
    }
}
