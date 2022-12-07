﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPhieuMuonTra
    {
        private static DALPhieuMuonTra instance;

        public static DALPhieuMuonTra Instance 
        { 
            get
            {
                if (instance == null) instance = new DALPhieuMuonTra();
                return instance;
            }
            set => instance = value; 
        }

        public List<PHIEUMUONTRA> GetAllPhieuMuonTra()
        {
            return QLTVDb.Instance.PHIEUMUONTRAs.ToList();
        }

        public PHIEUMUONTRA GetPhieuMuonTraById(int id)
        {
            return QLTVDb.Instance.PHIEUMUONTRAs.Find(id);
        }

        public List<PHIEUMUONTRA> FindTheoNgayMuon (int? ngay, int? thang, int? nam)
        {
            List<PHIEUMUONTRA> res = GetAllPhieuMuonTra();
            if (ngay != null) res = res.Where(p => p.NgayMuon.Value.Day == ngay).ToList();
            if (thang != null) res = res.Where(p => p.NgayMuon.Value.Month == thang).ToList();
            if (nam != null) res = res.Where(p => p.NgayMuon.Value.Year == nam).ToList();
            return res;

        }

        public List<PHIEUMUONTRA> FindTheoNgayTra(int? ngay, int? thang, int? nam)
        {
            List<PHIEUMUONTRA> res = GetAllPhieuMuonTra();
            if (ngay != null) res = res.Where(p => p.NgayTra.Value.Day == ngay).ToList();
            if (thang != null) res = res.Where(p => p.NgayTra.Value.Month == thang).ToList();
            if (nam != null) res = res.Where(p => p.NgayTra.Value.Year == nam).ToList();
            return res;
        }
        public List<PHIEUMUONTRA> FindPhieuMuonTre(DateTime today)
        {
            return QLTVDb.Instance.PHIEUMUONTRAs.Where(p => p.NgayTra == null && p.HanTra < today).ToList();
        }

        public List<PHIEUMUONTRA> FindPhieuMuonTre(List<PHIEUMUONTRA> ds, DateTime today)
        {
            return ds.Where(p => p.NgayTra == null && p.HanTra < today).ToList();
        }
        public bool AddPhieuMuonTra(int idDocGia, int idCuonSach, DateTime? ngayMuon, DateTime? hanTra)
        {
            try
            {
                var phieu = new PHIEUMUONTRA
                {
                    idDocGia = idDocGia,
                    idCuonSach = idCuonSach,
                    NgayMuon = ngayMuon,
                    HanTra = hanTra
                };
                // mark cuonsach as borrowed
                DALCuonSach.Instance.UpdCuonSach(idCuonSach, 1);
                QLTVDb.Instance.PHIEUMUONTRAs.Add(phieu);
                QLTVDb.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                return false;
            }
        }

        public bool UpdPhieuMuonTra(int soPhieu, DateTime? ngayMuon, DateTime? hanTra, DateTime? ngayTra, int? soTienPhat)
        {
            try
            {
                var phieu = GetPhieuMuonTraById(soPhieu);
                if (phieu == null) return false;
                if (ngayMuon != null) phieu.NgayMuon = ngayMuon;
                if (hanTra != null) phieu.HanTra = hanTra;
                if (ngayTra != null)
                {
                    phieu.NgayTra = ngayTra;
                    // mark cuonsach as unborrowed
                    DALCuonSach.Instance.UpdCuonSach(phieu.idCuonSach, 0);
                }
                if (soTienPhat != null) phieu.SoTienPhat = soTienPhat;
                QLTVDb.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                return false;
            }
        }

        public bool DelPhieuMuonTra(int soPhieu)
        {
            try
            {
                var phieu = GetPhieuMuonTraById(soPhieu);
                if (phieu == null) return false;
                QLTVDb.Instance.PHIEUMUONTRAs.Remove(phieu);
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
