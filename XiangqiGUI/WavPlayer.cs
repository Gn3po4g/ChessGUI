using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace XiangqiGUI;

public static class WavPlayer
{
    private static readonly WaveOutEvent outputDevice;
    private static readonly MixingSampleProvider mixer;
    private static readonly CachedSound captureWave = new("Sounds/Capture.wav");
    private static readonly CachedSound moveWave = new("Sounds/Move.wav");

    static WavPlayer()
    {
        outputDevice = new WaveOutEvent();
        mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(11025, 1))
        {
            ReadFully = true,
        };
        outputDevice.Init(mixer);
        outputDevice.Play();
    }

    public static void Capture()
    {
        var provider = new CachedSoundSampleProvider(captureWave);
        mixer.AddMixerInput(provider);
    }

    public static void Move()
    {
        var provider = new CachedSoundSampleProvider(moveWave);
        mixer.AddMixerInput(provider);
    }
}

class CachedSound
{
    public float[] AudioData { get; private init; }
    public WaveFormat WaveFormat { get; private init; }

    public CachedSound(string audioFileName)
    {
        using var reader = new AudioFileReader(audioFileName);
        WaveFormat = reader.WaveFormat;
        var buffer = new float[reader.WaveFormat.SampleRate * reader.WaveFormat.Channels];
        reader.Read(buffer, 0, buffer.Length);
        AudioData = buffer;
    }
}

class CachedSoundSampleProvider(CachedSound cachedSound) : ISampleProvider
{
    private long position;

    public int Read(float[] buffer, int offset, int count)
    {
        var availableSamples = (int)(cachedSound.AudioData.Length - position);
        var samplesToCopy = Math.Min(availableSamples, count);
        Array.Copy(cachedSound.AudioData, position, buffer, offset, samplesToCopy);
        position += samplesToCopy;
        return samplesToCopy;
    }

    public WaveFormat WaveFormat => cachedSound.WaveFormat;
}
