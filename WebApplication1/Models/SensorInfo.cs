//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SensorInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SensorInfo()
        {
            this.New_Soy = new HashSet<New_Soy>();
        }
    
        public int SenID { get; set; }
        public string DID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string IP { get; set; }
        public Nullable<int> SFType { get; set; }
        public string ZID { get; set; }
        public string SenZT { get; set; }
        public string SenName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<New_Soy> New_Soy { get; set; }
        public virtual User User { get; set; }
    }
}
