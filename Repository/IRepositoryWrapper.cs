namespace TodoApi.Repositories
{
    public interface IRepositoryWrapper
    {
        IHeroRepository Hero { get; }
        ITodoItemRepository TodoItem { get; }
        IEmployeeRepository Employee { get; }
        ICustomerRepository Customer { get; }
        IAdminRepository Admin { get; }
        IOTPRepository OTP { get; }
        IEventLogRepository EventLog { get; }
        ICustomerTypeRepository CustomerType { get; }
        ISupplierRepository Supplier { get; }
        ISupplierTypeRepository SupplierType { get; }
    }
}
