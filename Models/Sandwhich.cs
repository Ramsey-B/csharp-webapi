using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using smoothie_shack.Interfaces;

namespace smoothie_shack.Models
{
  public class Sandwhich : IPurchasable
  {
    public int Id {get; set;}
    [Required]
    [Range(1, 100)]
    public decimal Price { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(100)]
    public string Name { get; set; }
    public string Description {get; set;}
  }
}