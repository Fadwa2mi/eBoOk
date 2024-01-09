using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Bibliotheque
{
    public partial class Livres : Form
    {
        private static SqlConnection cn = new SqlConnection("Data Source=DESKTOP-0OHCLC8\\SQLEXPRESS;Initial Catalog=Bibliotheque;Integrated Security=True");
        public Livres()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "pdf",
                Filter = "pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textPdf.Text = openFileDialog1.FileName.ToString();
                axAcroPDF1.src = openFileDialog1.FileName;
            }
        }
        private bool rechercher_livre() 
        {
            bool p = false;
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Livre where id_l='" + textID.Text + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
                p = true;

            dr.Close();
            cn.Close();
            return p;
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            if (textID.Text == "" || textResume.Text == "" || comboBox1.Text == ""||textImage.Text==""||textPdf.Text==""||textTitre.Text==""||comboBox2.Text=="")
            {
                MessageBox.Show("Champ vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
         
            else if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Auteur Invalide!!", "champ Invalide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Type Invalide!!", "champ Invalide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (rechercher_livre() == false)
            {

                MessageBox.Show("Livre inexistant", "inexistant", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textID.Focus();
            }
            else
            {
                string message = "";

                if (checkBox1.Checked)
                {
                    message += "Romance, ";
                }

                if (checkBox2.Checked)
                {
                    message += "Mystère, ";
                }

                if (checkBox3.Checked)
                {
                    message += "Science-Fiction, ";
                }

                if (checkBox4.Checked)
                {
                   message+="Fantasy, ";
                }

                if (checkBox5.Checked)
                {
                    message += "Thriller, ";
                }

                if (checkBox6.Checked)
                {
                    message += "Historique, ";
                }

                if (checkBox7.Checked)
                {
                    message += "Aventure, ";
                }

                if (checkBox8.Checked)
                {
                    message += "Horreur, ";
                }

                if (checkBox9.Checked)
                {
                    message += "Humoristique, ";
                }

                if (checkBox10.Checked)
                {
                    message += "Biographique, ";
                }

                if (checkBox11.Checked)
                {
                    message += "Psychologique, ";
                }

                if (checkBox12.Checked)
                {
                    message += "Policier, ";
                }

                if (!string.IsNullOrEmpty(message))
                {
                    message = message.Substring(0, message.Length - 2);
                }

                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "UPDATE Livre SET titre_l = @title, annee_l = @anne, resume_l = @resume, id_auteur = @id_auteur, fichier = @fichier, images = @images, genre = @genre , type =@type WHERE id_l=@id_l ";

                cmd.Parameters.AddWithValue("@title", textTitre.Text);
                cmd.Parameters.AddWithValue("@anne", dateTimePicker1.Value.ToShortDateString());
                cmd.Parameters.AddWithValue("@resume", textResume.Text);
                cmd.Parameters.AddWithValue("@id_auteur", comboBox1.SelectedItem); // Assuming comboBox1.SelectedValue contains the id_auteur
                cmd.Parameters.AddWithValue("@genre", message);
                cmd.Parameters.AddWithValue("@type", comboBox2.SelectedItem);
                cmd.Parameters.AddWithValue("@id_l", textID.Text);

                // Handle file data for 'fichier'
                if (!string.IsNullOrEmpty(textPdf.Text))
                {
                    byte[] fichierData = File.ReadAllBytes(textPdf.Text);
                    cmd.Parameters.AddWithValue("@fichier", fichierData);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@fichier", DBNull.Value);
                }

                // Handle file data for 'images'
                if (!string.IsNullOrEmpty(textImage.Text))
                {
                    byte[] imageData = File.ReadAllBytes(textImage.Text);
                    cmd.Parameters.AddWithValue("@images", imageData);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@images", DBNull.Value);
                }

                int r = cmd.ExecuteNonQuery();
                if (r != 0)
                {
                    MessageBox.Show("Livre bien modifié", "modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                cn.Close();


            }
        }

        private void Livres_Load(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
           
            cmd.CommandText = "select id_a from Auteur";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                comboBox1.Items.Add(dr[0].ToString());

            }
            dr.Close();
            cn.Close();

            comboBox2.Items.Clear();
            comboBox2.Items.Add("Roman");
            comboBox2.Items.Add("Poesie");


        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            textPdf.Text = string.Empty;
            textID.Text = string.Empty;
            textTitre.Text = string.Empty;
            textResume.Text = string.Empty;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            textAuteur.Text = string.Empty;
            textImage.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            axAcroPDF1.src = "";
            pictureBox1.Image = null;
        }

        private void axAcroPDF1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textResume.Text==""||textImage.Text==""|| textPdf.Text == ""||textTitre.Text=="") // contrôle de saisie
            {
                MessageBox.Show("Champ vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Auteur Invalide!!", "champ Invalide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Type Invalide!!", "champ Invalide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

              
                StringBuilder message = new StringBuilder("");

                if (checkBox1.Checked)
                {
                    message.Append("Romance, ");
                }

                if (checkBox2.Checked)
                {
                    message.Append("Mystère, ");
                }

                if (checkBox3.Checked)
                {
                    message.Append("Science-Fiction, ");
                }

                if (checkBox4.Checked)
                {
                    message.Append("Fantasy, ");
                }

                if (checkBox5.Checked)
                {
                    message.Append("Thriller, ");
                }

                if (checkBox6.Checked)
                {
                    message.Append("Historique, ");
                }

                if (checkBox7.Checked)
                {
                    message.Append("Aventure, ");
                }

                if (checkBox8.Checked)
                {
                    message.Append("Horreur, ");
                }

                if (checkBox9.Checked)
                {
                    message.Append("Humoristique, ");
                }

                if (checkBox10.Checked)
                {
                    message.Append("Biographique, ");
                }

                if (checkBox11.Checked)
                {
                    message.Append("Psychologique, ");
                }

                if (checkBox12.Checked)
                {
                    message.Append("Policier, ");
                }

             
                // Supprimer la virgule et l'espace à la fin de la chaîne
                if (message.Length > 2)
                {
                    message.Length -= 2;
                }


                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandText = "INSERT INTO Livre (titre_l, annee_l, resume_l, id_auteur, fichier, images, genre, type) VALUES (@title, @anne, @resume, @id_auteur, @fichier, @images, @genre, @type)";

                    cmd.Parameters.AddWithValue("@title", textTitre.Text);
                    cmd.Parameters.AddWithValue("@anne", dateTimePicker1.Value.ToShortDateString());
                    cmd.Parameters.AddWithValue("@resume", textResume.Text);
                    cmd.Parameters.AddWithValue("@id_auteur", comboBox1.SelectedItem); // Assuming comboBox1.SelectedValue contains the id_auteur
                    cmd.Parameters.AddWithValue("@genre", message.ToString());
                    cmd.Parameters.AddWithValue("@type", comboBox2.SelectedItem);
                    // Handle file data for 'fichier'
                    if (!string.IsNullOrEmpty(textPdf.Text))
                    {
                        byte[] fichierData = File.ReadAllBytes(textPdf.Text);
                        cmd.Parameters.AddWithValue("@fichier", fichierData);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@fichier", DBNull.Value);
                    }

                    // Handle file data for 'images'
                    if (!string.IsNullOrEmpty(textImage.Text))
                    {
                        byte[] imageData = File.ReadAllBytes(textImage.Text);
                        cmd.Parameters.AddWithValue("@images", imageData);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@images", DBNull.Value);
                    }

                    int r = cmd.ExecuteNonQuery();
                    if (r != 0)
                    {
                        MessageBox.Show("Livre bien ajouté", "ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'ajout du livre : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }


            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file name and display it in a TextBox
                string selectedFileName = openFileDialog.FileName;
                textImage.Text = selectedFileName;

                // Load the image into the PictureBox
                pictureBox1.Image = Image.FromFile(selectedFileName);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select titre_l, annee_l, resume_l, id_auteur, genre, type from Livre";
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable t = new DataTable();
            t.Load(dr);
            dataGridView1.DataSource = t; // la liste d'affichage (DataGridView)
            dr.Close();
            cn.Close();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT CONCAT(nom_a, prenom_a) AS full_name FROM Auteur where id_a='" + comboBox1.Text + "'";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read()) 
                    {
                        textAuteur.Text = dr["full_name"].ToString();
                    }
                }
                cn.Close();
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textID.Text == "") 
                MessageBox.Show("Champ Code vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (rechercher_livre() == false) 
            {
                MessageBox.Show("Livre inexistant", "inexistant", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textID.Focus();

            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select * from Livre where id_l='" + textID.Text + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable t = new DataTable();
                t.Load(dr);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = t;
                dr.Close();
                cn.Close();

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textID.Text == "")
            {
                MessageBox.Show("Champ Code vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (rechercher_livre() == false)
            {

                MessageBox.Show("Livre inexistant", "inexistant", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textID.Focus();
            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = String.Format("delete from Livre where id_l='{0}'", textID.Text);
                int r = cmd.ExecuteNonQuery(); 
                if (r != 0)
                {
                    MessageBox.Show("Livre bien supprimé", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();
                    button2.PerformClick(); 


                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textAuteur_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
