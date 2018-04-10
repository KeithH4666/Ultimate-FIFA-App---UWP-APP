using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

//Ref: https://code.msdn.microsoft.com/windowsapps/Audio-Recorder-In-Windows-2d80c2fd

public class Recorder
{
    private const string audioFilename = "audio.m4a";
    private string filename;
    private MediaCapture capture;
    private InMemoryRandomAccessStream buffer;

    //For saving to user music
    FileSavePicker file = new FileSavePicker();

    //Booleans to handle when recording or not for pause/play button on footer of app
    public bool Recording;
    public bool Playing;

    private async Task<bool> Init()
    {
        
            buffer = new InMemoryRandomAccessStream();
      
            MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings
            {
                StreamingCaptureMode = StreamingCaptureMode.Audio
            };
            capture = new MediaCapture();
            await capture.InitializeAsync(settings);
            capture.RecordLimitationExceeded += (MediaCapture sender) =>
            {
                Stop();
                throw new Exception("Exceeded Record Limitation");
            };
            capture.Failed += (MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs) =>
            {
                Recording = false;
                throw new Exception(string.Format("Code: {0}. {1}", errorEventArgs.Code, errorEventArgs.Message));
            };
       
        return true;
    }

    public async void Record()
    {
        //Wait for init method to complet
        await Init();
        //Wait for capture 
        await capture.StartRecordToStreamAsync(MediaEncodingProfile.CreateM4a(AudioEncodingQuality.Auto), buffer);
        if (Recording) throw new InvalidOperationException("cannot excute two records at the same time");
        Recording = true;
    }

    public async void Stop()
    {
        await capture.StopRecordAsync();
        Recording = false;
    }

    public async Task Play(CoreDispatcher dispatcher)
    {
        Playing = true;
        MediaElement playback = new MediaElement();
        IRandomAccessStream audio = buffer.CloneStream();
        if (audio == null) throw new ArgumentNullException("buffer");

        //Creates folder locally on user device for the recorded sound
        StorageFolder storageFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        
        if (!string.IsNullOrEmpty(filename))
        {
            StorageFile original = await storageFolder.GetFileAsync(filename);
            await original.DeleteAsync();
        }
        await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
        {
            StorageFile storageFile = await storageFolder.CreateFileAsync(audioFilename, CreationCollisionOption.GenerateUniqueName);
            filename = storageFile.Name;
            using (IRandomAccessStream fileStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                await RandomAccessStream.CopyAndCloseAsync(audio.GetInputStreamAt(0), fileStream.GetOutputStreamAt(0));
                await audio.FlushAsync();
                audio.Dispose();
            }
            IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read);
            playback.SetSource(stream, storageFile.FileType);

            playback.Play();
            
        });
    }
}