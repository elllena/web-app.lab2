using System.ComponentModel.DataAnnotations;

namespace Lab2.Entities
{
    public enum KnifeType
    {
        [Display(Name ="knife")]
        Knife,
        [Display(Name = "dagger")]
        Dagger,
        [Display(Name = "saber")]
        Saber,
        [Display(Name = "katana")]
        Katana,
    }
}
