using System.ComponentModel.DataAnnotations;

namespace Keepr.Models
{
  public class Vault
  {
    public int Id { get; set; }

    // UserId is supplied by the controller.
    public string UserId { get; set; }

    [Required]
    [MinLength(1)]
    public string Name { get; set; }

    [Required]
    [MinLength(1)]
    public string Description { get; set; }
  }
}