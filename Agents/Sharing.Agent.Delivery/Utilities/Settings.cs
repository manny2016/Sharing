



namespace Sharing.Agent.Delivery
{
    using System.IO;
    using Sharing.Core;
    public class Settings
    {
        private static Settings settings;
        private static object lockObject = new object();
        const string configurationFile = "settings.json";
        public static Settings Create()
        {
            lock (lockObject)
            {
                if (settings == null)
                {
                    lock (lockObject)
                    {
                        if (File.Exists(configurationFile) == false)
                        {
                            settings = new Settings()
                            {
                                API = Constants.API,
                                BillingPrinter = string.Empty,
                                OrderCodePrinter = string.Empty,
                                ShopName = "柠檬工坊东坡里店",                                 
                                Autoprint = true
                            };
                            settings.Save();
                        }
                        else
                        {
                            settings = File.ReadAllText(configurationFile).DeserializeToObject<Settings>();
                        }
                    }
                }
            }
            return settings;
        }
        public string BillingPrinter { get; set; }
        public string OrderCodePrinter { get; set; }
        public string API { get; set; }
        public bool Autoprint { get; set; }
        public string ShopName { get; set; }
        
        public void Save()
        {
            if (File.Exists(configurationFile))
            {
                File.Delete(configurationFile);
            }
            using (var stream = new FileStream(configurationFile, FileMode.CreateNew, FileAccess.ReadWrite))
            {
                var buffers = System.Text.UTF8Encoding.Default.GetBytes(this.SerializeToJson());
                stream.Write(buffers, 0, buffers.Length);
                stream.Flush();
            }
            settings = this.MemberwiseClone() as Settings;
        }
    }
}
