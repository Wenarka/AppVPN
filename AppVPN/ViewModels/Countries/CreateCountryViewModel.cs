using System.ComponentModel.DataAnnotations;

namespace AppVPN.ViewModels.Countries
{
    public class CreateCountryViewModel
    {
        [Required(ErrorMessage = "Выберите сервер")]
        [Display(Name = "Сервер")]
        public string CountryServer { get; set; }
    }
}
