using System;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents an abstract view model base for an observable object that is identified by a unique string key.
	/// </summary>
	public abstract class BarKeyedObjectViewModelBase : ObservableObjectBase, IHasKey {

		private string key;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="BarKeyedObjectViewModelBase"/> class.
		/// </summary>
		protected BarKeyedObjectViewModelBase()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BarKeyedObjectViewModelBase"/> class.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		protected BarKeyedObjectViewModelBase(string key) {
			this.key = key;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IHasKey.Key"/>
		public string Key {
			get => key;
			set {
				if (key != value) {
					if (!string.IsNullOrEmpty(key))
						throw new ArgumentException("The key cannot be changed once it has been set.", nameof(value));

					key = value;
					this.NotifyPropertyChanged(nameof(Key));
				}
			}
		}
		
		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[Key='{this.Key}']";

	}

}
