using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.IServices;
using BLL.Validator;
using DAL.IUnitOfWork1;
using DTO.LikeDTO;
using DTO.UserDto;
using Entities;
using FluentValidation;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _work = unitOfWork;
        }

        public async Task CancelRequest(int receieverId, int senderId)
        {
            await _work._userRepository.CancelRequest(receieverId, senderId);
            await _work.Commit();
        }

        public async Task<bool> CheckFriendShip(int senderId, int receieverId)
        {
            return  _work._userRepository.CheckFriendShip(senderId, receieverId);
        }

        public async Task<bool> CheckIfUserExist(string Email)
        {
            var users = await _work._userRepository.UserEmails();
            if (users.Contains(Email.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Country>> Countries()
        {
            return await _work._userRepository.GetCountries();
        }

		public async Task<Country> CountryByName(int Id)
		{
            return await _work._userRepository.CountriesBySelectList(Id);
		}

		public async Task Create(RegistrationDto user)
        {

            var validatior = new RegistrationValidator();
            var validatiorresult = await validatior.ValidateAsync(user);
			if (!validatiorresult.IsValid)
			{
				throw new ValidationException(validatiorresult.Errors);
			}
			user.JoinedAt = DateTime.Now;
            User user1 = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JoinedAt = user.JoinedAt,
                UserImage = user.UserImage,
                Age = user.Age,
                Birth = user.Birth,
                CountryId = user.CountryId,
                Authentication = new UserAuthentication()
                {
                    Password = user.Authentication.Password,
                    Email = user.Authentication.Email.ToLower(),
                    ConfirmPassword = user.Authentication.ConfirmPassword
                },
            };
            await _work._userRepository.Add(user1);
            await _work.Commit();
        }

        public async Task<List<string>> Emails()
        {
            return await _work._userRepository.UserEmails();
        }

        public async Task<List<FriendRequest>> FriendRequests()
        {
            return await _work._userRepository.GetFriendRequests();
        }

        public async Task<List<UserAuthentication>> Get()
        {
            return await _work._userRepository.Get();
        }

        public async Task<List<UserUpdateDto>> GetFriendsofUser(int userId)
        {
            List<User> Users = await _work._userRepository.GetFriends(userId);
            List<UserUpdateDto> friendsofuser = _mapper.Map<List<UserUpdateDto>>(Users);
            return friendsofuser;
        }

		public async Task<List<UserToListDto>> GetInteractionsAndFriends(int LoggedId)
		{
            List<User>users=await _work._userRepository.GetLastInteractionsAndFriends(LoggedId);
            List<UserToListDto>dto=_mapper.Map<List<UserToListDto>>(users);
            return dto;
		}

		public async Task<User> GetUserByEmail(string Email)
        {
            var user = await _work._userRepository.GetUserWithEmail(Email);
            return user;
        }

		public async Task<List<UserToListDto>> GetUserFiltered(string filter)
		{
            var users = await _work._userRepository.GetUsersByFilter(filter);
            List<UserToListDto> dto = _mapper.Map<List<UserToListDto>>(users);
            return dto;
		}

		public async Task<List<User>> GetUsers()
        {
            return await _work._userRepository.Users();
        }

        public async Task<int> IdOfLoggedUser(string Email)
        {
            return await _work._userRepository.GetIdOfUser(Email);
        }

        public string ImgOfUser(int Id)
        {
            return _work._userRepository.UserPic(Id);
        }

        public async Task Like(LikeToAddDto like)
        {
            Like like1 = _mapper.Map<Like>(like);
            await _work._userRepository.SendLike(like1);
            await _work.Commit();
        }

        public async Task SendRequest(FriendRequest friendRequest)
        {
            await _work._userRepository.AddFriend(friendRequest);
            await _work.Commit();
        }

        public async Task Update(UserUpdateDto user)
        {
            User user2 = _mapper.Map<User>(user);
            await _work._userRepository.Update(user2);
            await _work.Commit();
        }

        public async Task UpdateWithouthImage(UserUpdateDto user)
        {
            User user1 = _mapper.Map<User>(user);
            _work._userRepository.UpdateWithouthImage(user1);
            await _work.Commit();
        }

        public async Task<UserUpdateDto> User(int Id)
        {
            User user = await _work._userRepository.GetById(Id);
            UserUpdateDto dto = _mapper.Map<UserUpdateDto>(user);
            return dto;
        }

        public async Task<UserUpdateDto> UserWithDto(string Email)
        {
            var user = await _work._userRepository.GetUserWithEmail(Email);
            UserUpdateDto dto = _mapper.Map<UserUpdateDto>(user);
            return dto;
        }


    }
}
