using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a service that can provide images for bar controls. 
	/// </summary>
	public class BarImageProvider : IBarImageProvider {

		private readonly Dictionary<string, Func<BarImageOptions, ImageSource>> factories = new Dictionary<string, Func<BarImageOptions, ImageSource>>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns whether there is a registration for the specified key.
		/// </summary>
		/// <param name="key">A string that uniquely identifies a bar control.</param>
		/// <returns>
		/// <c>true</c> if there is a registration for the specified key; otherwise, <c>false</c>.
		/// </returns>
		private bool IsRegistered(string key)
			=> factories.ContainsKey(key);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IBarImageProvider.GetImageSource(string, BarImageSize)"/>
		public ImageSource GetImageSource(string key, BarImageSize size)
			=> GetImageSource(key, new BarImageOptions(size));

		/// <inheritdoc cref="IBarImageProvider.GetImageSource(string, BarImageOptions)"/>
		public ImageSource GetImageSource(string key, BarImageOptions options) {
			if (factories.TryGetValue(key, out var factory))
				return factory.Invoke(options);

			return null;
		}

		/// <summary>
		/// Registers an image factory function for a key.
		/// </summary>
		/// <param name="key">A string that uniquely identifies a bar control.</param>
		/// <param name="factory">A factory method that examines <see cref="BarImageOptions"/> to return an <see cref="ImageSource"/>.</param>
		public void Register(string key, Func<BarImageOptions, ImageSource> factory) {
			if (string.IsNullOrEmpty(key))
				throw new ArgumentException("The key cannot be null or empty.", nameof(key));
			if (factory is null)
				throw new ArgumentNullException(nameof(factory));

			if (IsRegistered(key))
				throw new InvalidOperationException($"A factory with the key '{key}' has already been registered.");
			factories[key] = factory;
		}

		/// <summary>
		/// Unregisters an image factory function for a key.
		/// </summary>
		/// <param name="key">A string that uniquely identifies a bar control.</param>
		/// <returns>
		/// <c>true</c> if an image factory function was removed; otherwise, <c>false</c>.
		/// </returns>
		public bool Unregister(string key)
			=> factories.Remove(key);

	}

}
