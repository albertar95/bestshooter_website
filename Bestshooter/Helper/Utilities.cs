using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bestshooter.Models.DomainModel;
namespace Bestshooter.Helper
{
    public class Utilities
    {
       static Bestshooter1Entities db = null;
        public static List<Pack> FetchBestOrder()
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Packs.OrderByDescending(p => p.Counter).Take(3).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<Pack> AlternateOrder()
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
        public static List<Game> FetchGames()
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Games.OrderBy(p => p.Id).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Game FetchGame(string name) 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Games.Where(p => p.Name == name).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<Pack> GatherPacks(int id) 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Packs.Where(p => p.GameId == id).OrderBy(p => p.Id).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Pack FPack(string Name) 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Packs.Where(p => p.Name == Name).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool SaveMessage(string name,string sirname,string message) 
        {
            try
            {
                db = new Bestshooter1Entities();
                int mid = 0;
                if(db.Messages.Any())
                {
                mid = db.Messages.OrderByDescending(p => p.Id).First().Id + 1;
                }
                Message m = new Message() { Id = mid, Message1 = message, Name = name, Sirname = sirname ,Date = DateTime.Now};
                db.Messages.Add(m);
                if (db.SaveChanges() == 1)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Setting FSetting() 
        {
            try
            {
                db = new Bestshooter1Entities();
                return db.Settings.OrderByDescending(p => p.Id).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool SaveOrder(string name,string sirname,string email,string mobile,string packs,string totalfee) 
        {
            db = new Bestshooter1Entities();
            int oid = 0;
            if (db.Orders.Any())
                oid = db.Orders.OrderByDescending(p => p.Id).First().Id + 1;
            Order ord = new Order() { Id = oid, Date = DateTime.Now, Email = email, Mobile = mobile, Name = name, PackIds = packs, Sirname = sirname, TotalFee = totalfee };
            db.Orders.Add(ord);
            if (db.SaveChanges() == 1)
                return true;
            else
                return false;
        }
    }
}