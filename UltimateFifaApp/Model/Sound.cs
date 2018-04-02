using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFifaApp.Model
{
    class Sound
    {
        public String Name { get; set; }
        public SoundCatagory Catagory { get; set; }
        public String AudioFile { get; set; }
        public String ImageFile { get; set; }
    }

    public enum SoundCatagory
    {
        Bad,
        Fifa Comm,
        Good
    }
}
