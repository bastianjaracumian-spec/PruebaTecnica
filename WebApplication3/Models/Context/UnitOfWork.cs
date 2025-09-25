namespace ApiAtencionesMédicas.Models.Context
{
    public class UnitOfWork : IDisposable
    {
        public readonly ApiDBContext _context;
        public UnitOfWork(ApiDBContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
