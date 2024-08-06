using Skillbox_Homework17_Entity.Model;
using Skillbox_Homework17_Entity.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Skillbox_Homework17_Entity.Utility;
using System.Windows.Data;
using System.Security.Policy;
using System.Windows.Navigation;

namespace Skillbox_Homework17_Entity.ViewModel
{
    public class DataManageVM : INotifyPropertyChanged
    {
        #region Поля и свойства

        private Client selectedClient;
        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                if (selectedClient != value)
                {
                    selectedClient = value;
                    ClientPurchases = new ObservableCollection<Purchase>(DataWorker.GetPurchasesByEmail(SelectedClient?.email));
                    RaisePropertyChangedEvent("SelectedClient");
                }
            }
        }

        private ObservableCollection<Client> allClients;

        /// <summary>
        /// все Клиенты
        /// </summary>
        public ObservableCollection<Client> AllClients
        {
            get { return allClients; }
            set
            {
                if (allClients != value)
                {
                    allClients = value;
                    RaisePropertyChangedEvent("AllClients");
                }
            }
        }


        /// <summary>
        /// все Клиенты
        /// </summary>
        public ObservableCollection<Purchase> ClientPurchases { get; set; }

        public string ClientEmail { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientMiddleName { get; set; }
        public string ClientPhone { get; set; }


        public string PurchaseEmail { get; set; }
        public string PurchaseProductCode { get; set; }
        public string PurchaseProductName { get; set; }

        #endregion

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Конструкторы
        public DataManageVM()
        {
            AllClients = new ObservableCollection<Client>(DataWorker.GetAllClients());
            ClientPurchases = new ObservableCollection<Purchase>(DataWorker.GetPurchasesByEmail(SelectedClient?.email));
            
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
            PurchaseEmail = SelectedClient.email;
            AddNewPurchase newPurchaseWindow = new AddNewPurchase();
            SetCenterPositionAndOpen(newPurchaseWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
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
                    AllClients = new ObservableCollection<Client>(DataWorker.GetAllClients());
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
                    ClientPurchases = new ObservableCollection<Purchase>(DataWorker.GetPurchasesByEmail(SelectedClient?.email));

                },
            (obj) => obj!=null);
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

                        resultStr = DataWorker.AddClientByForm(
                            ClientEmail, ClientLastName, ClientFirstName, ClientMiddleName, ClientPhone);
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
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (PurchaseProductCode != "" && PurchaseProductName != "")
                    {

                        //resultStr = DataWorker.AddPurchaseByForm(PurchaseEmail, 
                        //    PurchaseProductCode, PurchaseProductName);
                        wnd.Close();
                    }
                });
            }
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
                    allClients = new ObservableCollection<Client>(DataWorker.GetAllClients());
                });
            }
        }

        #endregion

        #region Команды удаления

        private RelayCommand deleteClient;
        public RelayCommand DeleteClient
        {
            get
            {
                return deleteClient ?? new RelayCommand(obj =>
                {
                    string resultStr = "";
                    if (SelectedClient != null)
                    {
                        resultStr = DataWorker.DeleteClient(SelectedClient);
                        AllClients = new ObservableCollection<Client>(DataWorker.GetAllClients());
                    }
                });
            }
        }

        #endregion

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }



    }
}
