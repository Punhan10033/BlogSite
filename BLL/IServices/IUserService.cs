using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.LikeDTO;
using DTO.UserDto;
using Entities;

namespace BLL.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task Create(RegistrationDto user);
        Task<bool> CheckIfUserExist(string Email);
        Task<User> GetUserByEmail(string Email);
        Task<UserUpdateDto> UserWithDto(string Email);
        Task Update(UserUpdateDto user);
        Task<List<string>> Emails();
        Task<List<UserAuthentication>> Get();
        Task<UserUpdateDto> User(int Id);
        string ImgOfUser(int Id);
        Task UpdateWithouthImage(UserUpdateDto user);
        Task<List<Country>> Countries();
        Task<int> IdOfLoggedUser(string Email);
        Task SendRequest(FriendRequest friendRequest);
        Task<bool> CheckFriendShip(int senderId,int receieverId);
        Task CancelRequest(int receieverId,int senderId);
        Task<List<FriendRequest>> FriendRequests();
        Task<List<UserUpdateDto>> GetFriendsofUser(int userId);
        Task<List<UserToListDto>> GetUserFiltered(string filter);
        Task<List<UserToListDto>> GetInteractionsAndFriends(int LoggedId);
        Task<Country> CountryByName(int Id);
        Task Like(LikeToAddDto like);
    }
}
