using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppVPN.Models.Data
{
    public class Price
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]

        public short Id { get; set; }

        [Required(ErrorMessage = "Стоимость тарифа")]
        [Display(Name = "Стоимость")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "Выберите тариф")]
        [Display(Name = "Тариф с ценой")]
        public short TarifCost { get; set; }

        [Required(ErrorMessage = "Выберите дату установки")]
        [Display(Name = "Дата")]
        public DateTime DateInstal { get; set; }


        //Навигационные свойства
        [Display(Name = "")]
        [ForeignKey("TarifCostId")]
        public Tarif Tarif { get; set; }

    }
}
