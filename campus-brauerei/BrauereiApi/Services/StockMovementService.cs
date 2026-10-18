using BrauereiApi.Data;
using BrauereiApi.Models;
using BrauereiApi.Options;
using Microsoft.Extensions.Options;
namespace BrauereiApi.Services;

public interface IStockMovementService
{
    Task<MovementStatus> ReceiveStock(int articleId, int quantity);
    Task<MovementStatus> IssueStock(int articleId, int quantity);
}

public class StockMovementService : IStockMovementService
{
    private readonly InventoryBoundariesOptions _options;
    private readonly InventoryDbContext _context;
    
    public StockMovementService(IOptions<InventoryBoundariesOptions> options,  InventoryDbContext context)
    {
        _options = options.Value;
        _context = context;
    }

    public async Task<MovementStatus> ReceiveStock(int id, int amount) 
    {
        if (amount <= 0)
        { 
            return MovementStatus.BadRequest;
        }
        
        var item = await _context.BeerArticles.FindAsync(id);
        
        if (item == null)
        {
            return MovementStatus.NotFound;
        }
        
        int currentStock = item.Lagerbestand; 
        int newStock = currentStock + amount;

        if (newStock > _options.MaxPerArticle)
        {
            Console.WriteLine("Received stock are exceeding the maximum stock per article.");
            return MovementStatus.ExceedsMaximum;
        }
        
        item.Lagerbestand = newStock;
        _context.BeerArticles.Update(item);
        _context.SaveChanges();
        return MovementStatus.Success;
        
    }

    public async Task<MovementStatus> IssueStock(int id, int amount)
    {
        if (amount <= 0)
        { 
            return MovementStatus.BadRequest;
        }
        
        var item = await _context.BeerArticles.FindAsync(id);
        
        if (item == null)
        {
            return MovementStatus.NotFound;
        }
        
        int currentStock = item.Lagerbestand; 
        int newStock = currentStock - amount;

        if (newStock < 0)
        {
            Console.WriteLine("Articles cannot be issued. There are not enough articles in stock.");
            return MovementStatus.InsufficientStock;
        }
        else if (newStock < _options.MinPerArticle)
        {
            Console.WriteLine("Warning: Stock for current article is less than the minimum stock per article.");
            // Trigger production call for reproducing/ordering article (out of our scope)
            Console.WriteLine("Trigger production for reproduction.");
        }
        
        item.Lagerbestand = newStock;
        _context.BeerArticles.Update(item);
        await _context.SaveChangesAsync();
        return MovementStatus.Success;
    }
}