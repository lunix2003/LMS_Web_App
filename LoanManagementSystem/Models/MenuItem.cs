namespace LoanManagementSystem.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public List<MenuItem> SubItems { get; set; }  // For submenus
    }
}
