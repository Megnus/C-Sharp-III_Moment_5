using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramGenerator
{
    class XmlFileHandler
    {
        public static void WriteXML(DiagramData diagramData)
        {
            Microsoft.Win32.SaveFileDialog dl1 = new Microsoft.Win32.SaveFileDialog();
            dl1.DefaultExt = ".xml";
            dl1.Filter = "XML file (.xml)|*.xml";
            Nullable<bool> result = dl1.ShowDialog();

            if ((bool)result)
            {
                string filename = dl1.FileName;
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(DiagramData));
                System.IO.FileStream file = System.IO.File.Create(filename);
                writer.Serialize(file, diagramData);
                file.Close();
            }
        }

        public static DiagramData ReadXML()
        {
            DiagramData diagramData = null;
            Microsoft.Win32.OpenFileDialog dl1 = new Microsoft.Win32.OpenFileDialog();
            dl1.DefaultExt = ".xml";
            dl1.Filter = "XML file (.xml)|*.xml";
            Nullable<bool> result = dl1.ShowDialog();

            if (result == true)
            {
                string filename = dl1.FileName;
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(DiagramData));
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                diagramData = (DiagramData)reader.Deserialize(file);
                file.Close();
            }

            return diagramData;
        }
    }
}
