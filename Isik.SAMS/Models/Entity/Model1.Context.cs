﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StudentApprovalManagementEntities : DbContext
    {
        public StudentApprovalManagementEntities()
            : base("name=StudentApprovalManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SAMS_Administrator> SAMS_Administrator { get; set; }
        public virtual DbSet<SAMS_ApplicationStatus> SAMS_ApplicationStatus { get; set; }
        public virtual DbSet<SAMS_Department> SAMS_Department { get; set; }
        public virtual DbSet<SAMS_Files> SAMS_Files { get; set; }
        public virtual DbSet<SAMS_Program> SAMS_Program { get; set; }
        public virtual DbSet<SAMS_StudentApplications> SAMS_StudentApplications { get; set; }
        public virtual DbSet<SAMS_Users> SAMS_Users { get; set; }
        public virtual DbSet<SAMS_UserType> SAMS_UserType { get; set; }
    }
}
