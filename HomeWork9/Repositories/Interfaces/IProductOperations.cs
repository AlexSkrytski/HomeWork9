namespace HomeWork9.Repositories.Interfaces
{
    public interface IProductOperations
    {
        bool CheckStockAvailability(int productId, int requiredQuantity);
    }
}
