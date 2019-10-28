using Keycloak.Net;
using System;
using System.Threading.Tasks;

namespace Keycloak.NET.FluentAPI.Manage
{
    internal class UserGroups : IUserGroups
    {
        private readonly IRealmContext _context;
        private readonly KeycloakClient _client;

        public UserGroups(IRealmContext context)
        {
            _context = context;
            _client = context.Client;
        }

        public async Task<bool> Create(string name)
        {
            try
            {
                var group = new Net.Models.Groups.Group
                {
                    Name = name
                };

                return await _client.CreateGroupAsync(_context.ConnectionSettings.Realm, group);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //TODO: it has to be checked how to use groupName instead of id
        public async Task<bool> Delete(string groupId)
        {
            try
            {
                return await _client.DeleteGroupAsync(_context.ConnectionSettings.Realm, groupId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Rename(string groupId, string newName)
        {
            try
            {
                var existed = await _client.GetGroupAsync(_context.ConnectionSettings.Realm, groupId);
                existed.Name = newName;

                return await _client.UpdateGroupAsync(_context.ConnectionSettings.Realm, groupId, existed);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
