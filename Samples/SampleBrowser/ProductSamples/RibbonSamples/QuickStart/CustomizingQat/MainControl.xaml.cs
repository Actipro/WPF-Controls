using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Ribbon.UI;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.CustomizingQat;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private string? _serializedQatItems;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static MainControl() {
		// Attach to the clone events
		EventManager.RegisterClassHandler(typeof(MainControl), CloneService.CloneCreatedEvent, new EventHandler<DependencyObjectItemRoutedEventArgs>(OnCloneCreatedEvent));
		EventManager.RegisterClassHandler(typeof(MainControl), CloneService.CloneCreatingEvent, new EventHandler<DependencyObjectItemRoutedEventArgs>(OnCloneCreatingEvent));
		EventManager.RegisterClassHandler(typeof(MainControl), CloneService.CloneDisposedEvent, new EventHandler<DependencyObjectItemRoutedEventArgs>(OnCloneDisposedEvent));
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initialize the <c>CustomizeQat</c> control by assigning it the ribbon.
	/// </summary>
	private void Initialize() {
		customizeQat.Ribbon = null;
		customizeQat.Ribbon = ribbon;
	}

	private void OnApplyChangesButtonClick(object sender, RoutedEventArgs e) {
		// Save changes to QAT customization
		customizeQat.Save();
	}

	/// <summary>
	/// Occurs right before a clone is about to be readied for use.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private static void OnCloneCreatedEvent(object? sender, DependencyObjectItemRoutedEventArgs e) {
		var control = (MainControl)sender!;

		// Modify the properties of the clones control or attach to any events here... the cloned control is in e.Item
		control.eventsListBox.Items.Insert(0, string.Format("CloneCreated: {0}", e.OriginalSource));
	}

	/// <summary>
	/// Occurs when a clone creation is first requested.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private static void OnCloneCreatingEvent(object? sender, DependencyObjectItemRoutedEventArgs e) {
		var control = (MainControl)sender!;

		// You can create your own clone of the source object here or instead let Ribbon create one for you that will be passed to CloneCreated
		control.eventsListBox.Items.Insert(0, string.Format("CloneCreating: {0}", e.OriginalSource));
	}

	/// <summary>
	/// Occurs when a clone is being disposed (tracking by CloneService is done).
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private static void OnCloneDisposedEvent(object? sender, DependencyObjectItemRoutedEventArgs e) {
		var control = (MainControl)sender!;

		// Remove any event handlers for the cloned control here... the cloned control is in e.Item
		control.eventsListBox.Items.Insert(0, string.Format("CloneDisposed: {0}", e.OriginalSource));
	}

	private void OnLoadStateButtonClick(object sender, RoutedEventArgs e) {
		// Deserialize the list of QAT items that was previous persisted
		var exceptions = ribbon.DeserializeQuickAccessToolBarItems(_serializedQatItems);

		// Reinitialize
		Initialize();

		// Show a message if there was a problem
		if (exceptions.Count > 0)
			MessageBox.Show(exceptions.Count + " error(s) occurred while deserializing the QAT items.  The collection of exceptions in OnLoadStateButtonClick explains the problem.  This messagebox is for debugging-only purposes in this QuickStart.", "Loading Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
	}

	private void OnSaveStateButtonClick(object sender, RoutedEventArgs e) {
		// Save the serialized list of QAT items
		_serializedQatItems = ribbon.SerializeQuickAccessToolBarItems();

		MessageBox.Show("The current items in the QAT (in the actual Ribbon) have now been serialized to a string that can be restored later.\r\n\r\nNow make some more QAT item changes and be sure to apply them if you are using the customization controls.\r\nAfter changes are made, use the Load QAT State button to reload the persisted item data.");

		// Enable the load QAT state button
		loadQatStateButton.IsEnabled = true;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnInitialized(EventArgs e) {
		base.OnInitialized(e);

		// Initialize
		Initialize();
	}

}
