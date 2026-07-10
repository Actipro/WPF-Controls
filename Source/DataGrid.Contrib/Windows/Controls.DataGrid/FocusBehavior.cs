using ActiproSoftware.Windows.Extensions;
using DataGridControl = System.Windows.Controls.DataGrid;

namespace ActiproSoftware.Windows.Controls.DataGrid;

/// <summary>
/// Provides attached behavior for <see cref="DataGridControl"/> controls to track the focus.
/// </summary>
public static class FocusBehavior {

	#region Dependency Property Keys

	/// <summary>
	/// Defines the <c>IsFocusedHeader</c> attached property key.
	/// </summary>
	private static readonly DependencyPropertyKey IsFocusedHeaderPropertyKey
		= DependencyProperty.RegisterAttachedReadOnly("IsFocusedHeader", typeof(bool), typeof(FocusBehavior), new FrameworkPropertyMetadata(defaultValue: false));

	#endregion

	#region Dependency Properties

	/// <summary>
	/// Defines the <c>IsFocusedHeader</c> attached property.
	/// </summary>
	public static readonly DependencyProperty IsFocusedHeaderProperty
		= IsFocusedHeaderPropertyKey.DependencyProperty;

	/// <summary>
	/// Defines the <c>TrackingModes</c> attached property.
	/// </summary>
	public static readonly DependencyProperty TrackingModesProperty
		= DependencyProperty.RegisterAttached("TrackingModes", typeof(FocusTrackingModes), typeof(FocusBehavior), new FrameworkPropertyMetadata(defaultValue: FocusTrackingModes.None, OnTrackingModesPropertyValueChanged));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void OnDataGridGotFocus(object sender, RoutedEventArgs e) {
		if (sender is DataGridControl datagrid)
			UpdateFocusedHeader(datagrid);
	}

	private static void OnDataGridLostFocus(object sender, RoutedEventArgs e) {
		if (sender is DataGridControl datagrid)
			UpdateFocusedHeader(datagrid);
	}

	private static void OnTrackingModesPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		if (d is not DataGridControl datagrid)
			return;

		var trackingModes = (FocusTrackingModes)e.NewValue;
		if (FocusTrackingModes.None != trackingModes) {
			datagrid.AddHandler(UIElement.GotFocusEvent, (RoutedEventHandler)OnDataGridGotFocus, handledEventsToo: true);
			datagrid.AddHandler(UIElement.LostFocusEvent, (RoutedEventHandler)OnDataGridLostFocus, handledEventsToo: true);
		}
		else {
			datagrid.RemoveHandler(UIElement.GotFocusEvent, (RoutedEventHandler)OnDataGridGotFocus);
			datagrid.RemoveHandler(UIElement.LostFocusEvent, (RoutedEventHandler)OnDataGridLostFocus);
		}

		UpdateFocusedHeader(datagrid);
	}

	/// <summary>
	/// Updates the focused <see cref="DataGridColumnHeader"/>.
	/// </summary>
	private static void UpdateFocusedHeader(DataGridControl datagrid) {
		if (datagrid is null)
			return;

		// Get the list of headers
		var headers = datagrid.GetVisualDescendants().OfType<DataGridColumnHeader>().ToList();
		if (headers is not { Count: > 0 })
			return;

		// Update the focus based on the current tracking Modes
		var trackingModes = GetTrackingModes(datagrid);
		if ((trackingModes & FocusTrackingModes.Headers) != 0) {

			// Get the focused cell, if any, and look for an ancestor cell when editing cell
			var cell = (Keyboard.FocusedElement as FrameworkElement)?.FindAncestorOfType<DataGridCell>(includeSelf: true);

			// Update header focus, if any
			foreach (var header in headers) {

				// Determine if the column associated with this header if focused
				var isFocused = ((cell is not null) && (cell.Column == header.Column));

				// Update the focus for this header
				if (isFocused != GetIsFocusedHeader(header)) {
					if (isFocused)
						header.SetValue(IsFocusedHeaderPropertyKey, true);
					else
						header.ClearValue(IsFocusedHeaderPropertyKey);
				}
			}
		}
		else {
			// Clear header focus, if any
			foreach (var header in headers) {
				if (GetIsFocusedHeader(header))
					header.ClearValue(IsFocusedHeaderPropertyKey);
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns a value indicating whether a <see cref="DataGridColumnHeader"/> corresponds to the column
	/// of the a currently focused <see cref="DataGridCell"/>.
	/// </summary>
	/// <param name="obj">The object from which the property value is read.</param>
	public static bool GetIsFocusedHeader(DataGridColumnHeader obj)
		=> (bool)obj.GetValue(IsFocusedHeaderProperty);

	/// <summary>
	/// Gets the value of the <see cref="TrackingModesProperty"/> attached property for a specified <see cref="DataGridControl"/>.
	/// </summary>
	/// <param name="obj">The object to which the attached property is retrieved.</param>
	public static FocusTrackingModes GetTrackingModes(DataGridControl obj)
		=> (FocusTrackingModes)obj.GetValue(TrackingModesProperty);

	/// <summary>
	/// Sets the value of the <see cref="TrackingModesProperty"/> attached property to a specified <see cref="DataGridControl"/>.
	/// </summary>
	/// <param name="obj">The object to which the attached property is written.</param>
	/// <param name="value">
	/// A value indicating the focus that should be tracked in a <see cref="DataGridControl"/>.
	/// </param>
	public static void SetTrackingModes(DataGridControl obj, FocusTrackingModes value)
		=> obj.SetValue(TrackingModesProperty, value);

}
