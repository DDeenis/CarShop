using carShop.Context;
using carShop.Models;

namespace carShop.Data
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(CarShopContext context) : base(context)
        {
        }
    }
}