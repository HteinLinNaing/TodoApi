namespace TodoApi.Repositories
{
    public interface IRepositoryWrapper
    {
        IHeroRepository Hero { get; }
        ITodoItemRepository TodoItem { get; }
        IEmployeeRepository Employee { get; }
        ICustomerRepository Customer { get; }
        IAdminRepository Admin { get; }
    }
}
