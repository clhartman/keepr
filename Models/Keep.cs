using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class Keep
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public string UserId { get; set; }
    public string Img { get; set; }
    public bool IsPrivate { get; set; } = false;
    public int Views { get; set; } = 0;
    public int Shares { get; set; } = 0;
    public int Keeps { get; set; } = 0;

    // public Keep()
    // {
    //   this.Views = 0;
    //   this.Shares = 0;
    //   this.Keeps = 0;
    //   this.IsPrivate = false;
    // }

  }
}