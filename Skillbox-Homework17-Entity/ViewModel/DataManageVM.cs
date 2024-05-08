using Skillbox_Homework17_Entity.Model;
using Skillbox_Homework17_Entity.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public string ClientEmail { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientMiddleName { get; set; }
        public string ClientPhone { get; set; }

        public string PurchaseClientEmail { get; set; }
        public string PurchaseProductCode { get; set; }
        public string PurchaseProductName { get; set; }
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

        private RelayCommand addNewClient;
        public RelayCommand AddNewClient
        {
            get
            {
                return addNewClient ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (ClientEmail == null || (!ClientEmail.Contains("@")))
                    {
                        SetRedBlockControll(wnd, "EmailBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.AddClient(
                            ClientEmail,ClientLastName, ClientFirstName, ClientMiddleName, ClientPhone);
                        //MessageBox.Show(resultStr);
                        wnd.Close();
                    }
                });
            }
        }



        private RelayCommand addNewPurchase;
        public RelayCommand AddNewPurchase
        {
            get
            {
                return addNewPurchase ?? new RelayCommand(obj =>
                {
                });
            }
        }

        #endregion
        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        //private void ShowMessageToUser(string message)
        //{
        //    MessageView messageView = new MessageView(message);
        //    SetCenterPositionAndOpen(messageView);
        //}

    }
}
