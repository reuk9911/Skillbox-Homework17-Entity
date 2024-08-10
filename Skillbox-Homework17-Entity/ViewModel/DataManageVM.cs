﻿using Skillbox_Homework17_Entity.Model;
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

        /// <summary>
        /// Выделенный клиент
        /// </summary>
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


        private ObservableCollection<Purchase> clientPurchases;
        /// <summary>
        /// Покупки клиента
        /// </summary>
        public ObservableCollection<Purchase> ClientPurchases
        { 
            get{ return clientPurchases; }
            set
            {
                if (clientPurchases != value)
                {
                    clientPurchases = value;
                    RaisePropertyChangedEvent("ClientPurchases");
                }
            }
        }

        /// <summary>
        /// Email нового клиента
        /// </summary>
        public string ClientEmail { get; set; }

        /// <summary>
        /// Имя нового клиента
        /// </summary>
        public string ClientFirstName { get; set; }

        /// <summary>
        /// Фамилия нового клиента
        /// </summary>
        public string ClientLastName { get; set; }

        /// <summary>
        /// Отчество нового клиента
        /// </summary>
        public string ClientMiddleName { get; set; }

        /// <summary>
        /// Телефон нового клиента
        /// </summary>
        public string ClientPhone { get; set; }

        /// <summary>
        /// Email пользователя, который совершил покупку
        /// </summary>
        public string PurchaseEmail { get; set; }

        /// <summary>
        /// Код покупки
        /// </summary>
        public string PurchaseProductCode { get; set; }

        /// <summary>
        /// Наименование покупки
        /// </summary>
        public string PurchaseProductName { get; set; }

        private string Message;

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


        #region Команды Добавления

        private RelayCommand addNewClient;
        public RelayCommand AddNewClient
        {
            get
            {
                return addNewClient ?? new RelayCommand(obj =>
                {
                    if (ClientEmail == null || (!ClientEmail.Contains("@")))
                    {
                        //SetRedBlockControll(wnd, "EmailBlock");
                    }
                    else
                    {
                        Message =  DataWorker.AddClient(
                            ClientEmail, ClientLastName, ClientFirstName, ClientMiddleName, ClientPhone);
                        AllClients = new ObservableCollection<Client>(DataWorker.GetAllClients());
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
                    if (PurchaseProductCode != "" && PurchaseProductName != "")
                    {

                        Message = DataWorker.AddPurchase(SelectedClient.email,
                            PurchaseProductCode, PurchaseProductName);
                        ClientPurchases = new ObservableCollection<Purchase>(DataWorker.GetPurchasesByEmail(SelectedClient?.email));
                    }
                },
            (obj) => obj != null);
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
                    if (SelectedClient != null)
                    {
                        Message = DataWorker.DeleteClient(SelectedClient);
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
