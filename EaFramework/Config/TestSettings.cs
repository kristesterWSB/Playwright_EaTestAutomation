using EaFramework.driver;

namespace EaFramework.Config
{
    public class TestSettings
    {
        public float? Timeout = PlaywrightDriverInitializer.DEFAULT_TIMEOUT;
        public string[] Args { get; set; }
        public bool Headless { get; set; }
        public bool DevTools { get; set; }
        public int SlowMo { get; set;}
        public DriverType DriverType { get; set; }
        public string ApplicationUrl { get; set; }
    }

    public enum DriverType
    {
        Chromium,
        Firefox,
        Edge,
        Chrome,
        WebKit
    }
}
