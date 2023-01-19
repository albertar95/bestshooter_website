using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bestshooter.Models.DomainModel;
namespace Bestshooter.Helper
{
    public class AdminUtilities
    {
        static Bestshooter.Models.DomainModel.Bestshooter1Entities db = null;
        public static List<Game> Gamelist() 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Games.OrderByDescending(p => p.Id).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool SaveGame(Game game) 
        {
            try
            {
                db = new Bestshooter1Entities();
                int gid = 0;
                if(db.Games.Any())
                gid = db.Games.OrderByDescending(p => p.Id).ToList().First().Id + 1;
                game.Id = gid;
                db.Games.Add(game);
                if (db.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Game FGame(int id) 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Games.Where(p => p.Id == id).ToList().First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static int DelGame(int id)
        {
            try
            {
                db = new Bestshooter1Entities();
                Game g = db.Games.Find(id);
                if (g.Packs.Count == 0)
                {
                    db.Entry(g).State = System.Data.EntityState.Deleted;
                    if (db.SaveChanges() == 1)
                        return 0;
                    else
                        return 1;
                }
                else
                    return 2;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public static bool EditGame(Game game)
        {
            try
            {
                db = new Bestshooter1Entities();
                db.Games.Attach(game);
                db.Entry(game).State = System.Data.EntityState.Modified;
                if (db.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<Pack> Packlist()
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Packs.OrderByDescending(p => p.Id).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool SavePack(Pack pack)
        {
            try
            {
                db = new Bestshooter1Entities();
                int pid = 0;
                if(db.Packs.Any())
                pid = db.Packs.OrderByDescending(p => p.Id).ToList().First().Id + 1;
                pack.Id = pid;
                pack.Counter = 0;
                db.Packs.Add(pack);
                if (db.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Pack FPack(int id)
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Packs.Where(p => p.Id == id).ToList().First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool DelPack(int id)
        {
            try
            {
                db = new Bestshooter1Entities();
                Pack p = db.Packs.Find(id);
                db.Entry(p).State = System.Data.EntityState.Deleted;
                if (db.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool EditPack(Pack pack)
        {
            try
            {
                db = new Bestshooter1Entities();
                db.Packs.Attach(pack);
                db.Entry(pack).State = System.Data.EntityState.Modified;
                if (db.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Order FOrder(int id) 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Orders.Where(p => p.Id == id).ToList().First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Factor FFactor(int id)
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Factors.Where(p => p.Id == id).ToList().First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<Factor> Factorlist()
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Factors.Where(p => p.PurchaseNumber == 1).OrderBy(p => p.Date).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<Factor> UFactorlist() 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Factors.Where(p => p.PurchaseNumber == 2).OrderBy(p => p.Date).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<Message> Messagelist() 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Messages.OrderByDescending(p => p.Id).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Message FMessage(int id)
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Messages.Where(p => p.Id == id).ToList().First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static void UpdateM(int id) 
        {
            try
            {
                db = new Bestshooter1Entities();
                Message m = db.Messages.Where(p => p.Id == id).First();
                m.Read = 1;
                db.Messages.Attach(m);
                db.Entry(m).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
            
            }
        }
        public static Setting settings() 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Settings.OrderBy(p => p.Id).ToList().First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool UpdateSettings(Setting setts) 
        {
            try
            {
                db = new Bestshooter1Entities();
                db.Settings.Attach(setts);
                db.Entry(setts).State = System.Data.EntityState.Modified;
                if (db.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Admin Authenticate(string user,string pass) 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Admins.Where(p => p.Username == user && p.Password == pass).ToList().First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Admin profile(string name) 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Admins.Where(p => p.Username == name).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<Admin> Alist()
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Admins.Where(p => p.AdminRole == 2).OrderByDescending(p => p.Id).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool SaveAdmin(Admin adm)
        {
            try
            {
                db = new Bestshooter1Entities();
                int aid = 0;
                if(db.Admins.Any())
                aid = db.Admins.OrderByDescending(p => p.Id).First().Id + 1;
                adm.Id = aid;
                adm.Password = adm.Password.GetHashCode().ToString();
                db.Admins.Add(adm);
                if (db.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DeleteAdmin(int id)
        {
            try
            {
                db = new Bestshooter1Entities();
                var q = db.Admins.Where(p => p.Id == id).First();
                db.Entry(q).State = System.Data.EntityState.Deleted;
                if (db.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool UpdateAdmin(Admin admin)
        {
            try
            {
                db = new Bestshooter1Entities();
                db.Admins.Attach(admin);
                db.Entry(admin).State = System.Data.EntityState.Modified;
                if (db.SaveChanges() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}