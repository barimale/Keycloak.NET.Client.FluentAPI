using Keycloak.NET.FluentAPI.Manage.Events;
using Keycloak.NET.FluentAPI.Manage.Export;
using Keycloak.NET.FluentAPI.Manage.Import;
using Keycloak.NET.FluentAPI.Manage.Sessions;
using Event = Keycloak.NET.FluentAPI.Manage.Events.Event;
using PartialImport = Keycloak.NET.FluentAPI.Manage.Import.PartialImport;

namespace Keycloak.NET.FluentAPI.Manage
{
    internal class Manager : IManager
    {
        private readonly IRealmContext _context;

        public Manager(IRealmContext context)
        {
            _context = context;
        }

        public IGroup Groups => new Group(_context);
        public IEvents Events => new Event(_context);
        public ISession Sessions => new Session(_context);
        public IPartialImport Import => new PartialImport(_context);
        public IPartialExport Export => new PartialExport(_context);
    }
}