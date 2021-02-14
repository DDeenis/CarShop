using carShop.Context;

namespace carShop.Data
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CarShopContext context;
        private ICarRepository carRepository;
        private IClientRepository clientRepository;
        private IEmployeeRepository employeeRepository;

        public RepositoryManager(CarShopContext context)
        {
            this.context = context;
        }

        public ICarRepository Cars
        {
            get
            {
                if(carRepository == null)
                    carRepository = new CarRepository(context);

                return carRepository;
            }
        }

        public IClientRepository Clients
        {
            get
            {
                if(clientRepository == null)
                    clientRepository = new ClientRepository(context);

                return clientRepository;
            }
        }

        public IEmployeeRepository Employees
        {
            get
            {
                if(employeeRepository == null)
                    employeeRepository = new EmployeeRepository(context);

                return employeeRepository;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}