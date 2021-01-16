using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SMF
{
    class WeaponBuilder
    {
        T ReadXMLData<T>(int id)
        {
            T fileData;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (Stream reader = new FileStream("assets/weapons/" + id + ".xml", FileMode.Open))
            {
                fileData = (T)serializer.Deserialize(reader);
            }
            return fileData;
        }

        public IWeapon CreateWeapon(int id, Fish owner = null)
        {
            if (File.Exists("assets/weapons/" + id + ".xml"))
            {
                string type = XDocument.Load("assets/weapons/" + id + ".xml").Root.Element("Type").Value;
                if (type == "Melee")
                {
                    return new MeleeWeapon(ReadXMLData<MeleeWeapon.WeaponFileData>(id), owner);
                }
                else if (type == "Ranged")
                {
                    return new RangedWeapon(ReadXMLData<RangedWeapon.WeaponFileData>(id), owner);
                }
                else
                    return null;
            }
            else
            {
                Console.WriteLine("Wrong file!");
                return null;
            }
        }
    }
}
