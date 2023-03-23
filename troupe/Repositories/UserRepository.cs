using troupe.Models;
using troupe.Utils;

namespace troupe.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration) { }

    public List<User> GetAllUsers()
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
            select  u.id, 
		            u.uid,
		            u.name,
		            u.email,
		            u.photo,
		            u.phone,
		            u.bio
            from users u";

                var reader = cmd.ExecuteReader();

                var users = new List<User>();
                while (reader.Read())
                {
                    users.Add(new User()
                    {
                        Id = DbUtils.GetInt(reader, "id"),
                        Uid = DbUtils.GetString(reader, "uid"),
                        Name = DbUtils.GetString(reader, "name"),
                        Email = DbUtils.GetString(reader, "email"),
                        Photo = DbUtils.GetString(reader, "photo"),
                        Phone = DbUtils.GetString(reader, "phone"),
                        Bio = DbUtils.GetString(reader, "bio")
                    });
                }

                reader.Close();

                return users;
            }
        }
    }
}
