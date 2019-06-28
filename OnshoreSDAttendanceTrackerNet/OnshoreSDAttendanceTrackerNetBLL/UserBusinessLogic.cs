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
        IUserDO _dbUser = new UserDO();
        IUserBO _loginUser = new UserBO();
        IExceptionBO iEx = new ExceptionBO();

        public IUserBO CheckUserLogin(string email, string password)
        {
            password = HashPassword(password);
            if ((_dbUser = _ucda.GetUserLoginInformation(email, password)) != null)
            {
                _loginUser = Mapper.Map<IUserDO, IUserBO>(_dbUser);

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