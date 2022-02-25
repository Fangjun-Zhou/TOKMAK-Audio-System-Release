using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace AudioSystem.Runtime
{
    [System.Serializable]
    public class AudioDict : SerializableDictionary<string, AudioConfig>
    {
        
    }

    public class UIAudioController : UnityEngine.MonoBehaviour
    {
        #region Public Field

        /// <summary>
        /// The dictionary for different audio source.
        /// </summary>
        public AudioDict audioDict;

        /// <summary>
        /// The prefab to instantiate the audio player.
        /// </summary>
        [Required()]
        public AudioPlayer playerPrefab;

        /// <summary>
        /// The parent transform for audio instantiation.
        /// </summary>
        [Required()]
        public Transform audioParent;

        #endregion

        #region Private Field

        private Dictionary<string, AudioPlayer> _runtimePlayer = new Dictionary<string, AudioPlayer>();

        #endregion

        private void Awake()
        {
            foreach (string audioDictKey in audioDict.Keys)
            {
                playerPrefab.audioConfig = audioDict[audioDictKey];
                AudioPlayer instance = Instantiate(playerPrefab, audioParent);
                instance.gameObject.name = audioDictKey;
                
                _runtimePlayer.Add(audioDictKey, instance);
            }
        }

        #region Public Method

        /// <summary>
        /// Play the runtime audio.
        /// </summary>
        /// <param name="targetAudio">target audio name.</param>
        public void PlayAudio(string targetAudio)
        {
            if (!_runtimePlayer.ContainsKey(targetAudio))
            {
                Debug.LogError($"No runtime audio named {targetAudio}");
            }
            
            _runtimePlayer[targetAudio].Play();
        }

        #endregion
    }
}