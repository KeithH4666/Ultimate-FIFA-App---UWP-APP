using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UltimateFifaApp.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UltimateFifaApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Sound> Sounds;
        private List<MenuItem> MenuItems;
        public Recorder recorder = new Recorder();

        public MainPage()
        {
            this.InitializeComponent();
            
            Sounds = new ObservableCollection<Sound>();
            //Populate sounds as soon as app loads
            SoundManager.getAllSounds(Sounds);

            MenuItems = new List<MenuItem>();

            //Add each icon with correspondong catagory to menuItems
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icon/bad.png", Category = SoundCatagory.Bad });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icon/fifa.png", Category = SoundCatagory.Fifa });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icon/good.png", Category = SoundCatagory.Good });
        }

        //For the side menu button 
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //Pass all sounds back (Incase youve passed in catogory sounds) 
            SoundManager.getAllSounds(Sounds);
            //Set text above images to All sounds
            CategoryTextBlock.Text = "All Sounds";
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;
            //Set text above images to whatever category ir is
            CategoryTextBlock.Text = menuItem.Category.ToString();
            //Pass all sounds and category to get categoty songs
            SoundManager.GetSoundsByCategory(Sounds, menuItem.Category);
        }

        private void SoundGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Play what sound is clicked (Tapped)
            var sound = (Sound)e.ClickedItem;
            MyMediaElement.Source = new Uri(this.BaseUri, sound.AudioFile);

        }

        private void Record_Click(object sender, RoutedEventArgs e)
        {
            //If true
            if (recorder.Recording)
            {
                recorder.Stop();
                //Change icon
                Record.Icon = new SymbolIcon(Symbol.Memo);
            }
            else
            {
                recorder.Record();

                //Change icons
                Record.Icon = new SymbolIcon(Symbol.Microphone);
                Play.Icon = new SymbolIcon(Symbol.Play);

            }
        }

        private async void Play_Click(object sender, RoutedEventArgs e)
        {
            await recorder.Play(Dispatcher);

            if (recorder.Playing)
            {
                Play.Icon = new SymbolIcon(Symbol.Pause);
            }
            
        }
    }
}
