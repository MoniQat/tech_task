namespace tech_task.Models
{
    public class Configuration
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int RootElementId { get; set; }
        public ConfigurationItem RootElement { get; set; }
    }
}