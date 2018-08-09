using System.IO;
using System.Xml.Serialization;

namespace SMWControlibControls.GraphicsControls
{
    public class SpriteGridSettingsContainer
    {
        public int GridColorR, GridColorG, GridColorB;
        public int CenterSquareColorR, CenterSquareColorG, CenterSquareColorB;
        public int SelectionRectangleColorR, SelectionRectangleColorG, SelectionRectangleColorB;
        public int SelectedTilesColorR, SelectedTilesColorG, SelectedTilesColorB;
        public bool EnableCenterSquare;
        public int GridType;
        public SpriteGridSettingsContainer()
        {
        }

        public void Serialize(string path)
        {
            XmlSerializer serializer = new XmlSerializer(
                typeof(SpriteGridSettingsContainer));
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

        public static SpriteGridSettingsContainer Deserialize(string path)
        {
            XmlSerializer serializer = new XmlSerializer(
                typeof(SpriteGridSettingsContainer));
            FileStream fs = new FileStream(path, FileMode.Open);
            SpriteGridSettingsContainer obj = 
                (SpriteGridSettingsContainer)serializer.Deserialize(fs);
            fs.Close();
            return obj;
        }
    }
}
