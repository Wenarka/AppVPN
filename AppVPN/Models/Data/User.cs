using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AppVPN.Models.Data
{
    public class User : IdentityUser
    {
        //Дополнительные поля для каждого пользователя
        //Для преподавателя могут понадобиться данные с ФИО

        //сообщение об ошибке при валидации на стороне клиента
        [Required(ErrorMessage = "Введите ключ")]
        [Display(Name = "Ключ")]
        public required string AccessKey { get; set; }    
    }
}
