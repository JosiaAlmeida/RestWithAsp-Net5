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

        private string ComputerHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashByte = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashByte);
        }
    }
}
