using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bibliotheque
{
    public partial class Adherent : Form
    {
        private static SqlConnection cn = new SqlConnection("Data Source=DESKTOP-0OHCLC8\\SQLEXPRESS;Initial Catalog=Bibliotheque;Integrated Security=True");

        public Adherent()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Inscrit";
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable t = new DataTable();
            t.Load(dr); //charger la table par le contenu du DataReader
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = t;
            dr.Close();
            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Focus();
        }
        private bool rechercher_inscrit() //Verification: l'existence d'un employe
        {
            bool p = false;
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Inscrit where id_i='" + textBox1.Text + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
                p = true;

            dr.Close();
            cn.Close();
            return p;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Champ Id vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (rechercher_inscrit() == false)
            {

                button1.PerformClick();

                MessageBox.Show("Adherent introuvable", "introuvable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();

            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select * from Inscrit where id_i='" + textBox1.Text + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
                dateTimePicker1.Value = DateTime.Parse(dr[3].ToString());
                textBox4.Text = dr[4].ToString();
                textBox5.Text = dr[5].ToString();
                textBox6.Text = dr[6].ToString();
                textBox7.Text = dr[7].ToString();
                textBox8.Text = dr[8].ToString();
                dr.Close();
                cn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox8.Text == "" || textBox7.Text == "") // contrôle de saisie
            {
                MessageBox.Show("Champ vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = String.Format("INSERT INTO Inscrit (nom_i, prenom_i, date_naissance, ville, email, telephone, Logine, passworde) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", textBox2.Text, textBox3.Text,dateTimePicker1.Value.ToShortDateString(), textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text) ;
                int r = cmd.ExecuteNonQuery(); // ou ecrire cmd.ExecuteNonQuery() sans retour
                if (r != 0)
                {
                    MessageBox.Show("Adherent bien ajouté", "ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();

                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox8.Text == "" || textBox7.Text == "") // contrôle de saisie
            {
                MessageBox.Show("Champ vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (rechercher_inscrit() == false)
            {

                MessageBox.Show("Adherent inexistant", "inexistant", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();

            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = String.Format("UPDATE Inscrit SET nom_i = '{0}', prenom_i = '{1}', date_naissance = '{2}', ville = '{3}', email = '{4}', telephone = '{5}', Logine = '{6}', passworde = '{7}'WHERE id_i = '{8}'", textBox2.Text, textBox3.Text,dateTimePicker1.Value.ToShortDateString(), textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox1.Text);
                int r = cmd.ExecuteNonQuery(); 
                if (r != 0)
                {
                    MessageBox.Show("Adherent bien Modifié", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();

                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Champ Code vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (rechercher_inscrit() == false)
            {

                MessageBox.Show("Adherent inexistant", "inexistant", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = String.Format("delete from Inscrit where id_i='{0}'", textBox1.Text);
                int r = cmd.ExecuteNonQuery();
                if (r != 0)
                {
                    MessageBox.Show("Adherent bien supprimé", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();
                    button1.PerformClick(); 


                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenCsvFile(string filePath)
        {
            try
            {
               
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening CSV file: {ex.Message}");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Inscrit";

            try
            {
                
                {
                    cn.Open();

                    using (SqlCommand command = new SqlCommand(query, cn))
                    using (SqlDataReader reader = command.ExecuteReader())
                    using (StreamWriter writer = new StreamWriter("Inscrit_information.csv"))
                    {
                        //  headers
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            writer.Write(reader.GetName(i));
                            if (i < reader.FieldCount - 1)
                            {
                                writer.Write(",");
                            }
                        }
                        writer.WriteLine();

                        // data rows
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                writer.Write(reader[i]);
                                if (i < reader.FieldCount - 1)
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                        }
                    }
                    
                }

                MessageBox.Show("Les informations des adherents ont ete exportees avec succès.");
                
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            // Obtenez le chemin complet du fichier CSV
            string cheminFichier = Path.GetFullPath("student_information.csv");

            // Affichez le chemin dans la console pour le débogage
            Console.WriteLine($"Chemin du fichier CSV : {cheminFichier}");

           

        
            OpenCsvFile(cheminFichier);
        }
    }
}
