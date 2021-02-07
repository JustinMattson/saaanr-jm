using System.ComponentModel.DataAnnotations;

namespace SaaaNR.Models
{
  public class Contact
  {
    public int Id { get; set; }

    public string UserId { get; set; }
    // ^ auth0|OID ^
    // UserId is supplied by the controller.

    [Required]
    [MinLength(1)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(1)]
    public string LastName { get; set; }

    [Required]
    [MinLength(1)]
    public string Email { get; set; }
    public int Verified { get; set; }
    public string DOB { get; set; }
    public string Picture { get; set; }
    public string Phone { get; set; }
    public string Position { get; set; }
    public int Member { get; set; }
    public int Show { get; set; }
  }
}