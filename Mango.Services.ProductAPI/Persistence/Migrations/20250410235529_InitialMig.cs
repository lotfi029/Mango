using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Smart Phone", "IPhone X", "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B", "IPhone X", 1000m },
                    { 2, "Smart Phone", "Samsung Galaxy S10", "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B", "Samsung Galaxy S10", 900m },
                    { 3, "Smart Phone", "Google Pixel 3", "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B", "Google Pixel 3", 800m },
                    { 4, "Smart Phone", "OnePlus 6T", "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B", "OnePlus 6T", 700m },
                    { 5, "Smart Phone", "Nokia 9 PureView", "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B", "Nokia 9 PureView", 600m },
                    { 6, "Smart Phone", "Sony Xperia 1", "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B", "Sony Xperia 1", 1100m },
                    { 7, "Smart Phone", "LG G8 ThinQ", "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B", "LG G8 ThinQ", 800m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
