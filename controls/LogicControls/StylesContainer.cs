using System.IO;
using System.Xml.Serialization;

namespace SMWControlibControls.LogicControls
{
    public class StylesContainer
    {
        public int[] ForeColorRed;
        public int[] ForeColorGreen;
        public int[] ForeColorBlue;

        public string Font;
        public int BackColorRed, BackColorGreen, BackColorBlue;
        public int MarginBackColorRed, MarginBackColorGreen, MarginBackColorBlue;

        public int Size;
        public bool Bold;

        public StylesContainer()
        {

        }

        public void Serialize(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(StylesContainer));
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            FileStream fs = null;
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            fs = new FileStream(path, FileMode.Create);
            fs.Close();

            fs = new FileStream(path, FileMode.Open);
            serializer.Serialize(fs, this);
            fs.Close();
        }

        public static StylesContainer Deserialize(string path)
        {
            XmlSerializer serializer = new XmlSerializer(
                typeof(StylesContainer));
            FileStream fs = new FileStream(path, FileMode.Open);
            StylesContainer obj =
                (StylesContainer)serializer.Deserialize(fs);
            fs.Close();
            return obj;
        }
    }
}
