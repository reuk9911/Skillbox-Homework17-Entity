namespace Skillbox_Homework17_Entity.Model
{
    using Skillbox_Homework17_Entity.Utility;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.InteropServices;

    public partial class Client : INotifyPropertyChanged/*: ObservableObject*/
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Purchases = new HashSet<Purchase>();
        }

        private string _lastName;
        [StringLength(20)]
        public string lastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    RaisePropertyChangedEvent("lastName");
                }

            }
        }

        private string _firstName;
        [StringLength(20)]
        public string firstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    RaisePropertyChangedEvent("firstName");
                }
            }
        }

        private string _middleName;
        [StringLength(20)]
        public string middleName
        {
            get { return _middleName; }
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    RaisePropertyChangedEvent("middleName");
                }
            }
        }

        private string _phone;
        [StringLength(12)]
        public string phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    RaisePropertyChangedEvent("phone");
                }
            }
        }

        private string _email;

        public event PropertyChangedEventHandler PropertyChanged;

        [Key]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchases { get; set; }

        protected void RaisePropertyChangedEvent(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Client(string Email,
            string LastName = "", string FirstName = "", string MiddleName = "", string Phone = "")
        {

        }

    } 
    
}
