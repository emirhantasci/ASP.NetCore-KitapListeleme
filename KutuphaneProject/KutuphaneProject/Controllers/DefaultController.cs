using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KutuphaneProject.Models;
using System.Data.SQLite;
using System.Data;

namespace KutuphaneProject.Controllers
{
    
    public class DefaultController : Controller
    {
        public string SQLEmir()
        {
            string tumKitap = "";
            SQLiteConnection emirhan = new SQLiteConnection("Data Source=C:\\Users\\emirh\\Desktop\\emirhan\\yazılım\\görsel\\görsel örnekler\\Python-KutuphaneProjesi\\KutuphaneProjesi\\veritabani.db;Version=3;");
            emirhan.Open();

            SQLiteCommand sqlite_cmd;
            sqlite_cmd = emirhan.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM kitap";
            SQLiteDataReader dr = sqlite_cmd.ExecuteReader();

            while (dr.Read())
            {
                //Console.WriteLine(dr["Adi"].ToString());
                tumKitap = tumKitap + dr["Tur"] + "," + dr["Adi"] + "," + dr["YazarAdi"] + ";";
            }
            emirhan.Close();
            return tumKitap;
        }


        public IActionResult Index()
        {
            string tumKitaplar = SQLEmir();
            string[] yeniKitap = tumKitaplar.Split(';');
            yeniKitap[yeniKitap.Length - 1] = null;
            

            var ktp = new List<Models.Kitaplar>();

            foreach (string item in yeniKitap)
            {
                if (item != null)
                {
                    string[] yeniItem = item.Split(',');
                    
                    ktp.Add(new Kitaplar { Tur = yeniItem[0].ToString(), KitapAd = yeniItem[1].ToString(), Yazar = yeniItem[2].ToString() });
                }
                else
                {
                    continue;
                }  
            }
            
            
            return View(ktp);
        }
    }
}
