using NAudio.Wave;
using System;

namespace NAudio.Wave
{
    /// <summary>
    /// An interface meant to be implemented by audio objects
    /// </summary>
    public interface ISong : IDisposable
    {
        /// <summary>
        /// Gets the URI of the song.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        string URI { get; }

        /// <summary>
        /// Gets the play state of the songs.
        /// </summary>
        /// <value>
        /// The value of the play state.
        /// </value>
        PlaybackState PlayState { get; }

        /// <summary>
        /// Occurs when [playback stopped].
        /// </summary>
        event EventHandler<StoppedEventArgs> PlaybackStopped;

        /// <summary>
        /// Plays this song.
        /// </summary>
        void Play();
        /// <summary>
        /// Play and seek in this song.
        /// </summary>
        /// <param name="seek">The amount to seek in milliseconds.</param>
        void Play(int seek);
        /// <summary>
        ///  暂停这首歌。
        /// </summary>
        void Pause();
        /// <summary>
        /// 暂停这首歌，将位置设置为 0。不释放任何资源。
        /// </summary>
        void Stop();
        /// <summary>
        /// 从文件的开头算起的指定毫秒的目的。
        /// </summary>
        /// <param name="milliseconds">The amount of milliseconds.</param>
        void Seek(int milliseconds);

        //TODO: Add a length property that ought to return the amount of bytes in the song, something like this:
        //long Length { get; }
    }
}
