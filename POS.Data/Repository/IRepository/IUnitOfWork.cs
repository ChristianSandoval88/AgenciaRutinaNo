namespace POS.Data.Repository.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IPurchaseRepository Purchase { get; }
        void Save();
    }
}