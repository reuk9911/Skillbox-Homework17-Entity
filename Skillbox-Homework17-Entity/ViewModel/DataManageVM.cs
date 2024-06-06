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

namespace Skillbox_Homework17_Entity.ViewModel
{
    public class DataManageVM
    {
        #region Поля и свойства
        private ObservableCollection<Client> allClients;

        /// <summary>
        /// все Клиенты
        /// </summary>
        public ObservableCollection<Client> AllClients { get; set; }

        private ObservableCollection<Purchase> allPurchases;

        /// <summary>
        /// все Клиенты
        /// </summary>
        public ObservableCollection<Purchase> AllPurchases { get; set; }

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
            AllClients = new ObservableCollection<Client>(DataWorker.GetAllClients());
            AllPurchases = new ObservableCollection<Purchase>(DataWorker.GetAllPurchases());

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

                        resultStr = DataWorker.AddClientByForm(
                            ClientEmail, ClientLastName, ClientFirstName, ClientMiddleName, ClientPhone);
                        //MessageBox.Show(resultStr);
                        
                        Client newClient = new Client()
                        {
                            email = ClientEmail,
                            firstName = ClientFirstName,
                            lastName = ClientLastName,
                            middleName = ClientMiddleName,
                            phone = ClientPhone
                        };
                        wnd.Close();
                        AllClients.Add(newClient);
                    }
                });
            }
        }

        //private RelayCommand addNewClient;
        //public RelayCommand AddNewClient
        //{
        //    get
        //    {
        //        return addNewClient ?? new RelayCommand(obj =>
        //        {
        //            string resultStr = "";
        //            Client newClient = obj as Client;
        //            resultStr = DataWorker.AddClient(newClient);
        //            allClients = new ObservableCollection<Client>(DataWorker.GetAllClients());

        //        });
        //    }
        //}



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

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }



    }
}
