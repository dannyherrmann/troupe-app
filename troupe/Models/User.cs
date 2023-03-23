using System.ComponentModel.DataAnnotations;

namespace troupe.Models;
public class User
{
    public int Id { get; set; }

    [Required]
    public string Uid { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    public string Photo { get; set; }

    public string Bio { get; set; }

    public string Phone { get; set; }

}
