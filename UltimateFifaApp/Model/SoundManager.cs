using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFifaApp.Model
{
    public class SoundManager
    {
        //Call getSounds and store in var sound Must be public for access from MainPage
        public static void getAllSounds(ObservableCollection<Sound>sounds)
        {
            var allSounds = getSounds();
            sounds.Clear();

            allSounds.ForEach(p => sounds.Add(p));
        }

        public static void GetSoundsByCategory(ObservableCollection<Sound> sounds, SoundCatagory soundCategory)
        {
            var allSounds = getSounds();
            var filteredSounds = allSounds.Where(p => p.Catagory == soundCategory).ToList();
            sounds.Clear();
            filteredSounds.ForEach(p => sounds.Add(p));
        }

        private static List<Sound> getSounds()
        {
            var sounds = new List<Sound>();

            //Get sounds from bad folder
            sounds.Add(new Sound("Boo", SoundCatagory.Bad));
            sounds.Add(new Sound("Fail", SoundCatagory.Bad));

            //Get sounds from fifa folder
            sounds.Add(new Sound("defendingvul", SoundCatagory.Fifa));
            sounds.Add(new Sound("goalkeeperluck", SoundCatagory.Fifa));
            sounds.Add(new Sound("niceidea", SoundCatagory.Fifa));
            sounds.Add(new Sound("quickfeet", SoundCatagory.Fifa));
            sounds.Add(new Sound("save", SoundCatagory.Fifa));
            sounds.Add(new Sound("topcorner", SoundCatagory.Fifa));
            sounds.Add(new Sound("upperhandmid", SoundCatagory.Fifa));
            sounds.Add(new Sound("veryundeserv", SoundCatagory.Fifa));

            //Get sounds from Good folder
            sounds.Add(new Sound("airhorn", SoundCatagory.Good));
            sounds.Add(new Sound("clapping", SoundCatagory.Good));
            sounds.Add(new Sound("goal", SoundCatagory.Good));

            return sounds;
        }
        
    }
}
