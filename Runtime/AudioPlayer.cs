using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioSystem.Runtime.Utils;
using NaughtyAttributes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace AudioSystem.Runtime
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        #region Public Field

        [BoxGroup("Debug")]
        [Tooltip("Show some private field if enabled.")]
        public bool debugMode;

        [BoxGroup("Audio Config")]
        public AudioConfig audioConfig;

        [BoxGroup("Sound Speed")]
        public bool simulateSoundSpeed;
        
        #endregion

        #region Serialized Private Field

        [BoxGroup("Settings")]
        [SerializeField]
        private bool _playOnStart;
        
        [BoxGroup("Settings")]
        [SerializeField]
        private bool _isLoop;
        
        [BoxGroup("Settings")]
        [SerializeField]
        private float _loopTime;

        #endregion

        #region Private Field

        /// <summary>
        /// All the AudioLayerInstance to be played in runtime
        /// </summary>
        [BoxGroup("Private Field")]
        [ShowIf("debugMode")]
        [SerializeField]
        private List<AudioLayerInstance> _audioLayerInstances;

        /// <summary>
        /// the coroutine to loop play
        /// </summary>
        private Coroutine _loopPlayCoroutine;

        private Camera mainCam;

        #endregion

        private void Awake()
        {
            InitializeAudioPlayer();

            mainCam = Camera.main;
        }

        private void Start()
        {
            // Play on start
            if (playOnstart)
            {
                Play();
            }
        }

        /// <summary>
        /// The method to initialize all the audio layers
        /// </summary>
        private void InitializeAudioPlayer()
        {
            foreach (AudioLayer layer in audioConfig.audioLayers)
            {
                // create an AudioLayerInstance
                AudioLayerInstance layerInstance = new AudioLayerInstance()
                {
                    name = layer.name,
                    playSource = new AudioSource[layer.clips.Length],
                    nextPlayIndex = 0
                };
                _audioLayerInstances.Add(layerInstance);

                // create the parent for current layer
                GameObject layerParent = new GameObject(layer.name);
                layerParent.transform.SetParent(gameObject.transform);
                layerParent.transform.localPosition = Vector3.zero;

                // instantiate all the audio sources in this layer
                for (int i = 0; i < layer.clips.Length; i++)
                {
                    AudioClip layerClip = layer.clips[i];

                    AudioSource layerSource = Instantiate(layer.sourcePrefab, layerParent.transform);
                    layerSource.transform.localPosition = Vector3.zero;
                    layerSource.clip = layerClip;

                    // add the Audio Source to the layer instance
                    layerInstance.playSource[i] = layerSource;
                }
                
                // Shuffle all the audio ready to play in the instance
                Shuffle.ArrayShuffle(layerInstance.playSource);
            }
        }

        #region IAudioPlayer

        public bool playOnstart
        {
            get
            {
                return _playOnStart;
            }
            set
            {
                _playOnStart = value;
            }
        }

        public bool isLoop
        {
            get
            {
                return _isLoop;
            }
            set
            {
                _isLoop = value;
            }
        }

        public float loopTime
        {
            get
            {
                return _loopTime;
            }
            set
            {
                _loopTime = value;
            }
        }

        [Button()]
        public void Play()
        {
            if (isLoop)
            {
                // restart a new coroutine
                if (_loopPlayCoroutine != null)
                {
                    StopCoroutine(_loopPlayCoroutine);
                }
                _loopPlayCoroutine = StartCoroutine(LoopPlayCoroutine(loopTime));
            }
            else
            {
                PlayHelper();
            }
        }

        /// <summary>
        /// The helper method to play the audio
        /// </summary>
        private async void PlayHelper()
        {
            if (!simulateSoundSpeed)
            {
                foreach (AudioLayerInstance layerInstance in _audioLayerInstances)
                {
                    // if there's no clip in the layer, continue to the next layer
                    if (layerInstance.playSource == null || layerInstance.playSource.Length == 0)
                    {
                        continue;
                    }
                
                    // Play the clip
                    layerInstance.playSource[layerInstance.nextPlayIndex].Play();
                
                    // increase the index
                    layerInstance.nextPlayIndex++;
                
                    // if the index exceed the length of the array
                    // reset the index to 0 and shuffle the play list
                    if (layerInstance.nextPlayIndex >= layerInstance.playSource.Length)
                    {
                        layerInstance.nextPlayIndex = 0;
                        Shuffle.ArrayShuffle(layerInstance.playSource);
                    }
                }
                return;
            }
            
            // Get the distance between audio source and main cam
            float distance = (transform.position - mainCam.transform.position).magnitude;
            
            // Calculate the delay
            float delay = distance / 340;

            await Task.Delay((int) (delay * 1000));
            
            foreach (AudioLayerInstance layerInstance in _audioLayerInstances)
            {
                // if there's no clip in the layer, continue to the next layer
                if (layerInstance.playSource == null || layerInstance.playSource.Length == 0)
                {
                    continue;
                }
                
                // Play the clip
                layerInstance.playSource[layerInstance.nextPlayIndex].Play();
                
                // increase the index
                layerInstance.nextPlayIndex++;
                
                // if the index exceed the length of the array
                // reset the index to 0 and shuffle the play list
                if (layerInstance.nextPlayIndex >= layerInstance.playSource.Length)
                {
                    layerInstance.nextPlayIndex = 0;
                    Shuffle.ArrayShuffle(layerInstance.playSource);
                }
            }
        }

        /// <summary>
        /// The coroutine to loop play the audio clip
        /// </summary>
        /// <param name="loopTime">the time interval to loop</param>
        /// <returns>IEnumerator handler</returns>
        private IEnumerator LoopPlayCoroutine(float loopTime)
        {
            while (true)
            {
                // play once
                PlayHelper();

                // wait for loopTime seconds to play
                yield return new WaitForSeconds(loopTime);
            }
        }

        [Button()]
        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        [Button()]
        public void Resume()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}