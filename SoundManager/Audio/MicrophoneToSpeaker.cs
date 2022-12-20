using System.IO;
using System.Threading.Tasks;

namespace SoundManager.Audio;

using System;
using System.IO;
using NAudio.CoreAudioApi;
using NAudio.Wave;

    class MicrophoneToSpeaker
    {
        private WaveIn waveIn;
        
        public void Run()
        {
            // Den Standard-Eingabe-Endpunkt abrufen
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            MMDevice defaultInputDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);

            // Einen WaveIn-Streamer mit dem Standard-Eingabe-Endpunkt erstellen
            waveIn = new WaveIn();
            waveIn.DeviceNumber = 0;
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.StartRecording();
            

            // Aufnahme beenden und WaveIn-Streamer schließen
            // waveIn.StopRecording();
            // waveIn.Dispose();
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            // Den Standard-Ausgabe-Endpunkt abrufen
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            MMDevice defaultOutputDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);

            // Einen WaveOut-Player mit dem Standard-Ausgabe-Endpunkt erstellen
            WaveOut waveOut = new WaveOut();
            waveOut.DesiredLatency = 1;
            waveOut.DeviceNumber = 0;
            waveOut.Init(new RawSourceWaveStream(new MemoryStream(e.Buffer), waveIn.WaveFormat));

            // Wiedergabe starten
            waveOut.Play();
        }
    }

