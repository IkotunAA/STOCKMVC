namespace STOCKMVC.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task SaveChangesAsync();
    }
}

