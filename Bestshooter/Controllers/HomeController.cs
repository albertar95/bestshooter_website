using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bestshooter.Models.DomainModel;

namespace Bestshooter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MainPage()
        {
            ViewModel.FirstPageViewModel fpvm = new ViewModel.FirstPageViewModel();
            var pack = Helper.Utilities.FetchBestOrder();
            var games = Helper.Utilities.FetchGames();
            var setting = Helper.Utilities.FSetting();
            if (pack != null)
            {
                fpvm.MostOrdered = pack;
                fpvm.AllGames = games;
                fpvm.sets = setting;
            }
            else
            {
                var Apack = Helper.Utilities.AlternateOrder();
                fpvm.MostOrdered = Apack;
                fpvm.AllGames = games;
                fpvm.sets = setting;
            }
            return View(fpvm);
        }
        public ActionResult Games(string id)
        {
            Game g = Helper.Utilities.FetchGame(id);
            var l = Helper.Utilities.GatherPacks(g.Id);
            var q = Request.Cookies.AllKeys.ToList();
            List<string> name = new List<string>();
            foreach (var item in q)
            {
                if (item.Contains("BSSPackage_" + id + "_"))
                    name.Add(item);
            }
            List<ViewModel.PacklistViewModel> pl = new List<ViewModel.PacklistViewModel>();
            ViewModel.PacklistViewModel items;
            int cont = id.Count() + 12;
            foreach (var item in name)
            {
                items = new ViewModel.PacklistViewModel();
                items.cookieName = item.Substring(12 + id.Count());
                string qt = Request.Cookies[item].Value;
                items.Qty = qt;
                items.Fee = l.Where(p => p.Name == items.cookieName).First().Fee;
                items.Game = id;
                pl.Add(items);
            }
            ViewModel.PackageViewModel pvm = new ViewModel.PackageViewModel();
            pvm.Game = g;
            pvm.Packages = l;
            pvm.pl = pl;
            return View("Package", pvm);
        }
        [HttpPost]
        public ActionResult AddToCart(string gName, string pName)
        {
            string payam = "";
            if (Request.Cookies.AllKeys.Contains("BSSPackage_" + gName + "_" + pName))
            {
                //ویرایش
                payam = "این پکیج هم اکنون در لیست موجود است.شما می توانید از لیست خرید،تعداد را تغییر دهید.";
            }
            else
            {
                //افزودن کوکی جدید
                var cookie = new HttpCookie("BSSPackage_" + gName + "_" + pName, "1");
                cookie.Expires = DateTime.Now.AddMonths(1);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                payam = "پکیج به لیست اضافه شد";
            }
            return Json(new JsonData()
            {
                Success = true,
                Script = payam,
                Html = ""
            });
        }
        public ActionResult DeleteCart(string gName, string pName)
        {
            if (Request.Cookies.AllKeys.Contains("BSSPackage_" + gName + "_" + pName))
            {
                //update
                var cookie = new HttpCookie("BSSPackage_" + gName + "_" + pName);
                cookie.Expires = DateTime.Now;
                cookie.HttpOnly = true;
                Response.Cookies.Set(cookie);
                return Json(new JsonData()
                {
                    Success = true,
                    Script = "پکیج از لیست حذف شد",
                    Html = ""
                });
            }
            else
            {
                return Json(new JsonData()
                {
                    Success = false,
                    Script = "",
                    Html = ""
                });
            }

        }
        public string CartCount()
        {
            List<HttpCookie> lst = new List<HttpCookie>();
            for (int i = Request.Cookies.Count - 1; i >= 0; i--)
            {
                if (lst.Where(p => p.Name == Request.Cookies[i].Name).Any() == false)
                    lst.Add(Request.Cookies[i]);
            }
            int CartCount = lst.Where(p => p.Name.StartsWith("BSSPackage_")).Count();
            return CartCount.ToString();
        }
        public ActionResult priceCal(string gName, string pName, int Count, string price)
        {
            var cookie = new HttpCookie("BSSPackage_" + gName + "_" + pName, Count.ToString());
            cookie.Expires = DateTime.Now.AddMonths(1);
            cookie.HttpOnly = true;
            Response.Cookies.Set(cookie);
            int total = int.Parse(price) * Count;
            return Json(new JsonData
            {
                Success = true,
                Script = "#total-" + pName,
                Html = total.ToString() + " تومان"
            });

        }
        public ActionResult Checkout(string id)
        {
            List<HttpCookie> lst = new List<HttpCookie>();
            for (int i = Request.Cookies.Count - 1; i >= 0; i--)
            {
                if (lst.Where(p => p.Name == Request.Cookies[i].Name).Any() == false)
                    lst.Add(Request.Cookies[i]);
            }
            var MyCart = lst.Where(p => p.Name.StartsWith("BSSPackage_" + id)).ToList();
            List<Pack> packs = new List<Pack>();
            foreach (var item in MyCart)
            {
                packs.Add(Helper.Utilities.FPack(item.Name.Substring(12 + id.Count())));
            }
            List<ViewModel.ChecksViewModel> cvm = new List<ViewModel.ChecksViewModel>();
            ViewModel.ChecksViewModel pak;
            foreach (var item in packs)
            {
                pak = new ViewModel.ChecksViewModel();
                pak.pack = item;
                pak.qty = int.Parse(MyCart.Where(p => p.Name == "BSSPackage_" + id + "_" + item.Name).First().Value);
                pak.totalFee = (int.Parse(item.Fee) * pak.qty).ToString();
                cvm.Add(pak);
            }
            int tfee = 0;
            foreach (var item in cvm)
            {
                tfee += int.Parse(item.totalFee);
            }
            string pids = "";
            foreach (var item in packs)
            {
                pids += item.Id.ToString() + ",";
            }
            ViewModel.CheckoutPageViewModel cpvm = new ViewModel.CheckoutPageViewModel();
            cpvm.vm = cvm;
            cpvm.fee = tfee.ToString();
            cpvm.pa = pids;
            return View(cpvm);
        }
        [HttpPost]
        public ActionResult SendMessage(string Name, string Sirname, string Message)
        {
            if (Helper.Utilities.SaveMessage(Name, Sirname, Message))
            {
                return Json(new JsonData()
                {
                    Success = true,
                    Script = "با تشکر پیام شما ثبت شد",
                    Html = ""
                });
            }
            else
            {
                return Json(new JsonData()
                {
                    Success = false,
                    Script = "مشکل در سیستم.لطفا بعدا امتحان کنید",
                    Html = ""
                });
            }

        }

        [HttpPost]
        public ActionResult ShippingInfo(string totalfee,string packs, string Name, string Sirname, string Mobile, string Email)
        {
                if (Helper.Utilities.SaveOrder(Name, Sirname, Email, Mobile, packs, totalfee))
                {
                    return RedirectToAction("MainPage");
                }
                else
                { 
                
                }

            return View();
        }

    }
    public class JsonData
    {
        public string Html { get; set; }
        public bool Success { get; set; }
        public string Script { get; set; }
    }
}
