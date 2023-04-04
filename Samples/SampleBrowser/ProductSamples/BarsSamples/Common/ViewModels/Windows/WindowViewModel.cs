using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a view model for a window.
	/// </summary>
	public class WindowViewModel : ObservableObjectBase {

		private IDictionary<CompositeCommand, ICommand> commandMappings;
		private DocumentViewModel selectedDocument;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Raised to request that the view focuses the selected document.
		/// </summary>
		public event EventHandler RequestFocusSelectedDocument;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="barManager">The <see cref="Common.BarManager"/> to be associated with the view model.</param>
		public WindowViewModel(BarManager barManager) {
			this.BarManager = barManager ?? throw new ArgumentNullException(nameof(barManager));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private IDictionary<CompositeCommand, ICommand> CreateCommandMappings(BarManager barManager) {
			return GetCommandMappings(barManager).ToDictionary(x => x.Key, x => x.Value);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="Common.BarManager"/> associated with this view model.
		/// </summary>
		/// <value>A <see cref="Common.BarManager"/>.</value>
		public BarManager BarManager { get; }

		/// <summary>
		/// Gets each <see cref="CompositeCommand"/> that is mapped to an <see cref="ICommand"/>.
		/// </summary>
		/// <param name="barManager">The <see cref="Common.BarManager"/> associated with this view model.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey, TValue}"/> for each <see cref="CompositeCommand"/> key
		/// that is mapped to a corresponding <see cref="ICommand"/> value.
		/// </returns>
		protected virtual IEnumerable<KeyValuePair<CompositeCommand, ICommand>> GetCommandMappings(BarManager barManager) {
			// No default command mappings
			yield break;
		}

		/// <summary>
		/// Raises the <see cref="RequestFocusSelectedDocument"/> event.
		/// </summary>
		protected void OnRequestFocusSelectedDocument() {
			RequestFocusSelectedDocument?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Registers each mapped <see cref="ICommand"/> with the corresponding <see cref="CompositeCommand"/>.
		/// </summary>
		public void RegisterCommands() {
			if (commandMappings is null)
				commandMappings = CreateCommandMappings(BarManager);
			foreach (var mapping in commandMappings) {
				var compositeCommand = mapping.Key;
				var localCommand = mapping.Value;
				compositeCommand.RegisterCommand(localCommand);
			}
		}

		/// <summary>
		/// Gets or sets the view model of the selected document.
		/// </summary>
		/// <value>The selected <see cref="DocumentViewModel"/>, or <c>null</c> if a document is not selected.</value>
		public DocumentViewModel SelectedDocument {
			get => selectedDocument;
			set {
				if (selectedDocument != value) {
					selectedDocument?.UnregisterCommands();
					selectedDocument = value;
					selectedDocument?.RegisterCommands();

					this.NotifyPropertyChanged(nameof(SelectedDocument));
				}

			}
		}

		/// <summary>
		/// Unregisters each mapped <see cref="ICommand"/> from the corresponding <see cref="CompositeCommand"/>.
		/// </summary>
		public void UnregisterCommands() {
			if (commandMappings != null) {
				foreach (var mapping in commandMappings) {
					var compositeCommand = mapping.Key;
					var localCommand = mapping.Value;
					compositeCommand.UnregisterCommand(localCommand);
				}
			}
		}

	}

}
