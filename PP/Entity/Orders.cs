//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PP.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            this.OrderService = new HashSet<OrderService>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<System.TimeSpan> TimeOrder { get; set; }
        public Nullable<int> CodeClient { get; set; }
        public string StatusOrder { get; set; }
        public Nullable<System.DateTime> DateClose { get; set; }
        public Nullable<int> TimeRental { get; set; }
    
        public virtual Clients Clients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderService> OrderService { get; set; }
    }
}
