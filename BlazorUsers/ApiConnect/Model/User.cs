namespace BlazorUsers.ApiConnect.Model
{
    public class UserDataShort
    {
        public int id_User { get; set; }
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserData
    {
        public bool status { get; set; }
        public UserDataContainer data { get; set; }
    }

    public class UserDataContainer
    {
        public List<UserDataShort> users { get; set; }
    }

    public class ReqDataUser
    {
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthorizationUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserAddData
    {
        public bool status { get; set; }
    }
}
