using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFifaApp.Model
{
    public class Sound
    {
        //Getters + setters for for sound as a whole, e.g have name+cat+audio+image all in one 
        public String Name { get; set; }
        public SoundCatagory Catagory { get; set; }
        public String AudioFile { get; set; }
        public String ImageFile { get; set; }

        public Sound(string name, SoundCatagory catagory)
        {
            Name = name;
            Catagory = catagory;

            //Makes the sound easier to get
            AudioFile = String.Format("/Assets/Sounds/{0}/{1}.wav", catagory,name);
            ImageFile = String.Format("/Assets/Images/{0}/{1}.png", catagory, name);
        }
    }

    //Enums for catagorys
    public enum SoundCatagory
    {
        Bad,
        Fifa,
        Good
    }
}
