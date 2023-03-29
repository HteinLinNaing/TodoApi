using TodoApi.Models;
namespace TodoApi.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly TodoContext _repoContext;

        public RepositoryWrapper(TodoContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        private IHeroRepository? oHero;
        public IHeroRepository Hero
        {
            get
            {
                if (oHero == null)
                {
                    oHero = new HeroRepository(_repoContext);
                }

                return oHero;
            }
        }

        private ITodoItemRepository? oTodoItem;
        public ITodoItemRepository TodoItem
        {
            get
            {
                if (oTodoItem == null)
                {
                    oTodoItem = new TodoItemRepository(_repoContext);
                }
                return oTodoItem;
            }
        }

        private IEmployeeRepository? oEmployee;
        public IEmployeeRepository Employee
        {
            get
            {
                if (oEmployee == null)
                {
                    oEmployee = new EmployeeRepository(_repoContext);
                }
                return oEmployee;
            }
        }

        private ICustomerRepository? oCustomer;
        public ICustomerRepository Customer
        {
            get
            {
                if (oCustomer == null)
                {
                    oCustomer = new CustomerRepository(_repoContext);
                }
                return oCustomer;
            }
        }

        private IAdminRepository? oAdmin;
        public IAdminRepository Admin
        {
            get
            {
                if (oAdmin == null)
                {
                    oAdmin = new AdminRepository(_repoContext);
                }
                return oAdmin;
            }
        }

        private IOTPRepository? oOTP;
        public IOTPRepository OTP
        {
            get
            {
                if (oOTP == null)
                {
                    oOTP = new OTPRepository(_repoContext);
                }
                return oOTP;
            }
        }

        private IEventLogRepository? oEventLog;
        public IEventLogRepository EventLog
        {
            get
            {
                if (oEventLog == null)
                {
                    oEventLog = new EventLogRepository(_repoContext);
                }
                return oEventLog;
            }
        }

        private ICustomerTypeRepository? oCustomerType;
        public ICustomerTypeRepository CustomerType
        {
            get
            {
                if (oCustomerType == null)
                {
                    oCustomerType = new CustomerTypeRepository(_repoContext);
                }
                return oCustomerType;
            }
        }

        private ISupplierRepository? oSupplier;
        public ISupplierRepository Supplier
        {
            get
            {
                if (oSupplier == null)
                {
                    oSupplier = new SupplierRepository(_repoContext);
                }
                return oSupplier;
            }
        }

        private ISupplierTypeRepository? oSupplierType;
        public ISupplierTypeRepository SupplierType
        {
            get
            {
                if (oSupplierType == null)
                {
                    oSupplierType = new SupplierTypeRepository(_repoContext);
                }
                return oSupplierType;
            }
        }
    }
}