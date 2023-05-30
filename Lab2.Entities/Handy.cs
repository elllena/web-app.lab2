using System.ComponentModel.DataAnnotations;

namespace Lab2.Entities
{
    public enum Handy
    {
        [Display(Name ="oneHanded")]
        OneHanded,
        [Display(Name = "twoHanded")]
        TwoHanded,
    }
}
