using CoffeeShopApp.Models;
using CoffeeShopApp.Controllers;
namespace Expense_Tracker_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "CartId", "dbo.Users");
            DropForeignKey("dbo.CartItems", "CartId", "dbo.Carts");
            DropColumn("dbo.Carts", "UserId");
            RenameColumn(table: "dbo.Carts", name: "CartId", newName: "UserId");
            RenameIndex(table: "dbo.Carts", name: "IX_CartId", newName: "IX_UserId");
            DropPrimaryKey("dbo.Carts");
            AlterColumn("dbo.Carts", "CartId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Carts", "CartId");
            AddForeignKey("dbo.Carts", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.CartItems", "CartId", "dbo.Carts", "CartId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "CartId", "dbo.Carts");
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.Carts");
            AlterColumn("dbo.Carts", "CartId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Carts", "CartId");
            RenameIndex(table: "dbo.Carts", name: "IX_UserId", newName: "IX_CartId");
            RenameColumn(table: "dbo.Carts", name: "UserId", newName: "CartId");
            AddColumn("dbo.Carts", "UserId", c => c.Int(nullable: false));
            AddForeignKey("dbo.CartItems", "CartId", "dbo.Carts", "CartId", cascadeDelete: true);
            AddForeignKey("dbo.Carts", "CartId", "dbo.Users", "UserId");
        }
    }
}
