namespace tech_task.Models
{
    public class ConfigurationItem
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string? Value { get; set; }

        public int? ParentId { get; set; }
        public ConfigurationItem? Parent { get; set; }

    }
}