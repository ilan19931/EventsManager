using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EventsManagerLogic.Classes
{
    public class AppSettings
    {
        public bool RememberMe { get; set; }
        public string AccessKey { get; set; }

        private string m_FilePath = @"D:\AppSettings.xml";

        public AppSettings()
        {
            this.AccessKey = string.Empty;
            this.RememberMe = false;
        }

        public void SaveSettings()
        {
            if (!File.Exists(m_FilePath))
            {
                File.Create(m_FilePath).Dispose();
            }

            if (RememberMe == false)
            {
                AccessKey = string.Empty;
            }

            using (Stream stream = new FileStream(m_FilePath, FileMode.Truncate))
            {
                XmlSerializer xmlSettings = new XmlSerializer(typeof(AppSettings));
                xmlSettings.Serialize(stream, this);
            }
        }
        public AppSettings LoadSettings()
        {
            if (File.Exists(m_FilePath))
            {
                using (Stream stream = new FileStream(m_FilePath, FileMode.Open))
                {
                    AppSettings loadedSettings = new AppSettings();
                    try
                    {
                        XmlSerializer xmlSettings = new XmlSerializer(this.GetType());
                        loadedSettings = xmlSettings.Deserialize(stream) as AppSettings;
                    }
                    catch(Exception e)
                    {
                        stream.Dispose();
                        File.Delete(m_FilePath);
                    }

                    return loadedSettings;
                }
            }

            return new AppSettings();
        }
    }
}
