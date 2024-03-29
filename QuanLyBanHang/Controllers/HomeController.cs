﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using QuanLyBanHang.Models;
using PagedList;
namespace QuanLyBanHang.Controllers
{
    public class HomeController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();
        public ActionResult Index(string currentFilter, int?page, int maloaisp = 0,string SearchString="")
        {
            if (SearchString != "")
            {
                page = 1;
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP).Where(x => x.TenSP.ToUpper().Contains(SearchString.ToUpper()));
                return View(sanPhams.ToList());
            }
            else
                SearchString = currentFilter;
            ViewBag.CurrentFilter = currentFilter;
            if(maloaisp == 0)
            {
                int pageSize = 12;
                int pageNumber = (page ?? 1);
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP).OrderBy(x => x.TenSP);
                // phải order trước skip
                return View(sanPhams.ToPagedList(pageNumber, pageSize));
            } 
            else
            {
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP).Where(x => x.MaLoaiSP == maloaisp);
                return View(sanPhams.ToList());
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}