namespace carShop.Data
{
    public interface IRepositoryManager
    {
        ICarRepository Cars { get; }
        IClientRepository Clients { get; }
        IEmployeeRepository Employees { get; }

        void SaveChanges();
    }
}