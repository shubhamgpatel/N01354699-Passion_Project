namespace School_Programs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgramCourse1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProgramCourses",
                c => new
                    {
                        programCourse_id = c.Int(nullable: false, identity: true),
                        course_id = c.Int(nullable: false),
                        program_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.programCourse_id)
                .ForeignKey("dbo.Courses", t => t.course_id, cascadeDelete: true)
                .ForeignKey("dbo.Programs", t => t.program_id, cascadeDelete: true)
                .Index(t => t.course_id)
                .Index(t => t.program_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProgramCourses", "program_id", "dbo.Programs");
            DropForeignKey("dbo.ProgramCourses", "course_id", "dbo.Courses");
            DropIndex("dbo.ProgramCourses", new[] { "program_id" });
            DropIndex("dbo.ProgramCourses", new[] { "course_id" });
            DropTable("dbo.ProgramCourses");
        }
    }
}
