using System.ComponentModel.DataAnnotations;

namespace Lab2.Entities
{
    public enum HandMaterial
    {
        [Display(Name ="wood")]
        Wood,
        [Display(Name ="plastic")]
        Plastic,
        [Display(Name ="metal")]
        Metal,
    }
}
