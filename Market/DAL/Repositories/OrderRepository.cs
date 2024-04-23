namespace Market.DAL.Repositories
{
    public class OrderRepository
    {
        private RepositoryContext _repositoryContext;

        public OrderRepository() 
        {
            _repositoryContext = new RepositoryContext();
        }
    }
}
