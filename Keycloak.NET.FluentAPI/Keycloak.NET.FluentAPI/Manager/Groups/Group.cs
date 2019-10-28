using Keycloak.NET.FluentAPI.Manage.Groups;

namespace Keycloak.NET.FluentAPI.Manage
{
    internal class Group : IGroup
    {
        private readonly IRealmContext _context;

        public Group(IRealmContext context)
        {
            _context = context;
        }

        public IUserGroups UserGroups => new UserGroups(_context);
        public IDefaultGroups DefaultGroups => new DefaultGroups(_context);
    }
}
