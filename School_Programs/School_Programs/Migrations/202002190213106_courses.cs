namespace School_Programs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        course_id = c.Int(nullable: false, identity: true),
                        courseName = c.String(),
                        courseMessage = c.String(),
                        teacher_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.course_id)
                .ForeignKey("dbo.Teachers", t => t.teacher_id, cascadeDelete: true)
                .Index(t => t.teacher_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "teacher_id", "dbo.Teachers");
            DropIndex("dbo.Courses", new[] { "teacher_id" });
            DropTable("dbo.Courses");
        }
    }
}
