namespace Keycloak.NET.FluentAPI.Manage.Events
{
    internal class Event : IEvents
    {
        private readonly IRealmContext _context;

        public Event(IRealmContext context)
        {
            _context = context;
        }

        public ILoginEvents LoginEvents => new LoginEvents(_context);

        public IAdminEvents AdminEvents => new AdminEvents(_context);

        public IConfig Config => new Config(_context);
    }
}
