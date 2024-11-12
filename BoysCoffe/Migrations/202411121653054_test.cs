namespace BoysCoffe.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "ProductId");
        }

        public override void Down()
        {
            AddColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
        }
    }
}
