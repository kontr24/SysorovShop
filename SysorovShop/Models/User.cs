//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SysorovShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Введите Email!")]
        //[RegularExpression(@"^([\w-\.+)@((\[[0-9]]{1,3}\.[0-9]]{1,3}\.[0-9]]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Пожалуйста, введите действительный адрес электронной почты!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите логин!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Пожалуйста, повторите пароль!")]
        //[DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string StatusID { get; set; }


        public System.Web.Mvc.SelectList Status { get; set; }
    }
}
