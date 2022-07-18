using System;
using UnityEngine;

namespace KpattGames.Channels
{
    /// <summary>
    /// Encapsulates an Action which can be subscribed to and invoked.
    /// </summary>
    /// <typeparam name="T">The type of the value to pass into the event.</typeparam>
    public abstract class Channel<T> : ScriptableObject
    {
        /// <summary>
        /// The event to subscribe to.
        /// </summary>
        public event Action<T> OnEventRaised;

        /// <summary>
        /// Raises the channel's event.
        /// </summary>
        /// <param name="value">The value to pass into the event.</param>
        public void RaiseEvent(T value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}