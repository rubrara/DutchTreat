using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext context, 
            IWebHostEnvironment env,
            UserManager<StoreUser> userManager
            )
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("koki@koki.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Kostadin",
                    LastName = "Rtoski",
                    Email = "koki@koki.com",
                    UserName = "rubraa"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if (!_context.Products.Any())
            {
                var filePath = Path.Combine(_env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                products.ToList().ForEach(p =>
                {
                    p.Id = Guid.NewGuid();
                });

                _context.AddRange(products);

                var orderItem = new OrderItem()
                {
                    Product = products.First(),
                    Quantity = 5,
                    UnitPrice = products.First().Price
                };

                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    OrderNumber = "10000",
                    Items = new List<OrderItem>() {
                        orderItem
                    },
                    User = user

                };

                _context.Orders.Add(order);

                _context.SaveChanges();
            }
        }
    }
}
