using System.ComponentModel.DataAnnotations;

namespace SaaaNR.Models
{
  public class Event
  {
    [Required]
    public int id { get; set; }
    [Required]
    public string eventName { get; set; }
    [Required]
    public string startDate { get; set; }
    [Required]
    public string endDate { get; set; }
    public string venue { get; set; }
    public string website { get; set; }
    [Required]
    public string contactName { get; set; }
    [Required]
    public string contactEmail { get; set; }
    public string classes { get; set; }
  }
}