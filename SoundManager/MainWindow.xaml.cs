using System;
using System.Windows;
using NAudio.Wave;
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
        
        MicrophoneToSpeaker microphoneToSpeaker = new MicrophoneToSpeaker();
        microphoneToSpeaker.Run();
    }

    private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        
        Text.Text = Math.Round(e.NewValue, 2).ToString();
    }
}