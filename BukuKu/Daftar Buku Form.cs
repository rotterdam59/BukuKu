using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BukuKu
{
    public partial class Daftar_Buku_Form : Form
    {
        BindingList<ModelBuku> db = new BindingList<ModelBuku>();
        public Daftar_Buku_Form()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Daftar_Buku_Form_Load(object sender, EventArgs e)
        {
           dataGridView1.DataSource = db;
           LoadData();

            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
                
        }

        private void LoadData()
        {

            

            dataGridView1.Columns["TotalHalaman"].HeaderText = "Total Halaman";
            dataGridView1.Columns["HalamanDibaca"].HeaderText = "Halaman Dibaca";
            dataGridView1.Columns["SisaHalaman"].HeaderText = "Sisa Halaman";
   }

        private void Reset()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Judul - Penulis - Bahasa tidak boleh kosong", "PERINGATAN!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            int totalhalaman = (int)numericUpDown1.Value;
            int halamandibaca = (int)numericUpDown2.Value;

            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("Total Halaman Tidak boleh 0","PERINGATAN",MessageBoxButtons.OK);
                return;
            }

            if(halamandibaca > totalhalaman)
            {
                MessageBox.Show("Halaman Dibaca tidak bisa melebihi Total halaman","VALIDATE ERROR",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }


            var newbuku = new ModelBuku();
            newbuku.Judul = textBox1.Text.Trim();
            newbuku.Penulis = textBox2.Text.Trim();
            newbuku.Bahasa = textBox3.Text.Trim();

            int totalHalaman = (int)numericUpDown1.Value;
            int halamanDibaca = (int)numericUpDown2.Value;

          

            newbuku.TotalHalaman = totalHalaman;
            newbuku.HalamanDibaca = halamanDibaca;
 
            db.Add(newbuku);
            Reset();
            LoadData();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
     
                string sisaHalamanVal = row.Cells["SisaHalaman"].Value?.ToString();

                if (sisaHalamanVal == "Tamat" || sisaHalamanVal == "0")
                {
                    
                    row.DefaultCellStyle.BackColor = Color.DarkGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];


            lblTotalBuku.Text = "Total Halaman: " + row.Cells["TotalHalaman"].Value?.ToString();
            lblTotalDibaca.Text = "Total Halaman yang Pernah dibaca: " + row.Cells["HalamanDibaca"].Value?.ToString();
            lblBelumTerbaca.Text = "Total Halaman yang belum Terbaca: " + row.Cells["Sisahalaman"].Value?.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Pilih data terlebih dahulu");
                return;
            }

            int index = dataGridView1.CurrentRow.Index;
            string buku = db[index].Judul;

            DialogResult result = MessageBox.Show($"Apakah anda yakin ingin mengahapus buku {buku}?","PERINGATAN",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if(result != DialogResult.Yes)
            {
                return;
            }

            db.RemoveAt(index);
            LoadData();
            MessageBox.Show("Data Berhasil Dihapus");
        }
    }
}

public class ModelBuku
{
    public string Judul {  get; set; }
    public string Penulis { get; set; }
    public string Bahasa { get; set; }
    public int TotalHalaman { get; set; }
    public int HalamanDibaca { get; set; }

    public string SisaHalaman
    {
        get
        {
            int sisa = TotalHalaman - HalamanDibaca;
           
            return sisa <= 0 ? "Tamat" : sisa.ToString();

        }
    }
}