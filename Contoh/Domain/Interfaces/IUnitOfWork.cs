namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IUnitRepository Units { get; }
        int Complete();
    }
}
