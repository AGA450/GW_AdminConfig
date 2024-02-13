namespace GW_AdminConfig.Models
{
    public class PathConfigModel
    {
        public string Category { get; set; }=string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string ExtraField1 { get; set; } = string.Empty;
        public string ExtraField2 { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LastUpdatedBy { get; set; } = string.Empty;
        public DateTime LastUpdateTime { get; set; }
    }
}
