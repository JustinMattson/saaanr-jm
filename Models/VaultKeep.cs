using System.ComponentModel.DataAnnotations;

namespace SaaaNR.Models
{
  public class VaultKeep
  {
    public int id { get; set; }

    [Required]
    public int vaultId { get; set; }

    [Required]
    public int keepId { get; set; }

    public string userId { get; set; }
  }
}