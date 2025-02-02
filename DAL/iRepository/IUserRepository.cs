using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL.iRepository
{
	public interface IUserRepository
	{
		Task<List<User>> Users();
		Task Add(User user);
		Task Update(User user);
		Task Register(User user);
		Task<List<string>> UserEmails();
		Task<List<UserAuthentication>> Get();
		Task<User> GetUserWithEmail(string Email);
		Task<User> GetById(int Id);
		string UserPic(int Id);
		void UpdateWithouthImage(User user);
		Task<List<Country>> GetCountries();
		Task<int> GetIdOfUser(string Email);
		Task AddFriend(FriendRequest friendRequest);
		bool CheckFriendShip(int senderId, int receieverId);
		Task CancelRequest(int receieverId,int senderId);
		Task<List<FriendRequest>> GetFriendRequests();
		Task<List<User>>GetFriends(int senderId);
		Task<List<User>> GetLastInteractionsAndFriends(int loggedId);
		Task<List<User>> GetUsersByFilter(string searchvalue);
		Task<Country> CountriesBySelectList(int Id);
		Task SendLike(Like like);
	}
}
