// ReSharper disable once CheckNamespace
namespace AudioSystem.Runtime
{
    /// <summary>
    /// The interface for audio player.
    /// Includes basic operation to play audio.
    /// </summary>
    public interface IAudioPlayer
    {
        /// <summary>
        /// If the AudioPlayer play the clip when Start()
        /// </summary>
        bool playOnstart { get; set; }
        
        /// <summary>
        /// If the AudioPlayer loop play the audio
        /// </summary>
        bool isLoop { get; set; }
        
        /// <summary>
        /// The loop time to play the clip
        /// </summary>
        float loopTime { get; set; }
        
        /// <summary>
        /// Play the audio from start
        /// </summary>
        void Play();

        /// <summary>
        /// Stop the audio
        /// </summary>
        void Stop();

        /// <summary>
        /// Resume to play the audio from the point of last Stop()
        /// </summary>
        void Resume();

    }
}