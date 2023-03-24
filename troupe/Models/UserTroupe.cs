namespace troupe.Models;

public class UserTroupe
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TroupeId { get; set; }

    public bool IsLeader { get; set; }

    public int UserTypeId { get; set; }
}
