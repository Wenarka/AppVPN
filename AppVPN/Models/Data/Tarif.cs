using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppVPN.Models.Data
{
    public class Tarif
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]

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

        //Навигационные свойства
        [Display(Name = "")]
        [ForeignKey("AccessCountryId")]
        public Country Country { get; set; }

        public ICollection<Price> Prices { get; set; }

    }
}
