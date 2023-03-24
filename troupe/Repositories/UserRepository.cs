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
                SELECT u.id as UserId,
                        u.uid as uid,
                        u.name as UserName,
                        u.email as Email,
                        u.photo as Photo,
                        u.bio as Bio, 
                        u.phone as Phone, 
                        ut.id as UserTroupeTableId,
                        ut.troupeId as TroupeId,
                        ut.isLeader as isLeader,
                        ut.userTypeId as UserTypeId
                FROM users u
                JOIN userTroupes ut ON ut.userId = u.id
                ORDER BY u.id";

                var reader = cmd.ExecuteReader();

                var users = new List<User>();
                while (reader.Read())
                {
                    var userId = DbUtils.GetInt(reader, "UserId");
                    var user = users.Where(u => u.Id == userId).FirstOrDefault();

                    if (user == null)
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "UserId"),
                            Uid = DbUtils.GetString(reader, "uid"),
                            Name = DbUtils.GetString(reader, "UserName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Photo = DbUtils.GetString(reader, "Photo"),
                            Phone = DbUtils.GetString(reader, "Phone"),
                            Bio = DbUtils.GetString(reader, "Bio"),
                            UserTroupes = new List<UserTroupe>()
                        };
                        users.Add(user);
                    }

                    var userTroupeTableId = DbUtils.GetInt(reader, "UserTroupeTableId");
                    var existingUserTroupe = user.UserTroupes.FirstOrDefault(ut => ut.Id == userTroupeTableId);

                    if (existingUserTroupe == null)
                    {
                        user.UserTroupes.Add(new UserTroupe()
                        {
                            Id = userTroupeTableId,
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            TroupeId = DbUtils.GetInt(reader, "TroupeId"),
                            IsLeader = reader.GetBoolean(reader.GetOrdinal("isLeader")),
                            UserTypeId = DbUtils.GetInt(reader, "UserTypeId")
                        });
                    }
                }

                reader.Close();

                return users;
            }
        }
    }
}
