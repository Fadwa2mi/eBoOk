using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bibliotheque
{
    public partial class Employee : Form
    {
        private static SqlConnection cn = new SqlConnection("Data Source=DESKTOP-0OHCLC8\\SQLEXPRESS;Initial Catalog=Bibliotheque;Integrated Security=True");

        public Employee()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)//Recherche
        {
            if (textBox1.Text == "")
                MessageBox.Show("Champ Id vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (rechercher_employe() == false)
            {

                button1.PerformClick();

                MessageBox.Show("Employe introuvable", "introuvable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();

            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select * from Employe where id_employe='" + textBox1.Text + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
                textBox4.Text = dr[3].ToString();
                textBox5.Text = dr[4].ToString();
                textBox6.Text = dr[5].ToString();
                textBox7.Text = dr[6].ToString();
                textBox8.Text = dr[7].ToString();
                dr.Close();
                cn.Close();
            }
            

        }

        private void button1_Click(object sender, EventArgs e) //Nv
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            textBox1.Focus();
        }
        private bool rechercher_employe() //Verification: l'existence d'un employe
        {
            bool p = false;
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Employe where id_employe='" + textBox1.Text + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
                p = true;

            dr.Close();
            cn.Close();
            return p;
        }

        private void button6_Click(object sender, EventArgs e) //Afficher tt les enployes
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Employe ";
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable t = new DataTable();
            t.Load(dr); //charger la table par le contenu du DataReader
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = t;
            dr.Close();
            cn.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)//Ajout
        {
            if ( textBox2.Text == "" || textBox3.Text == ""|| textBox4.Text == ""|| textBox5.Text == ""||textBox6.Text == ""|| textBox8.Text == ""||textBox7.Text == "") // contrôle de saisie
            {
                MessageBox.Show("Champ vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
          
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = String.Format("insert into Employe (nom_employe, prenom_e, email, telephone, adresse, Logine, Passworde) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", textBox2.Text,textBox3.Text, textBox4.Text,textBox5.Text,textBox6.Text,textBox7.Text,textBox8.Text);
                int r = cmd.ExecuteNonQuery(); // ou ecrire cmd.ExecuteNonQuery() sans retour
                if (r != 0)
                {
                    MessageBox.Show("Employe bien ajouté", "ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();

                }

            }
            
        }

        private void button3_Click(object sender, EventArgs e)//Modifier
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox8.Text == "" || textBox7.Text == "") // contrôle de saisie
            {
                MessageBox.Show("Champ vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (rechercher_employe() == false)
            {

                MessageBox.Show("Employe inexistant", "inexistant", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
                
            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = String.Format("UPDATE Employe SET nom_employe = '{0}',prenom_e='{1}', email = '{2}', telephone = '{3}' , adresse='{4}', Logine='{5}', Passworde='{6}' WHERE id_employe = '{7}'", textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox1.Text);
                int r = cmd.ExecuteNonQuery(); // ou ecrire cmd.ExecuteNonQuery() sans retour
                if (r != 0)
                {
                    MessageBox.Show("Employe bien Modifié", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();

                }

            }

        }

        private void button4_Click(object sender, EventArgs e)//Supprimer
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Champ Code vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (rechercher_employe() == false)
            {

                MessageBox.Show("Employe inexistant", "inexistant", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = String.Format("delete from Employe where id_employe='{0}'", textBox1.Text);
                int r = cmd.ExecuteNonQuery(); 
                if (r != 0)
                {
                    MessageBox.Show("Employe bien supprimé", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();
                    button1.PerformClick(); //appeler le bouton nouveau pour vider les champs


                }

            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void ID_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
