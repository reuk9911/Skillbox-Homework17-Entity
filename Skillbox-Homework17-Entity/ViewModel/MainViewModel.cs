using Skillbox_Homework17_Entity.Model;
using Skillbox_Homework17_Entity.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Skillbox_Homework17_Entity.Utility;
using System.Collections.Specialized;

namespace Skillbox_Homework17_Entity.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        #region Поля и свойства
        //private ObservableCollection<Client> allClients;
        //private ObservableCollection<Purchase> allPurchases;

        /// <summary>
        /// все Клиенты
        /// </summary>
        //public ObservableCollection<Client> AllClients
        //{
        //    get { return allClients; }
        //    set
        //    {
        //        allClients = value;
        //        base.RaisePropertyChangedEvent(nameof(AllClients));
        //    }

        //}


        ///// <summary>
        ///// все Клиенты
        ///// </summary>
        //public ObservableCollection<Purchase> AllPurchases
        //{
        //    get { return allPurchases; }
        //    set
        //    {
        //        allPurchases = value;
        //        base.RaisePropertyChangedEvent(nameof(AllPurchases));
        //    }
        //}
        public ObservableCollection<Client> AllClients { get; set; } 
        public ObservableCollection<Purchase> AllPurchases { get; set; }
        #endregion

        #region Конструкторы
        public MainViewModel()
        {
            AllClients = new ObservableCollection<Client>(DataWorker.GetAllClients());
            AllPurchases = new ObservableCollection<Purchase>(DataWorker.GetAllPurchases());

            // сделать для Purchases тоже самое
        }
        #endregion

        #region Обработчики событий
        private void AllClients_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            string resultStr;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    if (e.NewItems?[0] is Client newClient)
                        resultStr = DataWorker.EditClient(newClient);
                    break;
                
                case NotifyCollectionChangedAction.Remove: // если удаление
                    if (e.OldItems?[0] is Client oldClient)
                        resultStr = DataWorker.DeleteClient(oldClient);
                    break;
                
                default: 
                    break;
            }
        }

        private void AllPurchases_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion




        #region Команды редактирования

        private RelayCommand editClient;
        public RelayCommand EditClient
        {
            get
            {
                return editClient ?? new RelayCommand(obj =>
                {
                    Client c = obj as Client;
                    string resultStr = DataWorker.EditClient(c);
                    AllClients = new ObservableCollection<Client>(DataWorker.GetAllClients());
                });
            }
        }

        #endregion


    }
}


