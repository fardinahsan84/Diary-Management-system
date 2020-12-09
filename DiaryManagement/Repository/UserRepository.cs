using DiaryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalDiary.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public IEnumerable<User> GetHighestPaidEmployees()
        {
            throw new NotImplementedException();
        }
    }
}