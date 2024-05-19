using Skillbox_Homework17_Entity.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_Homework17_Entity.Model
{
    public static class DataWorker
    {
        /// <summary>
        /// Добавление Клиента
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="MiddleName">Отчество</param>
        /// <param name="Phone">Номер телефона</param>
        /// <returns></returns>
        public static string AddClientByForm(string Email,
            string LastName = "", string FirstName = "", string MiddleName = "", string Phone = "")
        {
            string result = "Клиент с данным Email уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool CheckIsExist = db.Clients.Any(e => e.email == Email);
                if (!CheckIsExist)
                {
                    Client newClient = new Client()
                    {
                        email = Email,
                        firstName = FirstName,
                        lastName = LastName,
                        middleName = MiddleName,
                        phone = Phone
                    };
                    db.Clients.Add(newClient);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        //public static string AddClient(Client NewClient)
        //{
        //    string result = "Клиент с данным Email уже существует";
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        bool CheckIsExist = db.Clients.Any(e => e.email == NewClient.email);
        //        if (!CheckIsExist)
        //        {
                    
        //            db.Clients.Add(NewClient);
        //            db.SaveChanges();
        //            result = "Сделано!";
        //        }
        //        return result;
        //    }
        //}


        /// <summary>
        /// Добавление покупки
        /// </summary>
        /// <param name="Email">Email клиента</param>
        /// <param name="ProductCode">Код продукта</param>
        /// <param name="ProductName">Наименование продукта</param>
        /// <returns></returns>
        public static string AddPurchaseByForm(string Email, string ProductCode = "", string ProductName = "")
        {
            string result = "Клиента с данным Email не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool CheckIsExist = db.Clients.Any(e => e.email == Email);
                if (!CheckIsExist)
                {
                    Purchase newPurchase = new Purchase()
                    {
                        email = Email,
                        productCode = ProductCode,
                        productName = ProductName
                    };
                    db.Purchases.Add(newPurchase);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        public static string DeleteClient(Client client)
        {
            string result = "Такого клиента не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Clients.Remove(client);
                db.SaveChanges();
                result = "Сделано!";
            }
            return result;
        }

        public static string DeletePurchase(Purchase purchase)
        {
            string result = "Такой покупки не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Purchases.Remove(purchase);
                db.SaveChanges();
                result = "Сделано!";
            }
            return result;
        }

        public static string EditClient(Client EditedClient)
        {
            string result = "Такого клиента не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Client client = db.Clients.FirstOrDefault(e => e.email == EditedClient.email);
                if (client != null)
                {
                    client.firstName = EditedClient.firstName;
                    client.lastName = EditedClient.lastName;
                    client.middleName = EditedClient.middleName;
                    client.phone = EditedClient.phone;
                    db.SaveChanges();
                    result = "Сделано!";
                }
            }
            return result;
        }

        public static string EditPurchase(Client OldPurchase, string ProductCode, string ProductName)
        {
            string result = "Такого покупки не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Purchase purchase = db.Purchases.FirstOrDefault(e => e.email == OldPurchase.email);
                if (purchase != null)
                {
                    purchase.productCode = ProductCode;
                    purchase.productName = ProductName;
                    db.SaveChanges();
                    result = "Сделано!";
                }
            }
            return result;
        }

        public static List<Client> GetAllClients()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Clients.ToList();
                return result;
            }
        }

        public static List<Purchase> GetAllPurchases()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Purchases.ToList();
                return result;
            }
        }

        public static List<Purchase> GetPurchasesByEmail(string Email)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Purchases.Where(e => e.email == Email).ToList();
                return result;
            }
        }

    }
}
