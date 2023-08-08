namespace Accounting.Server.Authentication
{
    public class UserAccountService
    {
        private readonly List<UserAccount> users;
        public UserAccountService() 
        {
            users = new List<UserAccount>()
            {
                new UserAccount() { UserName = "admin", Password = "admin", Role = "Administrator" },
                new UserAccount() { UserName = "user", Password = "user", Role = "User" },
            };
        }

        public UserAccount? GetUserAccountByUserName(string username)
        {
            return users.FirstOrDefault(x => x.UserName == username);
        }

        public List<UserAccount> AddUserAccount(UserAccount userAccount)
        {
            users.Add(userAccount);
            return users;
        }

        public bool UserNameExist(string userName)
        {
            var account = users.Find( x => x.UserName == userName );
            return account != null;
        }
    }
}
