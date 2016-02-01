using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author:          Magnus Sundström
 * Creation Date:   2015-02-01
 * File:            XmlFileHandler.cs
 */

namespace DiagramGenerator
{
    /// <summary>
    /// Class for handeling saving and opening the plot data.
    /// </summary>
    class XmlFileHandler
    {
        /// <summary>
        /// Public method for saving the data to a xml-file.
        /// </summary>
        /// <param name="diagramData">This holds the data of the coordinatesystem and the plots.</param>
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

        /// <summary>
        /// Public method for opening the xml-file with a file opener dialog.
        /// </summary>
        /// <returns>The coordinatesystem and the plots as one object.</returns>
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