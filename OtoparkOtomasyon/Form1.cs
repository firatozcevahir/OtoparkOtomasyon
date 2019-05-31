using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtoparkOtomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Control> parkYeri = new List<Control>();
        Dictionary<string, string> aracPark = new Dictionary<string, string>();
        Dictionary<string, DateTime> aracGiris = new Dictionary<string, DateTime>();
        string aPlaka = "";
        DateTime girisSaat;
        DateTime cikisSaat;
        private void Form1_Load(object sender, EventArgs e)
        {
            ParkYeriTanimla();
            label3.Text = "Bugün " + DateTime.Today.ToShortDateString();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            aPlaka = txtPlaka.Text;
            AracGiris(aPlaka);
        }


        private void btnCikis_Click(object sender, EventArgs e)
        {
            aPlaka = txtPlaka.Text;
            string aParkYeri = txtSecilenPark.Text;
            AracCikis(aPlaka, ListeIndexAl(parkYeri, aParkYeri));
        }

        private void ParkYeriTanimla()
        {
            //Hepsi Boş Olarak Listeye Ekleniyor (Text Özellikleri -BOŞ-)
            parkYeri.Add(lblPark1);
            parkYeri.Add(lblPark2);
            parkYeri.Add(lblPark3);
            parkYeri.Add(lblPark4);
            parkYeri.Add(lblPark5);
            parkYeri.Add(lblPark6);
            parkYeri.Add(lblPark7);
            parkYeri.Add(lblPark8);
        }

        public void AracGiris(string plaka)
        {
            foreach (var item in parkYeri)
            {
                if (plaka != "")
                {
                    if (!aracPark.Keys.Contains(plaka))
                    {
                        if (item.Text == "BOŞ")
                        {
                            if (txtSecilenPark.Text != "" && item.Name != txtSecilenPark.Text)
                                continue;

                            girisSaat = DateTime.Now;

                            aracPark.Add(plaka, item.Name);
                            aracGiris.Add(plaka, girisSaat);
                            txtSecilenPark.Text = "";
                            item.BackColor = Color.FromArgb(255, 128, 128);
                            item.Text = plaka;
                            
                            listBox1.Items.Add(plaka + "|   " + girisSaat.ToShortTimeString());
                            txtPlaka.Text = "";
                            MessageBox.Show(plaka + " Plakalı Araç Giriş Yaptı","Bilgi");
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show(plaka + " Plakalı Araç Zaten Park yerinde","Uyarı");
                        break;
                    }
                }
                else
                {
                    MessageBox.Show("Bir Araç Plakası Giriniz");
                    break;
                }
            }
        }

        public void AracCikis(string plaka, int lblIndex)
        {
            if (aracPark.Keys.Contains(plaka))
            {
                parkYeri[lblIndex].BackColor = Color.FromArgb(192, 255, 192);
                parkYeri[lblIndex].Text = "BOŞ";
                aracPark.Remove(plaka);
                txtSecilenPark.Text = "";
                txtPlaka.Text = "";
                Fiyatlandirma(aPlaka);
                MessageBox.Show(plaka + " Plakalı Araç Çıkış Yaptı");
            }
            else
                MessageBox.Show("Böyle Bir Araç Yok");
        }
        public int ListeIndexAl(List<Control> geciciListe, string lblParkYeri)
        {
            int index = -1;
            foreach (var value in geciciListe)
            {
                index++;
                if (value.Name == lblParkYeri)
                    return index;
            }
            return -1;
        }

        public void Fiyatlandirma(string plaka)
        {
            int ucretlendirme = 0;
            cikisSaat = DateTime.Now;
            ucretlendirme = Convert.ToInt32((cikisSaat - aracGiris[plaka]).TotalHours);
            if (ucretlendirme == 0) ucretlendirme = 1;
            string sonuc = plaka + " | Park Ücreti : " + (ucretlendirme * 5).ToString("C");
            lblFiyatlandirma.Text = sonuc;
            this.Text = sonuc;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region ManuelParkSecim

        private void lblPark1_Click(object sender, EventArgs e)
        {
            ManuelParkYeriSec(0);
        }

        private void lblPark2_Click(object sender, EventArgs e)
        {
            ManuelParkYeriSec(1);
        }
        private void lblPark3_Click(object sender, EventArgs e)
        {
            ManuelParkYeriSec(2);
        }

        private void lblPark4_Click(object sender, EventArgs e)
        {
            ManuelParkYeriSec(3);
        }

        private void lblPark5_Click(object sender, EventArgs e)
        {
            ManuelParkYeriSec(4);
        }

        private void lblPark6_Click(object sender, EventArgs e)
        {
            ManuelParkYeriSec(5);
        }

        private void lblPark7_Click(object sender, EventArgs e)
        {
            ManuelParkYeriSec(6);
        }

        private void lblPark8_Click(object sender, EventArgs e)
        {
            ManuelParkYeriSec(7);
        }

        public void ManuelParkYeriSec(int index)
        {
            string secilenPark = parkYeri[index].Name.ToString();
            if (parkYeri[index].Text == "BOŞ")
            {
                txtSecilenPark.Text = secilenPark;
            }
            else
            {
                txtSecilenPark.Text = secilenPark;
                txtPlaka.Text = parkYeri[index].Text;
            }
        }
        #endregion

        private void txtPlaka_TextChanged(object sender, EventArgs e)
        {
            string secilenPark = txtPlaka.Text;

            foreach (var item in parkYeri)
            {
                if(secilenPark == item.Text)
                    txtSecilenPark.Text = item.Name;
            }
        }
    }
}
