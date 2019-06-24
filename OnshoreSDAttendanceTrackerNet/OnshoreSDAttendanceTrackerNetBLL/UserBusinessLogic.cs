using AutoMapper;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace OnshoreSDAttendanceTrackerNetBLL
{
    public class UserBusinessLogic
    {

        static UserCredentialsDataAccess _ucda = new UserCredentialsDataAccess();
        IUserLoginDO _dbUser = new UserLoginDO();
        IUserLoginBO _loginUser = new UserLoginBO();
        IExceptionBO iEx = new ExceptionBO();

        public IUserLoginBO CheckUserLogin(IUserLoginBO userLoginBO)
        {
            userLoginBO.Password = HashPassword(userLoginBO.Password);
          if((_dbUser = _ucda.GetUserLoginInformation(userLoginBO.Email, userLoginBO.Password)) != null)
            {
                _loginUser = Mapper.Map<IUserLoginDO,IUserLoginBO>(_dbUser);

                return _loginUser;
            } 
            else
            {
                return _loginUser;
            }
        }

    
    //Hash password

    public string HashPassword(string _passwordToHash)
    {
        StringBuilder _passwordReturn = new StringBuilder();
      
        SHA256 hash = SHA256.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(_passwordToHash);
        byte[] hashedBytes = hash.ComputeHash(inputBytes);
        for (int i = 0; i < hashedBytes.Length; i++)
        {
            _passwordReturn.Append(hashedBytes[i].ToString("X2"));
        }

        return _passwordReturn.ToString();
    }

   }
}