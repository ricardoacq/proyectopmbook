namespace MvcLogin.Models
{
    public class UserModel : CargaInicial
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int nLogin { get; set; }
        public string cLogin { get; set; }
    }
}