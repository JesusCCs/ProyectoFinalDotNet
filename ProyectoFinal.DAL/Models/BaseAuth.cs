namespace ProyectoFinal.DAL.Models
{
    public class BaseAuth : Base
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public Password Password { get; set; }
    }
}