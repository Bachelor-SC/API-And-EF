using ScSoMeAPI.Models.User;
using System.Data.Entity;
using System.Data.SqlClient;

namespace ScSoMeAPI.UserDB
{
    public class DBContext
    {
        //Sql information
        private string connectionString;
        private SqlConnection sqlConnection;
        private SqlCommand command;
        private SqlDataReader reader;

        private string dbString = "SCTest";

        public DBContext()
        {
            connectionString = $"Data Source=.;Initial Catalog={dbString}; Integrated Security=True;";

        }

        public async Task<User> GetValidatedUser(string username, string password)
        {
            User user = new User();
            string hashPassword = PasswordHashing.GetHashString(password);

            try
            {
                sqlConnection = new SqlConnection(connectionString);

                string sql = $"select [Username], [HashedPassword], [SubscriptionLevel] from [dbo].[Users] " +
                $"where [username] ='{username}' and [HashedPassword] ='{hashPassword}'";

                sqlConnection.Open();

                command = new SqlCommand(sql, sqlConnection);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    user.username = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0);
                    user.password = reader.IsDBNull(1) ? "Unknown" : reader.GetString(1);
                    user.subscriptionLevel = reader.IsDBNull(2) ? 1 : reader.GetInt32(2); //Default would be free user
                }

                sqlConnection.Close();

                return user;

            }
            catch (Exception e)
            {
                return user;
            }
        }


    }



}
