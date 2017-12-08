namespace RentalServiceAPI.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SettingsValueTypes", newName: "ValueTypes");
            CreateTable(
                "dbo.TitleMetaValues",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ValueTypeId = c.Int(nullable: false),
                        TitleId = c.Guid(nullable: false),
                        Value = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Titles", t => t.TitleId, cascadeDelete: true)
                .ForeignKey("dbo.ValueTypes", t => t.ValueTypeId, cascadeDelete: true)
                .Index(t => t.ValueTypeId)
                .Index(t => t.TitleId);
            
            AddColumn("dbo.RentalHistories", "ReturnByDate", c => c.DateTime());
            AddColumn("dbo.RentalHistories", "ReturnedDate", c => c.DateTime());
            DropColumn("dbo.RentalHistories", "ReturnDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RentalHistories", "ReturnDate", c => c.DateTime());
            DropForeignKey("dbo.TitleMetaValues", "ValueTypeId", "dbo.ValueTypes");
            DropForeignKey("dbo.TitleMetaValues", "TitleId", "dbo.Titles");
            DropIndex("dbo.TitleMetaValues", new[] { "TitleId" });
            DropIndex("dbo.TitleMetaValues", new[] { "ValueTypeId" });
            DropColumn("dbo.RentalHistories", "ReturnedDate");
            DropColumn("dbo.RentalHistories", "ReturnByDate");
            DropTable("dbo.TitleMetaValues");
            RenameTable(name: "dbo.ValueTypes", newName: "SettingsValueTypes");
        }
    }
}
