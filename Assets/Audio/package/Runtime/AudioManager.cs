// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using UnityEngine;
using System.Globalization;
using System.Collections.Generic;

namespace Microsoft.MixedReality.Toolkit.Audio
{
    /// <summary>
    /// The manager that handles the playback of AudioEvents
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the audio manager
        /// </summary>
        private static AudioManager Instance;
        /// <summary>
        /// The currently-playing events at runtime
        /// </summary>
        public static List<ActiveEvent> ActiveEvents { get; private set; }
        /// <summary>
        /// The AudioSource components that have been added by a previously-played event
        /// </summary>
        private static List<AudioSource> AvailableSources = new List<AudioSource>();
        /// <summary>
        /// List of the previously started events
        /// </summary>
        private static List<ActiveEvent> previousEvents = new List<ActiveEvent>();
        /// <summary>
        /// The language that all voice events should play in
        /// </summary>
        public static int CurrentLanguage { get; private set; }
        /// <summary>
        /// The full list of languages available
        /// </summary>
        public static string[] Languages;
        /// <summary>
        /// The default number of AudioSources to create in the pool
        /// </summary>
        private static int DefaultSourcesCount = 40;
        /// <summary>
        /// The total list of AudioSources for the manager to use
        /// </summary>
        private static List<AudioSource> SourcePool = new List<AudioSource>();
        private static bool debugMode = false;
        public static bool DebugMode {  get { return debugMode; } }
        private const int MaxPreviousEvents = 300;
        public static List<ActiveEvent> PreviousEvents
        {
            get { return previousEvents; }
        }

        #region Interface

        /// <summary>
        /// Start playing an AudioEvent
        /// </summary>
        /// <param name="eventToPlay">The AudioEvent to play</param>
        /// <param name="emitterObject">The GameObject to play the event on</param>
        /// <returns>The reference for the runtime event that can be modified or stopped explicitly</returns>
        public static ActiveEvent PlayEvent(AudioEvent eventToPlay, GameObject emitterObject)
        {
            if (!ValidateManager() || !ValidateEvent(eventToPlay))
            {
                return null;
            }

            ActiveEvent tempEvent = new ActiveEvent(eventToPlay, emitterObject.transform);
            tempEvent.Play();

            return tempEvent;
        }

        public static ActiveEvent PlayEvent(AudioEvent eventToPlay, Vector3 position)
        {
            if (!ValidateManager() || !ValidateEvent(eventToPlay))
            {
                return null;
            }

            ActiveEvent tempEvent = new ActiveEvent(eventToPlay, null);
            tempEvent.Play();
            tempEvent.SetAllSourcePositions(position);

            return tempEvent;
        }

        /// <summary>
        /// Start playing an AudioEvent
        /// </summary>
        /// <param name="eventToPlay">The AudioEvent to play</param>
        /// <param name="emitter">The AudioSource component to play the event on</param>
        /// <returns>The reference for the runtime event that can be modified or stopped explicitly</returns>
        public static ActiveEvent PlayEvent(AudioEvent eventToPlay, AudioSource emitter)
        {
            Debug.LogWarningFormat("AudioManager: deprecated function called on event {0} - play on an AudioSource no longer supported");
            if (!ValidateManager() || !ValidateEvent(eventToPlay))
            {
                return null;
            }

            ActiveEvent tempEvent = new ActiveEvent(eventToPlay, emitter.transform);
            tempEvent.Play();

            return tempEvent;
        }

        /// <summary>
        /// Stop all active instances of an audio event
        /// </summary>
        /// <param name="eventsToStop">The event to stop all instances of</param>
        public static void StopAll(AudioEvent eventsToStop)
        {
            for (int i = ActiveEvents.Count - 1; i >= 0; i--)
            {
                ActiveEvent tempEvent = ActiveEvents[i];
                if (tempEvent.rootEvent == eventsToStop)
                {
                    tempEvent.Stop();
                }
            }
        }

        /// <summary>
        /// Stop all active instances of a group
        /// </summary>
        /// <param name="groupNum">The group number to stop all instances of</param>
        public static void StopAll(int groupNum)
        {
            for (int i = ActiveEvents.Count - 1; i >= 0; i--)
            {
                ActiveEvent tempEvent = ActiveEvents[i];
                if (tempEvent.rootEvent.Group == groupNum)
                {
                    tempEvent.Stop();
                }
            }
        }

        /// <summary>
        /// Clear an ActiveEvent from the list of ActiveEvents
        /// </summary>
        /// <param name="stoppedEvent">The event that is no longer playing to remove from the ActiveEvent list</param>
        public static void RemoveActiveEvent(ActiveEvent stoppedEvent)
        {
            List<EventSource> sources = stoppedEvent.sources;
            for (int i = 0; i < sources.Count; i++)
            {
                AudioSource tempSource = sources[i].source;
                if (!AvailableSources.Contains(tempSource))
                {
                    AvailableSources.Add(tempSource);
                }
            }

            ActiveEvents.Remove(stoppedEvent);
            stoppedEvent = null;
        }

        public static void AddPreviousEvent(ActiveEvent newEvent)
        {
            previousEvents.Add(newEvent);

            while (previousEvents.Count > MaxPreviousEvents)
            {
                previousEvents.RemoveAt(0);
            }
        }

        /// <summary>
        /// Get the list of all cultures for compatible languges
        /// </summary>
        public static void UpdateLanguages()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            Languages = new string[cultures.Length];
            for (int i = 0; i < cultures.Length; i++)
            {
                Languages[i] = cultures[i].Name;
            }
        }

        public static void SetDebugMode(bool toggle)
        {
            debugMode = toggle;
            ClearSourceText();
        }

        #endregion

        #region Private Functions

        private void Update()
        {
            for (int i = 0; i < ActiveEvents.Count; i++)
            {
                ActiveEvent tempEvent = ActiveEvents[i];
                if (tempEvent != null && tempEvent.sources.Count != 0)
                {
                    tempEvent.Update();
                }
            }
        }

        /// <summary>
        /// Instantiate a new GameObject and add the AudioManager component
        /// </summary>
        private static void CreateInstance()
        {
            if (Instance != null)
            {
                return;
            }

            CurrentLanguage = 0;
            ActiveEvents = new List<ActiveEvent>();
            GameObject instanceObject = new GameObject("AudioManager");
            Instance = instanceObject.AddComponent<AudioManager>();
            DontDestroyOnLoad(instanceObject);
            CreateSources();
        }

        /// <summary>
        /// Create the pool of AudioSources
        /// </summary>
        private static void CreateSources()
        {
            for (int i = 0; i < DefaultSourcesCount; i++)
            {
                GameObject sourceGO = new GameObject("AudioSource" + i);
                sourceGO.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                sourceGO.transform.SetParent(Instance.transform);
                AudioSource tempSource = sourceGO.AddComponent<AudioSource>();
                tempSource.playOnAwake = false;
                SourcePool.Add(tempSource);
                AvailableSources.Add(tempSource);
                TextMesh newText = sourceGO.AddComponent<TextMesh>();
                newText.characterSize = 0.2f;
            }
        }

        private static void ClearSourceText()
        {
            for (int i = 0; i < AvailableSources.Count; i++)
            {
                TextMesh tempText = AvailableSources[i].GetComponent<TextMesh>();
                if (tempText != null)
                {
                    tempText.text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Get the number of active instances of an AudioEvent
        /// </summary>
        /// <param name="audioEvent">The event to query the number of active instances of</param>
        /// <returns>The number of active instances of the specified AudioEvent</returns>
        private static int CountActiveInstances(AudioEvent audioEvent)
        {
            int tempCount = 0;

            for (int i = 0; i < ActiveEvents.Count; i++)
            {
                if (ActiveEvents[i].rootEvent == audioEvent)
                {
                    tempCount++;
                }
            }

            return tempCount;
        }

        /// <summary>
        /// Call an immediate stop on all active audio events of a particular group
        /// </summary>
        /// <param name="groupNum">The group number to stop</param>
        private static void StopGroupInstances(int groupNum)
        {
            for (int i = 0; i < ActiveEvents.Count; i++)
            {
                ActiveEvent tempEvent = ActiveEvents[i];
                if (tempEvent.rootEvent.Group == groupNum)
                {
                    Debug.LogFormat("Stopping: {0}", tempEvent.name);
                    tempEvent.StopImmediate();
                }
            }
        }

        /// <summary>
        /// Look for an existing AudioSource component that is not currently playing
        /// </summary>
        /// <param name="emitterObject">The GameObject the AudioSource needs to be attached to</param>
        /// <returns>An AudioSource reference if one exists, otherwise null</returns>
        public static AudioSource GetUnusedSource()
        {
            ClearNullAudioSources();

            if (AvailableSources.Count > 0)
            {
                AudioSource tempSource = AvailableSources[0];
                AvailableSources.Remove(tempSource);
                return tempSource;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Remove any references to AudioSource components that no longer exist
        /// </summary>
        private static void ClearNullAudioSources()
        {
            for (int i = AvailableSources.Count - 1; i >= 0; i--)
            {
                AudioSource tempSource = AvailableSources[i];
                if (tempSource == null)
                {
                    AvailableSources.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Make sure that the AudioManager has all of the required components
        /// </summary>
        /// <returns>Whether there is a valid AudioManager instance</returns>
        public static bool ValidateManager()
        {
            if (Instance == null)
            {
                CreateInstance();
            }

            return Instance != null;
        }

        private static bool ValidateEvent(AudioEvent eventToPlay)
        {
            if (eventToPlay == null)
            {
                return false;
            }

            if (eventToPlay.InstanceLimit > 0 && CountActiveInstances(eventToPlay) >= eventToPlay.InstanceLimit)
            {
                return false;
            }

            if (eventToPlay.Group != 0)
            {
                StopGroupInstances(eventToPlay.Group);
            }

            return true;
        }

        #endregion

        #region Editor

        /// <summary>
        /// Mute all ActiveEvents that are not soloed
        /// </summary>
        public static void ApplyActiveSolos()
        {
            ValidateManager();

            bool soloActive = false;
            for (int i = 0; i < ActiveEvents.Count; i++)
            {
                if (ActiveEvents[i].Soloed)
                {
                    soloActive = true;
                }
            }

            if (soloActive)
            {
                for (int i = 0; i < ActiveEvents.Count; i++)
                {
                    ActiveEvents[i].ApplySolo();
                }
            }
            else
            {
                ClearActiveSolos();
            }
        }

        /// <summary>
        /// Unmute all events
        /// </summary>
        public static void ClearActiveSolos()
        {
            ValidateManager();

            for (int i = 0; i < ActiveEvents.Count; i++)
            {
                ActiveEvents[i].ClearSolo();
            }
        }

        #endregion
    }
}
