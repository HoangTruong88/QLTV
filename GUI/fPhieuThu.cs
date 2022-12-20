﻿using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class fPhieuThu : Form
    {
        public fPhieuThu()
        {
            InitializeComponent();
            init();
        }
        private DOCGIA DocGia;
        private void init()
        {
            TienThu = 0;
            dateNgayLap.Value = DateTime.Now.Date;
            comboDocGia.DataSource = BUSDocGia.Instance.GetAllDocGia();
            comboDocGia.ValueMember= "ID";
            comboDocGia.DisplayMember= "MaDocGia";
            comboDocGia.SelectedIndex = 0;
            DocGia = BUSDocGia.Instance.GetDocGia(Convert.ToInt32(comboDocGia.SelectedValue));
            labelNoHienTai.Text = DocGia.TongNoHienTai.ToString();
        }
        private int TienThu;
        private void siticoneTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (textTienThu.Text == null || textTienThu.Text == "") return;
            try 
            {
                TienThu = Convert.ToInt32(textTienThu.Text);
                Console.WriteLine(TienThu);
            }
            catch
            {
                textTienThu.Text = null;
                ErrorDia.Show("Số tiền thu sai format");
                return;
            }
            THAMSO thamso = BUSThamSo.Instance.GetAllThamSo();
            if(thamso.AD_QDKTTienThu == 1 && TienThu > DocGia.TongNoHienTai)
            {
                textTienThu.Text = null;
                ErrorDia.Show("Số tiền thu vượt quá quy định");
                return;
            }
            labelNoMoi.Text = ((int)DocGia.TongNoHienTai - (int)TienThu).ToString();
            
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            if (dateNgayLap.Value < DateTime.Now.Date)
            {
                ErrorDia.Show("Ngày lập không hợp lệ");
                return;
            }
            string err = BUSPhieuThu.Instance.AddPhieuThu(DocGia.ID, TienThu, dateNgayLap.Value.Date);
            if(err !="")
            {
                ErrorDia.Show(err);
                return;
            }
            SuccDia.Show("Thêm phiếu thu thành công");
            this.Close();
        }

        private void comboDocGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            DocGia = BUSDocGia.Instance.GetDocGia(Convert.ToInt32(comboDocGia.SelectedValue));
            labelNoHienTai.Text = DocGia.TongNoHienTai.ToString();
        }
    }
}
