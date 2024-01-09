using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bibliotheque
{
    public partial class Auteur : Form
    {
        private static SqlConnection cn = new SqlConnection("Data Source=DESKTOP-0OHCLC8\\SQLEXPRESS;Initial Catalog=Bibliotheque;Integrated Security=True");
        public Auteur()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select a.id_a, a.nom_a, a.prenom_a , a.date_naissance , p.nom_p from Auteur a inner join Pays p on a.id_p=p.id_p ";
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable t = new DataTable();
            t.Load(dr); //charger la table par le contenu du DataReader
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = t;
            dr.Close();
            cn.Close();
        }

        private void Auteur_Load(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;

            cmd.CommandText = "select id_p from Pays";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());

            }
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
                cmd.CommandText = "SELECT nom_p  FROM Pays where id_p='" + comboBox1.Text + "'";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        textBox5.Text = dr["nom_p"].ToString();
                    }
                }
                cn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
        }
        private bool rechercher_Auteur() 
        {
            bool p = false;
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from Auteur where id_a='" + textBox1.Text + "'";
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
            else if (rechercher_Auteur() == false)
            {

                button1.PerformClick();

                MessageBox.Show("Auteur introuvable", "introuvable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();

            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select * from Auteur where id_a='" + textBox1.Text + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
                dateTimePicker1.Value = DateTime.Parse(dr[3].ToString());
              
                textBox5.Text = dr[4].ToString();
             
                dr.Close();
                cn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" ||  textBox5.Text == "" ) // contrôle de saisie
            {
                MessageBox.Show("Champ vide!!", "champ vide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Pay Invalide!!", "champ Invalide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
          

            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = String.Format("insert into Employe (nom_a, prenom_a, date_naissance, id_p) values('{0}','{1}','{2}','{3}')", textBox2.Text, textBox3.Text, dateTimePicker1.Value.ToShortDateString(), comboBox1.SelectedItem.ToString());
                int r = cmd.ExecuteNonQuery(); 
                if (r != 0)
                {
                    MessageBox.Show("Auteur bien ajouté", "ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();

                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
