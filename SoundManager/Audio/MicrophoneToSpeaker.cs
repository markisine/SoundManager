using System;
using NAudio.Wave;

namespace SoundManager.Audio;

public class MicrophoneToSpeaker
{
    private readonly WaveIn _waveIn;
    private readonly BufferedWaveProvider _waveProvider;
    private readonly VolumeWaveProvider16 _volumeWaveProvider;
    private readonly WaveOut _waveOut;
    private readonly float _volume = 1.0f;

    public MicrophoneToSpeaker()
    {
        // MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
        // MMDevice defaultInputDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);

        _waveIn = new WaveIn();
        _waveIn.DeviceNumber = 0;
        _waveIn.WaveFormat = new WaveFormat(44100, 1);
        _waveIn.DataAvailable += WaveIn_DataAvailable;
        _waveIn.StartRecording();

        _waveProvider = new BufferedWaveProvider(_waveIn.WaveFormat)
        {
            BufferDuration = TimeSpan.FromSeconds(5)
        };

        _volumeWaveProvider = new VolumeWaveProvider16(_waveProvider)
        {
            Volume = _volume
        };

        _waveOut = new WaveOut();
        _waveOut.DeviceNumber = 0;
        _waveOut.Init(_volumeWaveProvider);
    }

    public void Run()
    {
        _waveOut.Play();
    }
    
    private void WaveIn_DataAvailable(object? sender, WaveInEventArgs e)
    {
        _waveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
    }
}