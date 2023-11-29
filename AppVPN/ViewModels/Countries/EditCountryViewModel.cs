using System.ComponentModel.DataAnnotations;

namespace AppVPN.ViewModels.Countries
{
    public class EditCountryViewModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Выберите сервер")]
        [Display(Name = "Сервер")]
        public string CountryServer { get; set; }
    }
}
