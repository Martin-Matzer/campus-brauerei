namespace BrauereiApi.Options;

public class InventoryBoundariesOptions
{
    public const string SectionName = "InventoryBoundaries";

    public int MinPerArticle { get; set; }
    public int MaxPerArticle { get; set; }
}
