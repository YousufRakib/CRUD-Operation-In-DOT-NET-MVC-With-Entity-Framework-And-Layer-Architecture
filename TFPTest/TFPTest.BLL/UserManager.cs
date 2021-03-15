using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TFPTest.Models;
using TFPTest.Models.ViewModel;
using TFPTest.TFPTest.DAL;

namespace TFPTest.TFPTest.BLL
{
    public class UserManager
    {
        UserRepository userRepository = new UserRepository();
        public long ExistingUser(ViewModel userData, string uploadPath)
        {
            return userRepository.ExistingUser(userData,uploadPath);
        }

        public bool Login(LoginData userData, out int userId)
        {
            return userRepository.Login(userData,out userId);
        }
        public bool LoginAfterConfirm(long userId)
        {
            return userRepository.LoginAfterConfirm(userId);
        }
        

        public List<UserAccount> userDataList()
        {
            return userRepository.userDataList();
        }

        public bool DeleteAccount(int userId)
        {
            return userRepository.DeleteAccount(userId);
        }

        public bool EditAccount(UserAccount userData)
        {
            return userRepository.EditAccount(userData);
        }
    }
}