using Skillbox_Homework17_Entity.Model;
using Skillbox_Homework17_Entity.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Skillbox_Homework17_Entity.ViewModel
{
    public class DataManageVM :INotifyPropertyChanged
    {
        #region Поля и свойства
        private List<Client> allClients;

        /// <summary>
        /// все Клиенты
        /// </summary>
        public List<Client> AllClients 
        { 
            get { return allClients; }
            set
            {
                allClients = value;
                NotifyPropertyChanged(nameof(AllClients));
            }
        }

        private List<Purchase> allPurchases;

        /// <summary>
        /// все Клиенты
        /// </summary>
        public List<Purchase> AllPurchases
        {
            get { return allPurchases; }
            set
            {
                allPurchases = value;
                NotifyPropertyChanged(nameof(AllPurchases));
            }
        }
        #endregion

        #region Конструкторы
        public DataManageVM()
        {
            allClients = DataWorker.GetAllClients();
            allPurchases = DataWorker.GetAllPurchases();
        }
        #endregion

        
        #region Методы открытия окон
        private void OpenAddNewClientWindowMethod() 
        {
            AddNewClient newClientWindow = new AddNewClient();
            SetCenterPositionAndOpen(newClientWindow);
        }

        private void OpenAddNewPurchaseWindowMethod()
        {
            AddNewPurchase newPurchaseWindow = new AddNewPurchase();
            SetCenterPositionAndOpen(newPurchaseWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Команды открытия окон
        private RelayCommand openAddNewClientWind;
        public RelayCommand OpenAddNewClientWind
        {
            get
            {
                return openAddNewClientWind ?? new RelayCommand(obj =>
                {
                    OpenAddNewClientWindowMethod();
                });
            }
        }

        private RelayCommand openAddNewPurchaseWind;
        public RelayCommand OpenAddNewPurchaseWind
        {
            get
            {
                return openAddNewPurchaseWind ?? new RelayCommand(obj =>
                {
                    OpenAddNewPurchaseWindowMethod();
                }
                );
            }
        }

        #endregion

        #region Команды Добавления
        private RelayCommand openAddNewClient;
        public RelayCommand OpenAddNewClient
        {
            get
            {
                return openAddNewClient ?? new RelayCommand(obj =>
                {
                });
            }
        }

        private RelayCommand openAddNewPurchase;
        public RelayCommand OpenAddNewPurchase
        {
            get
            {
                return openAddNewPurchase ?? new RelayCommand(obj =>
                {
                });
            }
        }

        #endregion

    }
}
