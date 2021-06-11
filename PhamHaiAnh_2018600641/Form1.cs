using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PhamHaiAnh_2018600641
{
    public partial class Form1 : Form
    {
        XmlDocument doc = new XmlDocument();
        string path = @"C:\Users\haian\Desktop\PhamHaiAnh_2018600641\PhamHaiAnh_2018600641\nhanvien.xml";
        int d;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hienthi();
        }
        private void hienthi()
        {
            dataGridView1.Rows.Clear();
            doc.Load(path);
            XmlNodeList ds = doc.SelectNodes("/congty/nhanvien");
            int sd = 0;
            dataGridView1.ColumnCount = 4;
            dataGridView1.Rows.Add();
            foreach (XmlNode nv in ds)
            {
                XmlNode manv = nv.SelectSingleNode("manv");
                dataGridView1.Rows[sd].Cells[0].Value = manv.InnerText.ToString();
                XmlNode hoten = nv.SelectSingleNode("hoten");
                dataGridView1.Rows[sd].Cells[1].Value = hoten.InnerText.ToString();
                XmlNode donvi = nv.SelectSingleNode("donvi");
                XmlNode dienthoai = donvi.SelectSingleNode("@dienthoai");
                dataGridView1.Rows[sd].Cells[2].Value = dienthoai.InnerText.ToString();
                XmlNode tendonvi = donvi.SelectSingleNode("tendonvi");
                dataGridView1.Rows[sd].Cells[3].Value = tendonvi.InnerText.ToString();
                dataGridView1.Rows.Add();
                sd++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode matrung = doc.SelectSingleNode("/congty/nhanvien[manv='" + txt_manv.Text + "']");
            if (matrung != null)
            {
                MessageBox.Show("Đã có mã nhân viên");
            }
            else
            {
                XmlNode nv = doc.CreateElement("nhanvien");

                XmlNode manv = doc.CreateElement("manv");
                manv.InnerText = txt_manv.Text;
                nv.AppendChild(manv);
                //họ tên
                XmlNode hoten = doc.CreateElement("hoten");
                hoten.InnerText = txt_hoten.Text;
                nv.AppendChild(hoten);
                //dơn vị
                XmlNode donvi = doc.CreateElement("donvi");

                XmlAttribute dienthoai = doc.CreateAttribute("dienthoai");
                dienthoai.InnerText = txt_dienthoai.Text;
                donvi.Attributes.Append(dienthoai);

                XmlNode tendonvi = doc.CreateElement("tendonvi");
                tendonvi.InnerText = txt_tendonvi.Text;
                donvi.AppendChild(tendonvi);

                nv.AppendChild(donvi);
                goc.AppendChild(nv);
            }
            

            doc.Save(path);
            hienthi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nvXoa = goc.SelectSingleNode("/congty/nhanvien[manv='" + txt_manv.Text + "']");
            MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            goc.RemoveChild(nvXoa);
            doc.Save(path);
            hienthi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doc.Load(path);
            XmlElement goc = doc.DocumentElement;
            XmlNode nv = goc.SelectSingleNode("/congty/nhanvien/donvi[tendonvi='" + txt_timkiem.Text + "']");

            if (txt_timkiem.TextLength == 0)
            {
                MessageBox.Show("Ban chua nhap tu tim kiem");
            }
            else
            {
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = 4;
                dataGridView1.Rows.Add();
                XmlNode manv = nv.SelectSingleNode("manv");
                dataGridView1.Rows[0].Cells[0].Value = manv.InnerText.ToString();
                XmlNode hoten = nv.SelectSingleNode("hoten");
                dataGridView1.Rows[0].Cells[1].Value = hoten.InnerText.ToString();
                XmlNode donvi = nv.SelectSingleNode("donvi");
                XmlNode dienthoai = donvi.SelectSingleNode("@dienthoai");
                dataGridView1.Rows[0].Cells[2].Value = dienthoai.InnerText.ToString();
                XmlNode tendonvi = donvi.SelectSingleNode("tendonvi");
                dataGridView1.Rows[0].Cells[3].Value = tendonvi.InnerText.ToString();
                dataGridView1.Rows.Add();

            }
        }
    }
}
