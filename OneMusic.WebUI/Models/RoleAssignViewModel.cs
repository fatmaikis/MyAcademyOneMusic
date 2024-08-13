namespace OneMusic.WebUI.Models
{
    public class RoleAssignViewModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool RoleExist { get; set; } // rol kişiye atanmısmı diye kontrol etmek amaçlı yapıldı
    }
}
