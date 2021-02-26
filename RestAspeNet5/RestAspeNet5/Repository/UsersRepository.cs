using RestAspeNet5.Data.VO;
using RestAspeNet5.Modals;
using RestAspeNet5.Modals.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RestAspeNet5.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MySQLContext _context;
        public UsersRepository(MySQLContext context) {
            _context = context;
        }
        public Users ValidateCredentials(UserVO user)
        {
            var pass = ComputerHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.users.FirstOrDefault(u => (u.UserName == user.UserName)&& (u.Password==pass));
        }
        public Users ValidateCredentials(string userName)
        {
            return _context.users.FirstOrDefault(u => (u.UserName == userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.users.FirstOrDefault(u => (u.UserName == userName));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }
        private string ComputerHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashByte = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashByte);
        }
        public Users RefreshUserInfo(Users user)
        {
            if (!_context.users.Any(u => u.ID.Equals(user.ID))) return null;
            var result = _context.users.SingleOrDefault(u => u.ID.Equals(user.ID));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user.ID);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

    }
}
