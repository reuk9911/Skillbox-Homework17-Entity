namespace Skillbox_Homework17_Entity.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchase: INotifyPropertyChanged
    {
        private int _id;
        private string _email;
        private string _productCode;
        private string _productName;

        public int id { get; set; }

        [Required]
        [StringLength(40)]
        public string email
        {
            get { return _email; }
            set 
            {
                if (_email != value)
                {
                    _email = value;
                    RaisePropertyChangedEvent("email");
                }
            }
        }
        [StringLength(10)]
        public string productCode
        {
            get { return _productCode; }
            set
            {
                if (_productCode != value)
                {
                    _productCode = value;
                    RaisePropertyChangedEvent("productCode");
                }
            }
        }

        [StringLength(40)]
        public string productName
        {
            get { return _productName; }
            set
            {
                if (_productName != value)
                {
                    _productName = value;
                    RaisePropertyChangedEvent("productName");
                }
            }
        }
        public virtual Client Client { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
