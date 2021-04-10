using System;
using System.Threading.Tasks;

namespace alexshko.fishingworld.Core.DB
{
    public interface UserDataBase
    {
        async Task ReadUserData(Action<User> actionAfterRead);
        void ReadUserCreateEmptyIfNotExistInDB();
        void UserUpdateCurrency(Currency currency, int newVal);
    }
}