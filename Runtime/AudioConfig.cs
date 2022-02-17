using UnityEngine;

// ReSharper disable once CheckNamespace
namespace AudioSystem.Runtime
{
    /// <summary>
    /// One layer of the Config.
    /// Every time call the Play() in the Player, play one of the clips in the layer.
    /// </summary>
    [System.Serializable]
    public class AudioLayer
    {
        /// <summary>
        /// The name of the layer.
        /// </summary>
        public string name;

        /// <summary>
        /// The prefab of the audio source for this layer
        /// </summary>
        public AudioSource sourcePrefab;
        
        /// <summary>
        /// All the audio clips in the layer.
        /// </summary>
        public AudioClip[] clips;
    }

    /// <summary>
    /// The instance of AudioLayer, will be used on runtime
    /// </summary>
    [System.Serializable]
    public class AudioLayerInstance
    {
        /// <summary>
        /// The name of the layer.
        /// </summary>
        public string name;
        
        /// <summary>
        /// All the audio sources which can be played on runtime in that layer
        /// </summary>
        public AudioSource[] playSource;

        /// <summary>
        /// The index of next audio source to play
        /// </summary>
        public int nextPlayIndex = 0;
    }
    
    /// <summary>
    /// The config ScriptableObject that stores all the audio clips in the Player
    /// </summary>
    [CreateAssetMenu(fileName = "Audio Config", menuName = "FinTOKMAK/Audio/Audio Config", order = 0)]
    public class AudioConfig : ScriptableObject
    {
        /// <summary>
        /// All the layers in the config file
        /// </summary>
        public AudioLayer[] audioLayers;
    }
}