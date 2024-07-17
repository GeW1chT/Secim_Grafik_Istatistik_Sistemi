using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Veri_Tabanlı_Parti_Seçim_Grafik_İstatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-VMV2MRC\MSSQLSERVER01; Initial Catalog = DBSECIMPROJE; Integrated Security = True");
        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            //Sandık Adlarını Comboboxa çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select SANDIKNO from TBLSANDIK",baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            baglanti.Close();

            //Grafiğe Toplam Sonuçları Getirme
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select SUM(BERAT), SUM(EFE), SUM(SELİM), SUM(EMİR), SUM(MUFFİN) FROM TBLSANDIK", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();  
            while (dr2.Read()) 
            {
                chart1.Series["Partiler"].Points.AddXY("BERAT", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("EFE", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("SELİM", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("EMİR", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("MUFFİN", dr2[4]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TBLSANDIK where SANDIKNO=@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) 
            {
                progressBar1.Value = int.Parse(dr[2].ToString());
                progressBar2.Value = int.Parse(dr[3].ToString());
                progressBar3.Value = int.Parse(dr[4].ToString());
                progressBar4.Value = int.Parse(dr[5].ToString());
                progressBar5.Value = int.Parse(dr[6].ToString());

                LblA.Text = dr[2].ToString();
                LblB.Text = dr[3].ToString();
                LblC.Text = dr[4].ToString();
                LblD.Text = dr[5].ToString();
                LblE.Text = dr[6].ToString();
            }
            baglanti.Close();
        }
    }
}