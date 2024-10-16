using Project.Models;
using Project.Models.Dtos;
using Project.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys.IRepository
{
    public interface IMessageRepository
    {
        void AddGroup(Group group);
        void RemoveConnection(Connection connection);
        Task<Connection> GetConnection(string connectionId);
        Task<Group> GetMessageGroup(string groupName);
        Task<Group> GetGroupForConnection(string connectionId);
        void AddMessage(Message message);
        void UpdateMessage(Message message);
        Task<Message> GetMessage(string RecipientId);
        Task<Message> GetMessageDefault(string SenderId, string recipientProductId);
        Task<Message> GetMessagePrice(string recipientProductId);
        Task<Message> GetBacktoMoney(string recipientProductId);
        Task<Message> GetWinner(string recipientProductId);
        Task<IEnumerable<MessageDtos>> GetMessageThread(string currentUsername, string recipientProductId);
        bool HasChanges();
        bool MessageisExits(string SenderId, string RecipientId);
        bool CheckTop1(string SenderId, string RecipientId);
        Task<bool> SaveAllAsync();

    }
}
