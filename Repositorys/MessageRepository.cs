using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Project.Data;
using Project.Models;
using Project.Models.Dtos;
using Project.Models.Groups;
using Project.Repositorys.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public MessageRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void AddGroup(Group group)
        {
            _db.Groups.Add(group);
        }

        public void AddMessage(Message message)
        {
            _db.Messages.Add(message);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _db.Connections.FindAsync(connectionId);
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _db.Groups
                .Include(c => c.Connections)
                .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        }


        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _db.Groups
                .Include(x => x.Connections)
                .FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public async Task<IEnumerable<MessageDtos>> GetMessageThread(string currentUsername, string recipientProductId)
        {
            var messages = await _db.Messages
                .Where(m => m.Recipient.Id == recipientProductId)
                .OrderBy(m => m.Content)
                .ProjectTo<MessageDtos>(_mapper.ConfigurationProvider)
                .ToListAsync();


            return messages;
        }

        public void RemoveConnection(Connection connection)
        {
            _db.Connections.Remove(connection);
        }

        public bool HasChanges()
        {
            return _db.ChangeTracker.HasChanges();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public void UpdateMessage(Message message)
        {
            _db.Messages.Update(message);
        }

        public bool MessageisExits(string SenderId, string RecipientId)
        {
            return _db.Messages.Any(u => u.SenderId == SenderId && u.RecipientId == RecipientId);
        }

        public async Task<Message> GetMessagePrice(string recipientProductId)
        {
            return await _db.Messages
                .OrderByDescending(c => c.Content)
                .FirstOrDefaultAsync(m => m.RecipientId == recipientProductId);
        }

        public async Task<Message> GetBacktoMoney(string recipientProductId)
        {
            return await _db.Messages.FirstOrDefaultAsync(m => m.MoneyBack == false && m.RecipientId == recipientProductId);
        }

        public async Task<Message> GetMessage(string RecipientId)
        {
            return await _db.Messages
                .Include(u => u.Sender)
                .Include(u => u.Recipient)
                .FirstOrDefaultAsync(x => x.RecipientId == RecipientId && x.MoneyBack == false);
        }

        public bool CheckTop1(string SenderId, string RecipientId)
        {
            return _db.Messages.Any(u => u.SenderId == SenderId && u.RecipientId == RecipientId && u.MoneyBack == false);
        }

        public async Task<Message> GetMessageDefault(string SenderId, string recipientProductId)
        {
            return await _db.Messages.FirstOrDefaultAsync(m => m.SenderId == SenderId && m.RecipientId == recipientProductId);
        }

        public async Task<Message> GetWinner(string recipientProductId)
        {
            return await _db.Messages.FirstOrDefaultAsync(m => m.MoneyBack == false && m.RecipientId == recipientProductId);
        }
    }
}
