using Skillbox_Homework17_Entity.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
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
        /// <returns>Результат операции</returns>
        public static string AddClient(string Email,
            string LastName = "", string FirstName = "", string MiddleName = "", string Phone = "")
        {
            string result = "";
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
                else
                    result = "Клиент с данным Email уже существует";
                return result;
            }
        }


        /// <summary>
        /// Добавление покупки
        /// </summary>
        /// <param name="Email">Email клиента</param>
        /// <param name="ProductCode">Код продукта</param>
        /// <param name="ProductName">Наименование продукта</param>
        /// <returns>Результат операции</returns>
        public static string AddPurchase(string Email, string ProductCode, string ProductName)
        {
            string result = "";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool CheckIsExist = db.Clients.Any(e => e.email == Email);
                if (CheckIsExist)
                {
                    Purchase newPurchase = new Purchase();
                    newPurchase.email = Email;
                    newPurchase.Client = db.Clients.Where(e => e.email == Email).First<Client>();
                    newPurchase.productCode = ProductCode;
                    newPurchase.productName = ProductName;
                    db.Purchases.Add(newPurchase);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                else
                    result = "Клиента с данным Email не существует";
                return result;
            }
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns>Результат операции</returns>
        public static string DeleteClient(Client client)
        {
            string result = "";
            using (ApplicationContext db = new ApplicationContext())
            {
                var recordToDelete = db.Clients.FirstOrDefault(e => e.email == client.email);
                if (recordToDelete != null)
                {
                    db.Clients.Remove(recordToDelete);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                else
                    result = "Такого клиента не существует!";
            }
            return result;
        }

        /// <summary>
        /// Удаление покупки
        /// </summary>
        /// <param name="purchase">Покупка</param>
        /// <returns>Результат операции</returns>
        public static string DeletePurchase(Purchase purchase)
        {
            string result = "";
            using (ApplicationContext db = new ApplicationContext())
            {
                var recordToDelete = db.Purchases.FirstOrDefault(e => e.id == purchase.id);
                if (recordToDelete != null)
                {

                    db.Purchases.Remove(recordToDelete);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                else
                    result = "Такой покупки не существует!";
            }
            return result;
        }

        /// <summary>
        /// Редактирование Клиента
        /// </summary>
        /// <param name="EditedClient">Клиент</param>
        /// <returns>Результат операции</returns>
        public static string EditClient(Client EditedClient)
        {
            string result="";
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
                else 
                    result = "Такого клиента не существует";

            }
            return result;
        }

        /// <summary>
        /// Редактирование покупки
        /// </summary>
        /// <param name="EditedPurchase">Покупка</param>
        /// <returns>Результат операции</returns>
        public static string EditPurchase(Purchase EditedPurchase)
        {
            string result = "";
            using (ApplicationContext db = new ApplicationContext())
            {
                Purchase purchase = db.Purchases.FirstOrDefault(e => e.id == EditedPurchase.id);
                if (purchase != null)
                {
                    purchase.productCode = EditedPurchase.productCode;
                    purchase.productName = EditedPurchase.productName;
                    db.SaveChanges();
                    result = "Сделано!";
                }
                else
                    result = "Покупки с таким id не существует";
            }
            return result;
        }

        /// <summary>
        /// Получает из БД всех клиентов
        /// </summary>
        /// <returns>Клиенты</returns>
        public static List<Client> GetAllClients()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Clients.ToList();
                return result;
            }
        }

        /// <summary>
        /// Получает из БД все покупки
        /// </summary>
        /// <returns>Покупки</returns>
        public static List<Purchase> GetAllPurchases()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Purchases.ToList();
                return result;
            }
        }


        /// <summary>
        /// Получает из БД все покупки по Email пользователя
        /// </summary>
        /// <returns>Покупки</returns>
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
