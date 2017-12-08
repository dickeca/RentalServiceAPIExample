namespace RentalServiceAPI.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SettingsValues", name: "SettingsValueTypeId", newName: "ValueTypeId");
            RenameIndex(table: "dbo.SettingsValues", name: "IX_SettingsValueTypeId", newName: "IX_ValueTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SettingsValues", name: "IX_ValueTypeId", newName: "IX_SettingsValueTypeId");
            RenameColumn(table: "dbo.SettingsValues", name: "ValueTypeId", newName: "SettingsValueTypeId");
        }
    }
}
