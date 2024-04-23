namespace Market.DAL.Repositories
{
    internal sealed class CartsRepository
    {
        private readonly RepositoryContext _context;

        public CartsRepository()
        {
            _context = new RepositoryContext();
        }
    }
}
