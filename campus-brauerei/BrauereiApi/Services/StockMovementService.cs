namespace BrauereiApi.Services;
using BrauereiApi.Options;

public interface IStockMovementService
{
    bool ReceiveStock(int articleId, int quantity);
    bool IssueStock(int articleId, int quantity);
}

public class StockMovementService : IStockMovementService
{
    private readonly InventoryBoundariesOptions _options;
    //private readonly _DbContext;

    public StockMovementService(InventoryBoundariesOptions options)
    {
        _options = options;
    }

    public bool ReceiveStock(int articleId, int quantity)
    {
        // TODO: Database call needed for current Stock
        int currentStock = 0; 
        
        int newStock = currentStock + quantity;

        if (newStock > _options.MaxPerArticle)
        {
            Console.WriteLine("Received stock are exceeding the maximum stock per article.");
            return false;
        }
        
        // TODO: Add to database
        
        return true;
        
    }

    public bool IssueStock(int articleId, int quantity)
    {
        // TODO: Database call needed for current Stock
        int currentStock = 0; 
        
        int newStock = currentStock - quantity;

        if (newStock < 0)
        {
            Console.WriteLine("Articles cannot be issued. There are not enough articles in stock.");
            return false;
        }
        else if (newStock < _options.MinPerArticle)
        {
            Console.WriteLine("Warning: Stock for current article is less than the minimum stock per article.");
            // Trigger production call for reproducing/ordering article (out of our scope)
            return true;
        }
        
        // TODO: Add to database
        return true;
    }
}