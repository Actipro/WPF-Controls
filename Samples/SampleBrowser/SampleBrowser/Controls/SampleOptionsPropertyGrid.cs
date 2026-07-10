using ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors;
using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Represents a <see cref="PropertyGrid"/> used to configure sample options.
/// </summary>
public class SampleOptionsPropertyGrid : PropertyGrid {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleOptionsPropertyGrid() {
		DefaultStyleKey = typeof(SampleOptionsPropertyGrid);

		// Adjust pre-defined columns
		Columns[0].CellBorderThickness = new Thickness();
		Columns[1].CellPadding = new Thickness(0, 2, 0, 2);

		// Use Grids/Editors integration
		BuiltinPropertyEditors.SetIsEnabled(this, true);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnPropertyValueChanged(PropertyModelValueChangeEventArgs e) {
		base.OnPropertyValueChanged(e);

		// Ensure the target element is scrolled into view
		(e.PropertyModel.Target as FrameworkElement)?.BringIntoView();
	}

}
