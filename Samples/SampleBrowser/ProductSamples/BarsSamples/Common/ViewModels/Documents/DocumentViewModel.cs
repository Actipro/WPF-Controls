using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a view model for a document.
	/// </summary>
	public class DocumentViewModel : ObservableObjectBase {

		private IDictionary<CompositeCommand, ICommand> commandMappings;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="barManager">The <see cref="Common.BarManager"/> to be associated with the view model.</param>
		public DocumentViewModel(BarManager barManager) {
			this.BarManager = barManager ?? throw new ArgumentNullException(nameof(barManager));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a mapping between <see cref="CompositeCommand"/> instances and the corresponding
		/// <see cref="ICommand"/> used by this view model.
		/// </summary>
		/// <param name="barManager">The associated <see cref="Common.BarManager"/>.</param>
		/// <returns></returns>
		private IDictionary<CompositeCommand, ICommand> CreateCommandMappings(BarManager barManager) {
			return GetCommandMappings(barManager).ToDictionary(x => x.Key, x => x.Value);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets if commands are currently registered.
		/// </summary>
		/// <value>
		/// <c>true</c> if commands are currently registered; otherwise, <c>false</c>.
		/// </value>
		public bool AreCommandsRegistered { get; private set; }

		/// <summary>
		/// Gets the <see cref="Common.BarManager"/> associated with this view model.
		/// </summary>
		/// <value>A <see cref="Common.BarManager"/>.</value>
		protected BarManager BarManager { get; }

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
		/// Handles any changes that are necessary after commands are registered.
		/// </summary>
		protected virtual void OnCommandsRegistered() {
			// No default operation
		}

		/// <summary>
		/// Registers each mapped <see cref="ICommand"/> with the corresponding <see cref="CompositeCommand"/>.
		/// </summary>
		public void RegisterCommands() {
			if (!AreCommandsRegistered) {
				try {
					if (commandMappings is null)
						commandMappings = CreateCommandMappings(BarManager);

					foreach (var mapping in commandMappings) {
						var compositeCommand = mapping.Key;
						var localCommand = mapping.Value;
						compositeCommand.RegisterCommand(localCommand);
					}

					OnCommandsRegistered();
				}
				finally {
					AreCommandsRegistered = true;
				}
			}
		}

		/// <summary>
		/// Unregisters each mapped <see cref="ICommand"/> from the corresponding <see cref="CompositeCommand"/>.
		/// </summary>
		public void UnregisterCommands() {
			if (AreCommandsRegistered) {
				try {
					if (commandMappings != null) {
						foreach (var mapping in commandMappings) {
							var compositeCommand = mapping.Key;
							var localCommand = mapping.Value;
							compositeCommand.UnregisterCommand(localCommand);
						}
					}
				}
				finally {
					AreCommandsRegistered = false;
				}
			}
		}

	}
}
