using System.ComponentModel.DataAnnotations;

namespace AppVPN.ViewModels.Prices
{
    public class EditPriceViewModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Название тарифа")]
        [Display(Name = "Название")]
        public string TarifName { get; set; }

        [Required(ErrorMessage = "Выберите страну")]
        [Display(Name = "Доступные страны")]
        public short AccessCountryId { get; set; }

        [Required(ErrorMessage = "Выберите длительность")]
        [Display(Name = "Длительность")]
        public int Duration { get; set; }
    }
}
