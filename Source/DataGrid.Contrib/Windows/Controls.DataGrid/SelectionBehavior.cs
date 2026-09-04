using ActiproSoftware.Windows.Extensions;
using DataGridControl = System.Windows.Controls.DataGrid;

namespace ActiproSoftware.Windows.Controls.DataGrid;

/// <summary>
/// Provides attached behavior for <see cref="DataGridControl"/> controls to track the selection.
/// </summary>
public static class SelectionBehavior {

	#region Dependency Property Keys

	/// <summary>
	/// Defines the <c>IsSelectedHeader</c> attached property key.
	/// </summary>
	private static readonly DependencyPropertyKey IsSelectedHeaderPropertyKey
		= DependencyProperty.RegisterAttachedReadOnly("IsSelectedHeader", typeof(bool), typeof(SelectionBehavior), new FrameworkPropertyMetadata(defaultValue: false));

	#endregion

	#region Dependency Properties

	/// <summary>
	/// Defines the <c>IsSelectedHeader</c> attached property.
	/// </summary>
	public static readonly DependencyProperty IsSelectedHeaderProperty
		= IsSelectedHeaderPropertyKey.DependencyProperty;

	/// <summary>
	/// Defines the <c>TrackingModes</c> attached property.
	/// </summary>
	public static readonly DependencyProperty TrackingModesProperty
		= DependencyProperty.RegisterAttached("TrackingModes", typeof(SelectionTrackingModes), typeof(SelectionBehavior), new FrameworkPropertyMetadata(defaultValue: SelectionTrackingModes.None, OnTrackingModesPropertyValueChanged));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void OnDatagridSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) {
		if (sender is DataGridControl datagrid)
			UpdateSelectedHeaders(datagrid);
	}

	private static void OnTrackingModesPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		if (d is not DataGridControl datagrid)
			return;

		var trackingModes = (SelectionTrackingModes)e.NewValue;
		if (SelectionTrackingModes.None != trackingModes)
			datagrid.SelectedCellsChanged += OnDatagridSelectedCellsChanged;
		else
			datagrid.SelectedCellsChanged -= OnDatagridSelectedCellsChanged;

		UpdateSelectedHeaders(datagrid);

	}

	/// <summary>
	/// Updates the selected <see cref="DataGridColumnHeader"/>.
	/// </summary>
	/// <param name="datagrid">The datagrid.</param>
	private static void UpdateSelectedHeaders(DataGridControl datagrid) {
		if (datagrid is null)
			return;

		// Get the list of headers
		var headers = datagrid.GetVisualDescendants().OfType<DataGridColumnHeader>().ToList();
		if (headers is not { Count: > 0 })
			return;

		// Update the selection based on the current tracking modes
		var trackingModes = GetTrackingModes(datagrid);
		if ((trackingModes & SelectionTrackingModes.Headers) != 0) {

			// Update header selections, if any
			foreach (var header in headers) {

				// Determine if the column associated with this header has any selected cells
				bool isSelected = false;
				foreach (var cellInfo in datagrid.SelectedCells) {
					if (cellInfo.Column == header.Column) {
						isSelected = true;
						break;
					}
				}

				// Update the selection for this header
				if (isSelected != GetIsSelectedHeader(header)) {
					if (isSelected)
						header.SetValue(IsSelectedHeaderPropertyKey, true);
					else
						header.ClearValue(IsSelectedHeaderPropertyKey);
				}
			}
		}
		else {
			// Clear header selections, if any
			foreach (var header in headers) {
				if (GetIsSelectedHeader(header))
					header.ClearValue(IsSelectedHeaderPropertyKey);
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Gets a value indicating whether a <see cref="DataGridColumnHeader"/> corresponds to the column
	/// of the a currently selected <see cref="DataGridCell"/>.
	/// </summary>
	/// <param name="obj">The object from which the property value is read.</param>
	public static bool GetIsSelectedHeader(DataGridColumnHeader obj)
		=> (bool)obj.GetValue(IsSelectedHeaderProperty);

	/// <summary>
	/// Gets the value of the <see cref="TrackingModesProperty"/> attached property for a specified <see cref="DataGridControl"/>.
	/// </summary>
	/// <param name="obj">The object to which the attached property is retrieved.</param>
	public static SelectionTrackingModes GetTrackingModes(DataGridControl obj)
		=> (SelectionTrackingModes)obj.GetValue(TrackingModesProperty);

	/// <summary>
	/// Sets the value of the <see cref="TrackingModesProperty"/> attached property to a specified <see cref="DataGridControl"/>.
	/// </summary>
	/// <param name="obj">The object to which the attached property is written.</param>
	/// <param name="value">
	/// A value indicating the selections that should be tracked in a <see cref="DataGridControl"/>.
	/// </param>
	public static void SetTrackingModes(DataGridControl obj, SelectionTrackingModes value)
		=> obj.SetValue(TrackingModesProperty, value);

}
