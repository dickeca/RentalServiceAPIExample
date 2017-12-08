namespace RentalServiceAPI.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ValueType = Model.ValueType;

    internal sealed class Configuration : DbMigrationsConfiguration<RentalServiceAPI.Model.Context.RentalServiceDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RentalServiceAPI.Model.Context.RentalServiceDbContext";
        }

        protected override void Seed(RentalServiceAPI.Model.Context.RentalServiceDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Titles.AddOrUpdate(t => t.DisplayName,
                new Title
                {
                    DisplayName = "Spaceballs 3: The Search for Spaceballs 2",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Star Wars Episode XXIX: The Corporate Sellout",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Animals in Nature",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Animals in Nature 2",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Not that Scary of a Movie 5",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Cheap Stock Parody Parody",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Onions and Their Many Uses",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Monty Python and the Holy Grail",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "History of the World Part 2",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Shrek 2: John Cleese is in this one",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Yet Another Coming of Age Film",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Cannibal! The Musical",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Shpadoinkle: The Making of Cannibal! The Musical",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "The Rocky Horror Picture Show",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "An Evening with Death",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Buried Alive",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "Monster Mart",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                },
                new Title
                {
                    DisplayName = "The King of Kong: A Fistful of Quarters",
                    MediaType = MediaType.BluRay,
                    TotalStock = 100,
                    AvailableStock = 100
                }
                );

            context.ValueTypes.AddOrUpdate(
                vt => vt.DisplayName,
                new ValueType
                {
                    DisplayName = "Address 1",
                    ValueFormat = ValueFormat.String
                },
                new ValueType
                {                
                    DisplayName = "Address 2",
                    ValueFormat = ValueFormat.String
                },
                new ValueType
                {           
                    DisplayName = "Address City",
                    ValueFormat = ValueFormat.String
                },
                new ValueType
                {      
                    DisplayName = "Address State",
                    ValueFormat = ValueFormat.String
                },
                new ValueType
                {       
                    DisplayName = "PaymentMethod",
                    ValueFormat = ValueFormat.Int
                },
                new ValueType
                {             
                    DisplayName = "Address Zipcode",
                    ValueFormat = ValueFormat.String
                }, 
                new ValueType
                {
                    DisplayName = "TitleDirector",
                    ValueFormat = ValueFormat.String
                },
                new ValueType
                {     
                    DisplayName = "TitleGenre",
                    ValueFormat = ValueFormat.String
                },
                new ValueType
                {             
                    DisplayName = "TitleActor",
                    ValueFormat = ValueFormat.String
                }
            );
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
