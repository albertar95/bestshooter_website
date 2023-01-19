using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bestshooter.Models.DomainModel;
using System.Web.Security;

namespace Bestshooter.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Panel()
        {
            return View();
        }
        //games
        public ActionResult Games()
        {
            var list = Helper.AdminUtilities.Gamelist();
            return View(list);
        }
        public ActionResult NewGame()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewGame(Game game, HttpPostedFileBase Main, HttpPostedFileBase GamePic, HttpPostedFileBase Alter)
        {
            game.Pic1 = "Content/Template/images/packpic/" + Main.FileName;
            game.Pic2 = "Content/Template/images/cover/" + GamePic.FileName;
            string pathMain = Server.MapPath("~") + "Content\\Template\\images\\packpic\\" + Main.FileName;
            string pathGamepic = Server.MapPath("~") + "Content\\Template\\images\\cover\\" + GamePic.FileName;
            if (Alter != null)
            {
                game.Pic3 = "Content/Template/images/packpic/" + Alter.FileName;
                string pathAlter = Server.MapPath("~") + "Content\\Template\\images\\packpic\\" + Alter.FileName;
                Alter.SaveAs(pathAlter);
            }
            Main.SaveAs(pathMain);
            GamePic.SaveAs(pathGamepic);
            if (Helper.AdminUtilities.SaveGame(game))
            {
                TempData["backfromsavegame"] = "ok";
                return RedirectToAction("Games");
            }
            else
            {
                ViewBag.errmessage = "مشکل در دیتابیس";
            }

            return View();
        }
        [HttpGet]
        public ActionResult EditGame(int id)
        {
            Game g = Helper.AdminUtilities.FGame(id);
            return View(g);
        }
        [HttpPost]
        public ActionResult UpdateGame(Game g, HttpPostedFileBase Main, HttpPostedFileBase GamePic, HttpPostedFileBase Alter)
        {
            if (Main == null && GamePic == null && Alter == null)
            {
                if (Helper.AdminUtilities.EditGame(g))
                {
                    TempData["backfromedit"] = "ok";
                    return RedirectToAction("Games");
                }
                else
                {
                    TempData["backfromedit2"] = "nok";
                    return RedirectToAction("EditGame");
                }
            }
            else
            {
                if (Main != null)
                {
                    string oldpath = Server.MapPath("~") + g.Pic1;
                    g.Pic1 = "Content/Template/images/packpic/" + Main.FileName;
                    string pathMain = Server.MapPath("~") + "Content\\Template\\images\\packpic\\" + Main.FileName;
                    Main.SaveAs(pathMain);
                    System.IO.File.Delete(oldpath);
                }
                if (GamePic != null)
                {
                    string oldpath1 = Server.MapPath("~") + g.Pic2;
                    g.Pic2 = "Content/Template/images/cover/" + GamePic.FileName;
                    string pathGamepic = Server.MapPath("~") + "Content\\Template\\images\\cover\\" + GamePic.FileName;
                    GamePic.SaveAs(pathGamepic);
                    System.IO.File.Delete(oldpath1);
                }
                if (Alter != null)
                {
                    string oldpath2 = "";
                    if (g.Pic3 != null)
                    {
                        oldpath2 = Server.MapPath("~") + g.Pic3;
                        g.Pic3 = "Content/Template/images/packpic/" + Alter.FileName;
                        string pathAlter = Server.MapPath("~") + "Content\\Template\\images\\packpic\\" + Alter.FileName;
                        Alter.SaveAs(pathAlter);
                        System.IO.File.Delete(oldpath2);
                    }
                    else
                    {
                        g.Pic3 = "Content/Template/images/packpic/" + Alter.FileName;
                        string pathAlter = Server.MapPath("~") + "Content\\Template\\images\\packpic\\" + Alter.FileName;
                        Alter.SaveAs(pathAlter);
                    }

                }
                if (Helper.AdminUtilities.EditGame(g))
                {
                    TempData["backfromedit"] = "ok";
                    return RedirectToAction("Games");
                }
                else
                {
                    TempData["backfromedit2"] = "nok";
                    return RedirectToAction("EditGame");
                }
            }
        }
        public ActionResult DeleteGame(int id)
        {
            Game g = Helper.AdminUtilities.FGame(id);
            string pathMain = Server.MapPath("~") + g.Pic1;
            string pathGamePic = Server.MapPath("~") + g.Pic2;
            string pathAlter = "";
            if (g.Pic3 != null)
            {
                pathAlter = Server.MapPath("~") + g.Pic3;
                System.IO.File.Delete(pathAlter);
            }
            if (Helper.AdminUtilities.DelGame(id) == 0)
            {
                if (g.Pic3 != null)
                {
                    System.IO.File.Delete(pathAlter);
                }
                System.IO.File.Delete(pathMain);
                System.IO.File.Delete(pathGamePic);
                TempData["backfromdelete"] = "ok";
            }
            else if (Helper.AdminUtilities.DelGame(id) == 1)
            {
                TempData["backfromdelete2"] = "ok";
            }
            else if (Helper.AdminUtilities.DelGame(id) == 2)
            {
                TempData["backfromdeletebutpack"] = "ok";
            }
            return RedirectToAction("Games");
        }
        public ActionResult InfoGame(int id)
        {
            Game g = Helper.AdminUtilities.FGame(id);
            return View(g);
        }
        //end of game
        //packs
        public ActionResult Packs()
        {
            var list = Helper.AdminUtilities.Packlist();
            return View(list);
        }
        public ActionResult NewPack()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPack(Pack pack, HttpPostedFileBase Main, HttpPostedFileBase Guns)
        {
            string mpath = Server.MapPath("~") + "Content\\Template\\images\\packpic\\" + Main.FileName;
            string gpath = Server.MapPath("~") + "Content\\Template\\images\\packsinfo\\" + Guns.FileName;
            Main.SaveAs(mpath);
            Guns.SaveAs(gpath);
            pack.Picture = "Content/Template/images/packpic/" + Main.FileName;
            pack.DescPicture = "Content/Template/images/packsinfo/" + Guns.FileName;
            if (Helper.AdminUtilities.SavePack(pack))
            {
                TempData["backformsavepack"] = "ok";
                return RedirectToAction("Packs");
            }
            else
            {
                ViewBag.perr = "nok";
            }
            return View();
        }
        public ActionResult EditPack(int id)
        {
            Pack p = Helper.AdminUtilities.FPack(id);
            return View(p);
        }
        public ActionResult UpdatePack(Pack p, HttpPostedFileBase Main, HttpPostedFileBase Guns)
        {
            if (Main == null && Guns == null)
            {
                if (Helper.AdminUtilities.EditPack(p))
                {
                    TempData["backfromeditpack"] = "ok";
                    return RedirectToAction("Packs");
                }
                else
                {
                    TempData["backfromeditpack2"] = "nok";
                    return RedirectToAction("EditPack");
                }
            }
            else
            {
                if (Main != null)
                {
                    string oldpath = Server.MapPath("~") + p.Picture;
                    p.Picture = "Content/Template/images/packpic/" + Main.FileName;
                    string pathMain = Server.MapPath("~") + "Content\\Template\\images\\packpic\\" + Main.FileName;
                    Main.SaveAs(pathMain);
                    System.IO.File.Delete(oldpath);
                }
                if (Guns != null)
                {
                    string oldpath1 = Server.MapPath("~") + p.DescPicture;
                    p.DescPicture = "Content/Template/images/cover/" + Guns.FileName;
                    string pathGuns = Server.MapPath("~") + "Content\\Template\\images\\packsinfo\\" + Guns.FileName;
                    Guns.SaveAs(pathGuns);
                    System.IO.File.Delete(oldpath1);
                }
                if (Helper.AdminUtilities.EditPack(p))
                {
                    TempData["backfromeditpack"] = "ok";
                    return RedirectToAction("Packs");
                }
                else
                {
                    TempData["backfromeditpack2"] = "nok";
                    return RedirectToAction("EditPack");
                }
            }
        }
        public ActionResult DeletePack(int id)
        {
            Pack p = Helper.AdminUtilities.FPack(id);
            string opath1 = Server.MapPath("~") + p.Picture;
            string opath2 = Server.MapPath("~") + p.DescPicture;
            System.IO.File.Delete(opath1);
            System.IO.File.Delete(opath2);
            if (Helper.AdminUtilities.DelPack(id))
            {
                TempData["backfromdeletepack"] = "ok";
            }
            else
            {
                TempData["backfromdeletepack2"] = "nok";
            }
            return RedirectToAction("Packs");
        }
        public ActionResult InfoPack(int id)
        {
            Pack p = Helper.AdminUtilities.FPack(id);
            return View(p);
        }
        //end of pack
        public ActionResult Factors()
        {
            var list = Helper.AdminUtilities.Factorlist();
            return View(list);
        }
        public ActionResult InfoFactor(int id)
        {
            ViewModel.FactorViewModel fvm = new ViewModel.FactorViewModel();
            fvm.factor = Helper.AdminUtilities.FFactor(id);
            fvm.order = Helper.AdminUtilities.FOrder(fvm.factor.OrderId);
            List<Pack> plist = new List<Pack>();
            foreach (var item in fvm.order.PackIds.Substring(0, fvm.order.PackIds.Count() - 1).Split(','))
            {
                plist.Add(Helper.AdminUtilities.FPack(int.Parse(item)));
            }
            fvm.packs = plist;
            return View(fvm);
        }
        public ActionResult Unsuccessful()
        {
            var list = Helper.AdminUtilities.UFactorlist();
            return View(list);
        }
        public ActionResult InfoUFactor(int id)
        {
            ViewModel.FactorViewModel fvm = new ViewModel.FactorViewModel();
            fvm.factor = Helper.AdminUtilities.FFactor(id);
            fvm.order = Helper.AdminUtilities.FOrder(fvm.factor.OrderId);
            List<Pack> plist = new List<Pack>();
            foreach (var item in fvm.order.PackIds.Substring(0, fvm.order.PackIds.Count() - 1).Split(','))
            {
                plist.Add(Helper.AdminUtilities.FPack(int.Parse(item)));
            }
            fvm.packs = plist;
            return View(fvm);
        }
        public ActionResult Messages()
        {
            var list = Helper.AdminUtilities.Messagelist();
            return View(list);
        }
        public ActionResult MessageInfo(int id)
        {
            Message m = Helper.AdminUtilities.FMessage(id);
            Helper.AdminUtilities.UpdateM(id);
            return View(m);
        }
        public ActionResult Setting()
        {
            var set = Helper.AdminUtilities.settings();
            return View(set);
        }
        public ActionResult UpdateSetting(Setting setting)
        {
            if (Helper.AdminUtilities.UpdateSettings(setting))
            {
                TempData["backfromsetts"] = "ok";
                return RedirectToAction("Setting", "Admin");
            }
            else
            {
                TempData["backfromsetts"] = "nok";
                return RedirectToAction("Setting", "Admin");
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authen(string usrname, string pass, bool remember = false)
        {
            Admin adm = Helper.AdminUtilities.Authenticate(usrname, pass.GetHashCode().ToString());
            if (adm != null)
            {
                FormsAuthentication.SetAuthCookie(usrname, remember);
                return RedirectToAction("Panel");
            }
            else
            {
                TempData["err"] = "ok";
                return View("Login");
            }
        }
        public ActionResult MyProfile()
        {
            Admin a = Helper.AdminUtilities.profile(User.Identity.Name);
            List<Admin> list = new List<Admin>();
            foreach (var item in Helper.AdminUtilities.Alist())
            {
                if (item.Id != a.Id)
                    list.Add(item);
            }
            ViewModel.AdminViewModel avm = new ViewModel.AdminViewModel();
            avm.admin = a;
            avm.admins = list;
            return View(avm);
        }
        public ActionResult NewAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAdmin(Admin admin)
        {
            if (Helper.AdminUtilities.SaveAdmin(admin))
            {
                TempData["backfromadd"] = "ok";
                return RedirectToAction("MyProfile", "Admin");
            }
            else
            {
                TempData["backfromadd"] = "nok";
                return RedirectToAction("MyProfile", "Admin");
            }
        }
        public ActionResult DeleteAdmin(int id)
        {
            if (Helper.AdminUtilities.DeleteAdmin(id))
            {
                TempData["backfromdel"] = "ok";
                return RedirectToAction("MyProfile", "Admin");
            }
            else
            {
                TempData["backfromdel"] = "nok";
                return RedirectToAction("MyProfile", "Admin");
            }
        }
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(string current, string pass, string Rpass)
        {
            if (Rpass != pass)
            {
                ViewBag.notmatch = "ok";
                return View();
            }
            Admin a = Helper.AdminUtilities.Authenticate(User.Identity.Name, current.GetHashCode().ToString());
            if (a != null)
            {
                a.Password = pass.GetHashCode().ToString();
                if (Helper.AdminUtilities.UpdateAdmin(a))
                {
                    TempData["backfrompass"] = "ok";
                    return RedirectToAction("MyProfile", "Admin");
                }
                else
                {
                    TempData["backfrompass"] = "nok";
                    return RedirectToAction("MyProfile", "Admin");
                }
            }
            else
            {
                ViewBag.err1 = "ok";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("MainPage", "Home");
        }
    }
    public class JsonDataA
    {
        public string Html { get; set; }
        public bool Success { get; set; }
        public string Script { get; set; }
    }

}
