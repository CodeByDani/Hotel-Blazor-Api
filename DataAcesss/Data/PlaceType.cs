using System.ComponentModel.DataAnnotations;

namespace DataAcesss.Data
{
    public enum PlaceType
    {
        [Display(Name = "هتل")]
        Hotel,
        [Display(Name = "ویلا")]
        Villa,
        [Display(Name = "آپارتمان")]
        Apartment,
        [Display(Name = "باغ")]
        Garden,
    }
}