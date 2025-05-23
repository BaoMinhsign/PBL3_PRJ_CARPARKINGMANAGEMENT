//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            this.ParkingSpaces = new HashSet<ParkingSpace>();
            this.TRANSACTION_LOG = new HashSet<TRANSACTION_LOG>();
        }
    
        public int VehicleID { get; set; }
        public string LicensePlate { get; set; }
        public int ID_Khach { get; set; }
        public int ID_Ve { get; set; }
        public string VehicleType { get; set; }
    
        public virtual KHACHHANG KHACHHANG { get; set; }
        public virtual LOAIVE LOAIVE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParkingSpace> ParkingSpaces { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSACTION_LOG> TRANSACTION_LOG { get; set; }
    }
}
