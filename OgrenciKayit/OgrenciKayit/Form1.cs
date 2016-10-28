using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciKayit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCinsiyet.Items.Add("Erkek");
            cmbCinsiyet.Items.Add("Kadın");
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                //Ogrenci ogr = new Ogrenci(txtAd.Text, txtSoyad.Text, dtpDogumTarihi.Value, cmbCinsiyet.SelectedIndex == 0 ? true : false);
                Ogrenci ogr = new Ogrenci();
                ogr.Ad = txtAd.Text;
                ogr.Soyad = txtSoyad.Text;
                ogr.DogumTarihi = dtpDogumTarihi.Value;
                ogr.ErkekMi = cmbCinsiyet.SelectedIndex == 0 ? true : false;
                lstOgrenci.Items.Add(ogr);
                ogrenciler.Add(ogr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FormuTemizle();
            }
        }
        private void FormuTemizle()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
                else if (item is ComboBox)
                    (item as ComboBox).SelectedIndex = -1;
                else if (item is DateTimePicker)
                    (item as DateTimePicker).Value = DateTime.Now;
            }
        }

        private void lstOgrenci_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOgrenci.SelectedItem == null) return;

            //Ogrenci secilenOgrenci = lstOgrenci.SelectedItem as Ogrenci;
            Ogrenci secilenOgrenci = (Ogrenci)lstOgrenci.SelectedItem;
            txtAd.Text = secilenOgrenci.Ad;
            txtSoyad.Text = secilenOgrenci.Soyad;
            dtpDogumTarihi.Value = secilenOgrenci.DogumTarihi;
            cmbCinsiyet.SelectedIndex = secilenOgrenci.ErkekMi ? 0 : 1;
        }
        List<Ogrenci> ogrenciler = new List<Ogrenci>();
        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            string kelime = txtArama.Text.ToLower();
            List<Ogrenci> sonuc = ogrenciler.Where(x => x.Ad.ToLower().Contains(kelime) || x.Soyad.ToLower().Contains(kelime)).ToList(); // Linq 
            ListeyiDoldur(sonuc);
        }
        void ListeyiDoldur(List<Ogrenci> liste)
        {
            lstOgrenci.Items.Clear();
            foreach (Ogrenci item in liste)
            {
                lstOgrenci.Items.Add(item);
            }
        }
        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstOgrenci.SelectedItem == null)
            {
                MessageBox.Show("Neyi sileyim?");
                return;
            }
            Ogrenci silinecekOgrenci = lstOgrenci.SelectedItem as Ogrenci;
            if (MessageBox.Show($"{silinecekOgrenci.Ad} isimli öğrenci silinsin mi?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                ogrenciler.Remove(silinecekOgrenci);
            FormuTemizle();
            ListeyiDoldur(ogrenciler);
        }

        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstOgrenci.SelectedItem == null)
            {
                MessageBox.Show("Neyi güncelleyeyim?");
                return;
            }
            Ogrenci guncellenecekOgrenci = (Ogrenci)lstOgrenci.SelectedItem;
            guncellenecekOgrenci = ogrenciler.Where(x => x.ID == guncellenecekOgrenci.ID).First();
            guncellenecekOgrenci.Ad = txtAd.Text;
            guncellenecekOgrenci.Soyad = txtSoyad.Text;
            guncellenecekOgrenci.ErkekMi= cmbCinsiyet.SelectedIndex == 0 ? true : false;
            guncellenecekOgrenci.DogumTarihi = dtpDogumTarihi.Value;
            FormuTemizle();
            ListeyiDoldur(ogrenciler);

        }
    }
}
