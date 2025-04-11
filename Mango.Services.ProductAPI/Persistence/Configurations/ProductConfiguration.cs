using Mango.Services.ProductAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mango.Services.ProductAPI.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.Description)
            .HasMaxLength(2000);

        builder.Property(e => e.Name)
            .HasMaxLength(100);
        
        builder.Property(e => e.CategoryName)
            .HasMaxLength(100);



        Product[] productsToSeed = 
        [
            new()
            {
                Id = 1,
                Name = "IPhone X",
                Description = "IPhone X",
                Price = 1000,
                CategoryName = "Smart Phone",
                ImageUrl = "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B"
            },
            new()
            {
                Id = 2,
                Name = "Samsung Galaxy S10",
                Description = "Samsung Galaxy S10",
                Price = 900,
                CategoryName = "Smart Phone",
                ImageUrl = "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B"
            },
            new()
            {
                Id = 3,
                Name = "Google Pixel 3",
                Description = "Google Pixel 3",
                Price = 800,
                CategoryName = "Smart Phone",
                ImageUrl = "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B"
            },
            new()
            {
                Id = 4,
                Name = "OnePlus 6T",
                Description = "OnePlus 6T",
                Price = 700,
                CategoryName = "Smart Phone",
                ImageUrl = "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B"
            },
            new()
            {
                Id = 5,
                Name = "Nokia 9 PureView",
                Description = "Nokia 9 PureView",
                Price = 600,
                CategoryName = "Smart Phone",
                ImageUrl = "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B"
            },
            new()
            {
                Id = 6,
                Name = "Sony Xperia 1",
                Description = "Sony Xperia 1",
                Price = 1100,
                CategoryName = "Smart Phone",
                ImageUrl = "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B"
            },
            new()
            {
                Id = 7,
                Name = "LG G8 ThinQ",
                Description = "LG G8 ThinQ",
                Price = 800,
                CategoryName = "Smart Phone",
                ImageUrl = "https://fakeimg.pl/440x230/282828/eae0d0/?retina=1&text=Supports%20emojis!%20%F0%9F%98%8B"
            }
        ];

        builder.HasData(productsToSeed);
    }
}
