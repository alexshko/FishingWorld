using System;
using System.Threading.Tasks;

namespace alexshko.fishingworld.Core.DB
{
    public interface UserDataBase
    {
        Task<User> ReadUserData(int timeout);
        Task<User> ReadUserCreateEmptyIfNotExistInDB();
        //Task UserUpdateCurrency(Currency currency, int newVal);
        Task SaveUserData(User u);
    }
}