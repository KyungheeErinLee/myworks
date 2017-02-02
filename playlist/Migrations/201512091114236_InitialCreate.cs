namespace TestTwo_20151.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieTitle = c.String(),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Director_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directors", t => t.Director_Id)
                .Index(t => t.Director_Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreFulls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieForLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieTitle = c.String(nullable: false),
                        TicketPrice = c.Decimal(precision: 18, scale: 2),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Director_Id = c.Int(),
                        GenreFull_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directors", t => t.Director_Id)
                .ForeignKey("dbo.GenreFulls", t => t.GenreFull_Id)
                .Index(t => t.Director_Id)
                .Index(t => t.GenreFull_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ContentType = c.String(),
                        ImageFile = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Movie_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieForLists", "GenreFull_Id", "dbo.GenreFulls");
            DropForeignKey("dbo.MovieForLists", "Director_Id", "dbo.Directors");
            DropForeignKey("dbo.GenreMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Movies", "Director_Id", "dbo.Directors");
            DropIndex("dbo.GenreMovies", new[] { "Movie_Id" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_Id" });
            DropIndex("dbo.MovieForLists", new[] { "GenreFull_Id" });
            DropIndex("dbo.MovieForLists", new[] { "Director_Id" });
            DropIndex("dbo.Movies", new[] { "Director_Id" });
            DropTable("dbo.GenreMovies");
            DropTable("dbo.Images");
            DropTable("dbo.MovieForLists");
            DropTable("dbo.GenreFulls");
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
            DropTable("dbo.Directors");
        }
    }
}
