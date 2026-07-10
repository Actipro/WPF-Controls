using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents a view model for a window.
/// </summary>
/// <param name="barManager">The <see cref="Common.BarManager"/> to be associated with the view model.</param>
public class WindowViewModel(BarManager barManager) : ObservableObjectBase {

	private IDictionary<CompositeCommand, ICommand>? _commandMappings;
	private DocumentViewModel? _selectedDocument;

	// --------------------------------------------------------------------------------------------------
	// EVENTS
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Raised to request that the view focuses the selected document.
	/// </summary>
	public event EventHandler? RequestFocusSelectedDocument;

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private Dictionary<CompositeCommand, ICommand> CreateCommandMappings(BarManager barManager)
		=> GetCommandMappings(barManager).ToDictionary(pair => pair.Key, pair => pair.Value);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="Common.BarManager"/> associated with this view model.
	/// </summary>
	public BarManager BarManager { get; } = barManager ?? throw new ArgumentNullException(nameof(barManager));

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
	/// Raises the <see cref="RequestFocusSelectedDocument"/> event.
	/// </summary>
	protected void OnRequestFocusSelectedDocument()
		=> RequestFocusSelectedDocument?.Invoke(this, EventArgs.Empty);

	/// <summary>
	/// Registers each mapped <see cref="ICommand"/> with the corresponding <see cref="CompositeCommand"/>.
	/// </summary>
	public void RegisterCommands() {
		_commandMappings ??= CreateCommandMappings(BarManager);
		foreach (var mapping in _commandMappings) {
			var compositeCommand = mapping.Key;
			var localCommand = mapping.Value;
			compositeCommand.RegisterCommand(localCommand);
		}
	}

	/// <summary>
	/// The view model of the selected document.
	/// </summary>
	public DocumentViewModel? SelectedDocument {
		get => _selectedDocument;
		set {
			if (_selectedDocument != value) {
				_selectedDocument?.UnregisterCommands();
				_selectedDocument = value;
				_selectedDocument?.RegisterCommands();

				OnPropertyChanged();
			}
		}
	}

	/// <summary>
	/// Unregisters each mapped <see cref="ICommand"/> from the corresponding <see cref="CompositeCommand"/>.
	/// </summary>
	public void UnregisterCommands() {
		if (_commandMappings is not null) {
			foreach (var mapping in _commandMappings) {
				var compositeCommand = mapping.Key;
				var localCommand = mapping.Value;
				compositeCommand.UnregisterCommand(localCommand);
			}
		}
	}

}
