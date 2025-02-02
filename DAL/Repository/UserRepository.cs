using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using DAL.DataContext;
using DAL.iRepository;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;
        public UserRepository(BlogContext blogContext)
        {
            _context = blogContext;
        }

        public async Task Add(User user)
        {
            user.Authentication.Email.ToLower();
            await _context.Users.AddAsync(user);
        }

        public async Task AddFriend(FriendRequest friendRequest)
        {

            await _context.FriendRequests.AddAsync(friendRequest);
        }

        public async Task CancelRequest(int ReceieverId, int SenderId)
        {
            var request = await _context.FriendRequests.Where(x => x.ReceieverId == ReceieverId && x.SenderId == SenderId).FirstOrDefaultAsync();
            _context.FriendRequests.Remove(request);
        }

        public bool CheckFriendShip(int senderId, int receieverId)
        {
            var reqeust=_context.FriendRequests.Where(x=>x.SenderId==senderId && x.ReceieverId==receieverId || x.SenderId==receieverId && x.ReceieverId==senderId).FirstOrDefault();
            if (reqeust != null)
            {   
                return true;
            }
            else
            {
                return false;
            }
        }

		public async Task<Country> CountriesBySelectList(int Id)
		{
            return await _context.Countries.Where(x=>x.CountryId==Id).FirstOrDefaultAsync();
		}

		public async Task<List<UserAuthentication>> Get()
        {
            return await _context.UserAuthentication.ToListAsync();
        }

        public async Task<User> GetById(int Id)
        {
            return await _context.Users.Include(x => x.Country).Where(x => x.UserId == Id).FirstOrDefaultAsync();
        }

 

        public async Task<List<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<List<FriendRequest>> GetFriendRequests()
        {
            return await _context.FriendRequests.ToListAsync();
        }

        public async Task<List<User>> GetFriends(int UserId)
        {
            var friendrquests=await _context.FriendRequests.Where(x=>x.ReceieverId==UserId||x.SenderId==UserId).ToListAsync();
            List<User> users = new List<User>();
            foreach (var friend in friendrquests)
            {
                if (friend.SenderId == UserId)
                {
                    users.Add(await _context.Users.Where(x => x.UserId == friend.ReceieverId).FirstOrDefaultAsync());
                }
                else
                {
                    users.Add(await _context.Users.Where(x => x.UserId == friend.SenderId).FirstOrDefaultAsync());
                }
            }
            return users;
        }   
        public async Task<int> GetIdOfUser(string Email)
        {
            return await _context.Users.Where(x => x.Authentication.Email.ToLower() == Email.ToLower()).Select(x => x.UserId).FirstOrDefaultAsync();
        }

		public async Task<List<User>> GetLastInteractionsAndFriends(int loggedId)
		{
            var messages=await _context.Messages2.Where(x=>x.SenderId== loggedId || x.ReceieverId ==loggedId).OrderBy(x=>x.MessageDate).ToListAsync();
            List<User>users = new List<User>();
            foreach(var m in messages.OrderByDescending(x=>x.MessageDate))
            {
                if(m.SenderId != loggedId)
                {
                    var user= await _context.Users.Where(x => x.UserId == m.SenderId).Include(x=>x.UserSender.Where(x=>x.MessageStatus==false && x.ReceieverId==loggedId)).FirstOrDefaultAsync();
                    if (!users.Contains(user))
                    { 
                        users.Add(user);
                    }
                }
                else
                {
                    var user = await _context.Users.Where(x => x.UserId == m.ReceieverId).FirstOrDefaultAsync();
                    if(!users.Contains(user))
                    {
                        users.Add(user);
                    }
                }
            }
            return users;
		}

		public async Task<List<User>> GetUsersByFilter(string searchvalue)
		{
            return await _context.Users.Where(x => x.FirstName.Contains(searchvalue) || x.LastName.Contains(searchvalue)).ToListAsync();
		}

		public async Task<User> GetUserWithEmail(string Email)
        {
            return await _context.Users.Where(x => x.Authentication.Email.ToLower() == Email.ToLower()).Include(x => x.Country).FirstOrDefaultAsync();
        }



        public async Task Register(User user)
        {
            await _context.AddAsync(user);
        }

        public async Task SendLike(Like like)
        {
            like.LikeDate=DateTime.Now;
            await _context.Likes.AddAsync(like);
        }

        public async Task Update(User user)
        {
            user.Age = (int)((DateTime.Now - user.Birth).TotalDays / 365.242199);
            _context.Update(user);
            _context.Entry(user).Property(x => x.JoinedAt).IsModified = false;
        }

        public void UpdateWithouthImage(User user)
        {
            user.Age = (int)((DateTime.Now - user.Birth).TotalDays / 365.242199);
            _context.Update(user);
            _context.Entry(user).Property(x => x.JoinedAt).IsModified = false;
            _context.Entry(user).Property(x => x.UserImage).IsModified = false;

        }

        public async Task<User> User(string Email)
        {
            return await _context.Users.Where(x => x.Authentication.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<List<string>> UserEmails()
        {
            return await _context.UserAuthentication.Select(x => x.Email).ToListAsync();
        }

        public string UserPic(int Id)
        {
            return _context.Users.Where(x => x.UserId == Id).Select(x => x.UserImage).FirstOrDefault();
        }

        public async Task<List<User>> Users()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
