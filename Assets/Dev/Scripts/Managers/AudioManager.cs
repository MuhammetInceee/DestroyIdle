using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Scripts.Manager
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioSource source;
        private readonly Dictionary<AudioClips, AudioClip> _sounds = new();

        private void Start()
        {
            foreach (var sound in Sound.allCases) _sounds.Add(sound, Resources.Load<AudioClip>($"Sounds/{sound}"));
        }

        internal void Play(AudioClips clip)
        {
            AudioSource.PlayClipAtPoint(_sounds[clip], transform.position);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum AudioClips { destroy, stack, sell, collect, hit }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Sound
    {
        public static readonly Sound collect = new(AudioClips.collect);
        public static readonly Sound sell = new(AudioClips.sell);
        public static readonly Sound destroy = new(AudioClips.destroy);
        public static readonly Sound stack = new(AudioClips.stack);
        public static readonly Sound hit = new(AudioClips.hit);

        private readonly AudioClips clip;
        internal static readonly List<AudioClips> allCases = new() {AudioClips.destroy, AudioClips.stack, AudioClips.sell, AudioClips.collect, AudioClips.hit };
        private Sound(AudioClips clip) => this.clip = clip;
        public void Play() => AudioManager.Instance.Play(clip);
    }
}
