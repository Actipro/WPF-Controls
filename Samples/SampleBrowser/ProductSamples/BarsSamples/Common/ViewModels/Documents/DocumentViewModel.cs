using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents a view model for a document.
/// </summary>
/// <param name="barManager">The <see cref="Common.BarManager"/> to be associated with the view model.</param>
public class DocumentViewModel(BarManager barManager) : ObservableObjectBase {

	private IDictionary<CompositeCommand, ICommand>? _commandMappings;

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a mapping between <see cref="CompositeCommand"/> instances and the corresponding
	/// <see cref="ICommand"/> used by this view model.
	/// </summary>
	/// <param name="barManager">The associated <see cref="Common.BarManager"/>.</param>
	private Dictionary<CompositeCommand, ICommand> CreateCommandMappings(BarManager barManager)
		=> GetCommandMappings(barManager).ToDictionary(pair => pair.Key, pair => pair.Value);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates if commands are currently registered.
	/// </summary>
	public bool AreCommandsRegistered { get; private set; }

	/// <summary>
	/// The <see cref="Common.BarManager"/> associated with this view model.
	/// </summary>
	protected BarManager BarManager { get; } = barManager ?? throw new ArgumentNullException(nameof(barManager));

	/// <summary>
	/// Returns each <see cref="CompositeCommand"/> that is mapped to an <see cref="ICommand"/>.
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
				_commandMappings ??= CreateCommandMappings(BarManager);

				foreach (var mapping in _commandMappings) {
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
				if (_commandMappings is not null) {
					foreach (var mapping in _commandMappings) {
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
