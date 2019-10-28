namespace Keycloak.NET.FluentAPI.Manage.Sessions
{
    internal class Session : ISession
    {
        private readonly IRealmContext _context;

        public Session(IRealmContext context)
        {
            _context = context;
        }

        public IRealmSessions RealmSessions => new RealmSessions(_context);
        public IRevocation Revocation => new Revocation(_context);
    }
}
