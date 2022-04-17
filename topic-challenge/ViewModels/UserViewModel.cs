using System.ComponentModel.DataAnnotations;

namespace topic_challenge.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage= "O campo username é obrigatório!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo confirmar senha é obrigatório!")]
        [Compare("Password", ErrorMessage = "As senhas não correspondem, digite novamente!")]
        public string ConfirmPassword { get; set; }
        public bool Valid { get; set; } = true;
    }
}
