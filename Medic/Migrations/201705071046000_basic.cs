namespace Medic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointment",
                c => new
                    {
                        AppointmentID = c.Int(nullable: false, identity: true),
                        DoctorID = c.Int(nullable: false),
                        PatientID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Place = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentID)
                .ForeignKey("dbo.Doctor", t => t.DoctorID, cascadeDelete: true)
                .ForeignKey("dbo.Patient", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.DoctorID)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.Doctor",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Specialization = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DoctorID);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.PatientID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointment", "PatientID", "dbo.Patient");
            DropForeignKey("dbo.Appointment", "DoctorID", "dbo.Doctor");
            DropIndex("dbo.Appointment", new[] { "PatientID" });
            DropIndex("dbo.Appointment", new[] { "DoctorID" });
            DropTable("dbo.Patient");
            DropTable("dbo.Doctor");
            DropTable("dbo.Appointment");
        }
    }
}
