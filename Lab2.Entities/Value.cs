using System.ComponentModel.DataAnnotations;

namespace Lab2.Entities
{
    public enum Value
    {
        [Display(Name ="collected")]
        Collected,
        [Display(Name = "origin")]
        Origin,
    }
}
