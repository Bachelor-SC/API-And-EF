using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScSoMeAPI.Models;
using ScSoMeAPI.Models.UserData;
using System.Reflection.Emit;

namespace ScSoMeAPI.Data
{
    public class UserInfoHandler
    {

        //EF Core does not allow for easy inheritance or child classes for models
        //UserInfo has Child objects like Location and SocialMedia, which, upon using Entity.Add()
        //creates internal foreign keys, which won't allow to save the object
        //As the UserInfo class from tne blazor frontend doesn't need to be split, and even extends to the User class

        #region CRUD UserInfo
        //The PostCreateUserInfo splits up the true UserInfo class into sub classes, that can then be added
        //to the database
        public async Task<UserInfo> PostCreateUserInfo(UserInfo user)
        {
            using BachelordbContext ctx = new BachelordbContext();
            try
            {
                User newUser;
                newUser = await PostCreateUser(BuildUser(user));

                var newUserInfo = ctx.UserInfo.Add(UserInfoDBBuilder(user));
                var newSoMe = ctx.SocialMedia.Add(BuildSoMe(user));
                var newExternalLink = ctx.ExternalLink.Add(BuildExLink(user));
                var newUserLocation = ctx.UserLocations.Add(BuildLocation(user));

                await ctx.SaveChangesAsync();



            }
            catch (Exception e)
            {

            }


            return user;
        }

        //Method for Creating new User: Called in (PostCreateUserInfo)
        public async Task<User> PostCreateUser(User user)
        {
            using BachelordbContext ctx = new BachelordbContext();
            try
            {
                var newUser = ctx.Users.Add(user);
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }
            return user;
        }

        //Retrieves UserInfo 
        public async Task<UserInfo> GetUserInfo(string username)
        {
            UserInfo userInfo = new UserInfo();

            using BachelordbContext ctx = new BachelordbContext();
            try
            {
                userInfo.Username = GetUserInfoDB(username, ctx).Username;
                userInfo.SocialMedia = GetSoMe(username, ctx);
                userInfo.Description = GetUserInfoDB(username, ctx).Description;
                userInfo.ProfilePicture = GetUserInfoDB(username, ctx).ProfilePicture;
                userInfo.CoverPicture = GetUserInfoDB(username, ctx).CoverPicture;
                userInfo.Location = GetLocation(username, ctx);
                userInfo.Phonenumber = GetUserInfoDB(username, ctx).Phonenumber;
                userInfo.Email = GetUserInfoDB(username, ctx).Email;
                userInfo.ExternalLinks = GetExternalLink(username, ctx);
                userInfo.HashedPassword = GetUser(username, ctx).HashedPassword;
                userInfo.SubscriptionLevel = GetUser(username, ctx).SubscriptionLevel;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return userInfo;
        }

        //Parent method for patching UserInfo
        public async Task<UserInfo> PatchUserInfo(UserInfo userInfo)
        {
            using BachelordbContext ctx = new BachelordbContext();

            await PatchUser(BuildUser(userInfo), ctx);

            await PatchUserInfoDB(userInfo, ctx);
            await PatchSocialMedia(userInfo, ctx);
            await PatchExternalLinks(userInfo, ctx);
            await PatchLocation(userInfo, ctx);

            return userInfo;
        }

        //Parent method for deleting specific user
        public async Task DeleteUser(string username)
        {
            using BachelordbContext ctx = new BachelordbContext();

            try
            {
                ctx.Users.Remove(GetUser(username, ctx));
                ctx.UserInfo.Remove(GetUserInfoDB(username, ctx));
                ctx.SocialMedia.Remove(GetSoMe(username, ctx));
                ctx.ExternalLink.Remove(GetExternalLink(username, ctx));
                ctx.UserLocations.Remove(GetLocation(username, ctx));

                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

        }
        #endregion

        #region SocialMedia Handler
        //Retrieves SocialMedia for specific user
        private SocialMedia GetSoMe(string username, BachelordbContext ctx)
        {
            //using BachelordbContext ctx = new BachelordbContext();
            SocialMedia soMe = new SocialMedia();

            try
            {
                soMe = ctx.SocialMedia.First(a => a.Username == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return soMe;
        }

        private async Task PatchSocialMedia(UserInfo userInfo, BachelordbContext ctx)
        {
            try
            {
                var result = GetSoMe(userInfo.Username, ctx);
                if (result != null)
                {
                    result.Username = userInfo.Username;
                    result.Facebook = userInfo.SocialMedia.Facebook;
                    result.LinkedIn = userInfo.SocialMedia.LinkedIn;
                    result.Instagram = userInfo.SocialMedia.Instagram;

                    await ctx.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        //Method for building Location sub class
        private SocialMedia BuildSoMe(UserInfo user)
        {
            SocialMedia SoMe = new SocialMedia();

            SoMe.Username = user.Username;
            SoMe.Facebook = user.SocialMedia.Facebook;
            SoMe.LinkedIn = user.SocialMedia.LinkedIn;
            SoMe.Instagram = user.SocialMedia.Instagram;

            return SoMe;
        }

        #endregion

        #region ExternalLink Handler
        //Retrieves ExternalLink for specific user
        private ExternalLink GetExternalLink(string username, BachelordbContext ctx)
        {
            //using BachelordbContext ctx = new BachelordbContext();
            ExternalLink exLink = new ExternalLink();

            try
            {
                exLink = ctx.ExternalLink.First(a => a.Username == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return exLink;
        }

        private async Task PatchExternalLinks(UserInfo userInfo, BachelordbContext ctx)
        {
            try
            {
                var result = GetExternalLink(userInfo.Username, ctx);
                if (result != null)
                {
                    result.Username = userInfo.Username;
                    result.Link = userInfo.ExternalLinks.Link;



                    await ctx.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        //Method for building Location sub class
        private ExternalLink BuildExLink(UserInfo user)
        {
            ExternalLink exLink = new ExternalLink();
            exLink.Username = user.Username;
            exLink.Link = user.ExternalLinks.Link;

            return exLink;
        }
        #endregion

        #region UserInfoDB Handler
        //Retrieves UserInfoDB for specific user
        private UserInfoDB GetUserInfoDB(string username, BachelordbContext ctx)
        {
            //using BachelordbContext ctx = new BachelordbContext();
            UserInfoDB userDB = new UserInfoDB();

            try
            {
                userDB = ctx.UserInfo.First(a => a.Username == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return userDB;
        }

        private async Task PatchUserInfoDB(UserInfo userInfo, BachelordbContext ctx)
        {

            try
            {
                var result = GetUserInfoDB(userInfo.Username, ctx);
                if (result != null)
                {
                    result.Username = userInfo.Username;
                    result.Description = userInfo.Description;
                    result.ProfilePicture = userInfo.ProfilePicture;
                    result.CoverPicture = userInfo.CoverPicture;
                    result.Phonenumber = userInfo.Phonenumber;
                    result.Email = userInfo.Email;

                    await ctx.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        //Method for building UserInfoDB sub class
        private UserInfoDB UserInfoDBBuilder(UserInfo user)
        {
            UserInfoDB temp = new UserInfoDB();
            temp.Username = user.Username;
            temp.Description = user.Description;
            temp.ProfilePicture = user.ProfilePicture;
            temp.CoverPicture = user.CoverPicture;
            temp.Phonenumber = user.Phonenumber;
            temp.Email = user.Email;

            return temp;
        }



        #endregion

        #region Location Handler

        //Retrieves Location for specific user
        private Location GetLocation(string username, BachelordbContext ctx)
        {
            //using BachelordbContext ctx = new BachelordbContext();
            Location location = new Location();

            try
            {
                location = ctx.UserLocations.First(a => a.Username == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return location;
        }

        private async Task PatchLocation(UserInfo userInfo, BachelordbContext ctx)
        {
            try
            {
                var result = GetLocation(userInfo.Username, ctx);
                if (result != null)
                {
                    result.Username = userInfo.Username;
                    result.Address = userInfo.Location.Address;
                    result.PostalCode = userInfo.Location.PostalCode;
                    result.City = userInfo.Location.City;


                    await ctx.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        //Method for building Location sub class
        private Location BuildLocation(UserInfo user)
        {
            Location newLocation = new Location();

            newLocation.Username = user.Username;
            newLocation.Address = user.Location.Address;
            newLocation.PostalCode = user.Location.PostalCode;
            newLocation.City = user.Location.City;

            return newLocation;
        }

        #endregion

        #region User Handler
        private User GetUser(string username, BachelordbContext ctx)
        {
            User user = new User();

            try
            {
                user = ctx.Users.First(a => a.Username == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return user;
        }

        private async Task PatchUser(User user, BachelordbContext ctx)
        {

            try
            {
                var result = GetUser(user.Username, ctx);
                if (result != null)
                {
                    result.Username = user.Username;
                    result.HashedPassword = user.HashedPassword;
                    result.SubscriptionLevel = user.SubscriptionLevel;


                    await ctx.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private User BuildUser(UserInfo user)
        {
            User newUser = new User();

            newUser.Username = user.Username;
            newUser.HashedPassword = user.HashedPassword;
            newUser.SubscriptionLevel = user.SubscriptionLevel;
            

            return newUser;
        }



        #endregion





    }
}
