namespace School_Programs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgramCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        program_id = c.Int(nullable: false, identity: true),
                        programName = c.String(),
                        programLength = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.program_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Programs");
        }
    }
}
