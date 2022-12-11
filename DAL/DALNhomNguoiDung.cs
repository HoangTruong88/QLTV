﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALNhomNguoiDung
    {
        private static DALNhomNguoiDung instance;

        public static DALNhomNguoiDung Instance 
        { 
            get
            {
                if (instance == null) instance = new DALNhomNguoiDung();
                return instance;
            }
            set => instance = value; 
        }

        public List<NHOMNGUOIDUNG> GetAllNhomNguoiDung ()
        {
            return QLTVDb.Instance.NHOMNGUOIDUNGs.ToList ();
        }

        public NHOMNGUOIDUNG GetNhomNguoiDungById (int id)
        {
            return QLTVDb.Instance.NHOMNGUOIDUNGs.Find(id);
        }

        public NHOMNGUOIDUNG GetNhomNguoiDungByMa (string ma)
        {
            var res = QLTVDb.Instance.NHOMNGUOIDUNGs.Where(n => n.MaNhomNguoiDung == ma);
            return (res.Any() ? res.First() : null);
        }

        public bool AddNhomNguoiDung (string tenNhom)
        {
            try
            {
                var nhom = new NHOMNGUOIDUNG
                {
                    TenNhomNguoiDung = tenNhom
                };
                QLTVDb.Instance.NHOMNGUOIDUNGs.Add(nhom);
                QLTVDb.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.InnerException.ToString ());
                return false;
            }
        }

        public bool UpdNhomNguoiDung(int id, string tenNhom)
        {
            try
            {
                var nhom = GetNhomNguoiDungById(id);
                if (nhom == null) return false;
                nhom.TenNhomNguoiDung = tenNhom;
                QLTVDb.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                return false;
            }
        }

        public bool DelNhomNguoiDung (int id)
        {
            try
            {
                var nhom = GetNhomNguoiDungById(id);
                if (nhom == null) return false;
                QLTVDb.Instance.NHOMNGUOIDUNGs.Remove(nhom);
                QLTVDb.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                return false;
            }
        }
    }
}
