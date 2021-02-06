using System.ComponentModel.DataAnnotations;

namespace SaaaNR.Models
{
  public class Profile
  {
    public int Id { get; set; }

    // UserId is supplied by the controller.
    public string UserId { get; set; }

    [Required]
    [MinLength(1)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(1)]
    public string LastName { get; set; }

    [Required]
    [MinLength(1)]
    public string Email { get; set; }

    [MinLength(1)]
    public string Picture { get; set; }

    [MinLength(10)]
    public string Phone { get; set; }

    [MaxLength(1)]
    public int Member { get; set; }

  }
}