using System;
using System.Threading.Tasks;

namespace alexshko.fishingworld.Core.DB
{
    public interface UserDataBase
    {
        Task<User> ReadUserData(int timeout);
        Task ReadUserCreateEmptyIfNotExistInDB();
        void UserUpdateCurrency(Currency currency, int newVal);
    }
}