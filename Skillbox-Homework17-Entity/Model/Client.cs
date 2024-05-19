namespace Skillbox_Homework17_Entity.Model
{
    using Skillbox_Homework17_Entity.Utility;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.InteropServices;

    public partial class Client: ObservableObject
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
                _lastName = value;
                base.RaisePropertyChangedEvent("lastName");
            } 
        }

        private string _firstName;
        [StringLength(20)]
        public string firstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                base.RaisePropertyChangedEvent("firstName");
            }
        }

        private string _middleName;
        [StringLength(20)]
        public string middleName
        {
            get { return _middleName; }
            set
            {
                _middleName = value;
                base.RaisePropertyChangedEvent("middleName");
            }
        }

        private string _phone;
        [StringLength(12)]
        public string phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                base.RaisePropertyChangedEvent("phone");
            }
        }

        private string _email;
        [Key]
        [StringLength(40)]
        public string email
        {
            get { return _email; }
            set
            {
                _email = value;
                base.RaisePropertyChangedEvent("email");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
