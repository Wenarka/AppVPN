using System.ComponentModel.DataAnnotations;

namespace AppVPN.ViewModels.Tarifs
{
    public class CreateTarifViewModel
    {
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
