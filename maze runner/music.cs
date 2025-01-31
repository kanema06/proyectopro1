using System;
using System.IO;
using NAudio.Wave;

public class BackgroundMusic
{
    private IWavePlayer waveOut;
    private AudioFileReader[] audioFileReaders;

    public BackgroundMusic(string[] audioFilePaths)
    {
        waveOut = new WaveOutEvent();
        audioFileReaders = new AudioFileReader[audioFilePaths.Length];
        for (int i = 0; i < audioFilePaths.Length; i++)
        {
                audioFileReaders[i] = new AudioFileReader(audioFilePaths[i]);
        }
    }
    public void Play(int trackNumber)
    {

        waveOut.Init(audioFileReaders[trackNumber]);
        waveOut.Play();
    }

    public void Stop()
    {
        waveOut.Stop();
        foreach (var reader in audioFileReaders)
        {
            reader.Dispose();
        }
        waveOut.Dispose();
    }
}
