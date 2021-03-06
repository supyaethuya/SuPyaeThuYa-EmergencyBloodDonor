//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FacebookLoginTesting.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class UserProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfile()
        {
            this.Posts = new HashSet<Post>();
        }
    
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string BloodType { get; set; }
        public string Age { get; set; }
        public string PhoneNumber { get; set; }

        [Display(Name = "Date of Latest Donation")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfDonation { get; set; }
        public string Location { get; set; }
        public string ImagePath { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
