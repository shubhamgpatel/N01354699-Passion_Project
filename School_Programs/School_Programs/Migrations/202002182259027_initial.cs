namespace School_Programs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        teacher_id = c.Int(nullable: false, identity: true),
                        teacherName = c.String(),
                        teacherEmail = c.String(),
                        teacherPhone = c.String(),
                    })
                .PrimaryKey(t => t.teacher_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Teachers");
        }
    }
}
