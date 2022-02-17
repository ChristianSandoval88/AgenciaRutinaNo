namespace POS.Data.Repository.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        void Save();
    }
}