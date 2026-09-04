using ActiproSoftware.ProductSamples.EditorsSamples.Common;
using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.EnumEditBoxIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		DataContext = this;

		sortedEditBox.EnumSortComparer = EnumValueNameSortComparer.Instance;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the selection is changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnSortComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
		var sortComboBox = (ComboBox)sender;
		editBox.EnumSortComparer = sortComboBox.SelectedIndex switch {
			1 => EnumValueNameSortComparer.Instance,
			_ => null
		};
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The current value.
	/// </summary>
	public EnumWithFlags EnumWithFlagsCurrentValue { get; set; }

	/// <summary>
	/// The current value.
	/// </summary>
	public EnumWithoutFlags EnumWithoutFlagsCurrentValue { get; set; }

	/// <summary>
	/// The current value.
	/// </summary>
	public EnumWithFlags? NullableEnumWithFlagsCurrentValue { get; set; } = EnumWithFlags.None;

	/// <summary>
	/// The current value.
	/// </summary>
	public EnumWithoutFlags? NullableEnumWithoutFlagsCurrentValue { get; set; } = EnumWithoutFlags.None;

}
