using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    abstract class FileData
    {
        public FileData Copy()
        {
            FileData other = (FileData)this.MemberwiseClone();
            return other;
        }

        public int ID = 0;
        public string Name = "";
        public int SizeX = 128;
        public int SizeY = 72;
    }
}
