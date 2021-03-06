//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Isik.SAMS.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SAMS_StudentApplications
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SAMS_StudentApplications()
        {
            this.SAMS_Files = new HashSet<SAMS_Files>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string Email { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> EnrolledBy { get; set; }
        public Nullable<int> ProgramId { get; set; }
        public Nullable<System.Guid> GUID { get; set; }
        public string PassportNumber { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string BachelorUni { get; set; }
        public string BachelorProgram { get; set; }
        public string BachelorCountry { get; set; }
        public Nullable<decimal> BachelorGPA { get; set; }
        public Nullable<System.DateTime> BachelorGradDate { get; set; }
        public string LanguageProficiency { get; set; }
        public string HighSchoolName { get; set; }
        public string HighSchoolGradYear { get; set; }
        public Nullable<decimal> HighSchoolGPA { get; set; }
        public string HighSchoolCountry { get; set; }
        public Nullable<int> LanguageExamScore { get; set; }
        public Nullable<bool> DualCitizenship { get; set; }
        public Nullable<bool> BlueCardOwner { get; set; }
        public Nullable<bool> IsGradFromUni { get; set; }
        public string VerificationCode { get; set; }
    
        public virtual SAMS_ApplicationStatus SAMS_ApplicationStatus { get; set; }
        public virtual SAMS_Department SAMS_Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAMS_Files> SAMS_Files { get; set; }
        public virtual SAMS_Program SAMS_Program { get; set; }
        public virtual SAMS_Users SAMS_Users { get; set; }
        public virtual SAMS_Users SAMS_Users1 { get; set; }
    }
}
