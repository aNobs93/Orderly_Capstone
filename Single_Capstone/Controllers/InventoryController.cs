﻿using Microsoft.AspNet.Identity;
using Single_Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Single_Capstone.Controllers
{
    public class InventoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Inventory
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var business = db.Businesses.Where(b => b.ApplicationId == userId).FirstOrDefault();
            var inventory = db.Inventories.Where(i => i.BusinessId == business.Id).ToList();
            return View(inventory);
        }

        public ActionResult CalcInventoryValue(InventoryProducts inventoryProducts)
        {
            var inventory = db.Inventories.Where(i => i.Id == inventoryProducts.InventoryId).FirstOrDefault();
            inventory.TotalInventoryWorth += inventoryProducts.TotalValueOfProducts;
            inventory.ProfitMargin += (inventoryProducts.ProfitToBeMadePerUnit * inventoryProducts.Units);
            db.Entry(inventory).State = EntityState.Modified;
            db.SaveChanges();
             

            return RedirectToAction("Index");
        }

        public ActionResult SelectedInventoryDetails(int id)//This allows user to look deeper into previous inventory when they hit details of an inventory from the index
        {
            var inventoryProducts = db.InventoryProducts.Where(ip => ip.InventoryId == id).ToList();
            return View(inventoryProducts);
        }

        // GET: Inventory/Details/5
        public ActionResult Details()
        {
            var userId = User.Identity.GetUserId();
            var business = db.Businesses.Where(b => b.ApplicationId == userId).FirstOrDefault();
            var inventory = db.Inventories.Where(i => i.BusinessId == business.Id).FirstOrDefault();
            return View();
        }

        //public void InventoryWorth()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var business = db.Businesses.Where(b => b.ApplicationId == userId).FirstOrDefault();
        //    var inventory = db.Inventories.Where(i => i.BusinessId == business.Id).FirstOrDefault();
        //    //inventory.Products = db.Products.Where(p => p.InventoryId == inventory.Id).ToList();
        //    for (int i = 0; i < inventory.Products.Count; i++)
        //    {
        //        //inventory.TotalInventoryWorth += (inventory.Products[i].PricePerUnitPurchased * inventory.Products[i].Units);
        //    }//This finds the Inventory worth purchased price * units and adds the value to the ongoing total
        //    db.Entry(inventory).State = EntityState.Modified;
        //    db.SaveChanges();
        //}

        //public void InventoryProfitMargin()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var business = db.Businesses.Where(b => b.ApplicationId == userId).FirstOrDefault();
        //    var inventory = db.Inventories.Where(i => i.BusinessId == business.Id).FirstOrDefault();
        //    //inventory.Products = db.Products.Where(p => p.InventoryId == inventory.Id).ToList();
        //    for (int i = 0; i < inventory.Products.Count; i++)
        //    {
        //        //inventory.ProfitMargin += (inventory.Products[i].Units * (inventory.Products[i].PricePerUnitSelling - inventory.Products[i].PricePerUnitPurchased));
        //    }//This finds the Inventory profit to be made based off units selling price - purchased rate * units
        //    db.Entry(inventory).State = EntityState.Modified;
        //    db.SaveChanges();
        //}

        // GET: Inventory/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var business = db.Businesses.Where(b => b.ApplicationId == userId).FirstOrDefault();
            var findMax = db.Inventories.Where(f => f.BusinessId == business.Id).ToList();
            int max = findMax.Max(m => m.Id);
            Inventory inventory = new Inventory();
            var key = db.Inventories.Where(k => k.BusinessId == business.Id).FirstOrDefault();
            inventory.BusinessId = business.Id;
            inventory.GetDate = DateTime.Now.ToShortDateString();
            inventory.LastInventoryId = max;
            db.Inventories.Add(inventory);
            db.SaveChanges();
            if(key == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create", "InventoryProduct", inventory);
            
        }

       [HttpPost]
        public ActionResult Create(Inventory inventory)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
