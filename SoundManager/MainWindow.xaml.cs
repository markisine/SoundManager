using System.Windows;
using System.Windows.Controls;
using SoundManager.Audio;

namespace SoundManager;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();

        //Hier wichtig du darfst keine Windows.Forms sachen benutzen weil wir wpf benutzen du musst stattdessen system.windows.controls benutzen
        
        Button button = new Button()
        {
            Height = 10,
            Width = 10,
        };

        
        
        Slider slider = new Slider()
        {
            Interval = 10,
            Orientation = Orientation.Vertical,
            Maximum = 12,
            Minimum = -60
        };

        Grid1.Children.Add(button);
        Grid1.Children.Add(slider);
        
        MicrophoneToSpeaker microphoneToSpeaker = new MicrophoneToSpeaker();
        microphoneToSpeaker.Run();
    }
}