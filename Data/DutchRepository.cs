using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;
        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

       
        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            return !includeItems ? _context.Orders.ToList(): _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).ToList(); 
        }

        public IEnumerable<Order> GetAllOrdersByUser(string? username, bool includeItems)
        {
            var orders = _context.Orders.Where(o => o.User.UserName.Equals(username));

            return includeItems ? orders.Include(o => o.Items).ThenInclude(i => i.Product).ToList(): orders.ToList(); 
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            { 
                return _context.Products.OrderBy(p => p.Title);
            }
            catch(Exception ex) {
                _logger.LogError($"Failed to get all product: {ex}");
                return Enumerable.Empty<Product>();
            }
        }

        public IEnumerable<Product> GetAllProductsByCategory(string category)
        {
            return _context.Products.Where(p => p.Category == category);
        }

        public Order? GetOrderById(string username, Guid id)
        {
            return _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).FirstOrDefault(o => o.Id == id && o.User.UserName == username);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
