namespace GW_AdminConfig
{
    public class LaneDeviceModel
    {
        public int DeviceId { get; set; } = 0;
        public string LaneId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string IPAddress { get; set; } = string.Empty;
        public int Port { get; set; }
        public string ExtraField1 { get; set; } = string.Empty;
        public string ExtraField2 { get; set; } = string.Empty;
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
