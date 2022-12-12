namespace ScSoMeAPI.Models.UserData
{
    public class UserInfoDB
    {
        //Clean UserInfo class without child objects, since that fucks up in EF Core for me
        public string Username { get; set; }

        public string Description { get; set; }

        public string ProfilePicture { get; set; }

        public string CoverPicture { get; set; }


        public string Phonenumber { get; set; }

        public string Email { get; set; }


    }
}
