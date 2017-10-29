using alfrek.api.Configuration.Interfaces;

namespace alfrek.api.Configuration
{
    public class AwsConfiguration : ICloudConfiguration
    {
        public string Key { get; set; }
        public string Secret { get; set; }

        public string ProfileName { get; set; }
    }
}