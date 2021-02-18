using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DataGridControl = System.Windows.Controls.DataGrid;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.Windows.Controls.DataGrid {

	/// <summary>
	/// Provides attached behavior for <see cref="DataGridControl"/> controls to track the selection.
	/// </summary>
	public static class SelectionBehavior {

		#region Dependency Property Keys

		/// <summary>
		/// Identifies the <c>IsSelectedHeader</c> dependency property key.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <c>IsSelectedHeader</c> dependency property key.</value>
		private static readonly DependencyPropertyKey IsSelectedHeaderPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsSelectedHeader",
			typeof(bool), typeof(SelectionBehavior), new FrameworkPropertyMetadata(false));

		#endregion // Dependency Property Keys

		#region Dependency Properties

		/// <summary>
		/// Identifies the <c>IsSelectedHeader</c> attached dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <c>IsSelectedHeader</c> attached dependency property.</value>
		public static readonly DependencyProperty IsSelectedHeaderProperty = IsSelectedHeaderPropertyKey.DependencyProperty;

		/// <summary>
		/// Identifies the <c>TrackingModes</c> attached dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <c>TrackingModes</c> attached dependency property.</value>
		public static readonly DependencyProperty TrackingModesProperty = DependencyProperty.RegisterAttached("TrackingModes",
			typeof(SelectionTrackingModes), typeof(SelectionBehavior), new FrameworkPropertyMetadata(SelectionTrackingModes.None, OnTrackingModesPropertyValueChanged));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Handles the <c>SelectedCellsChanged</c> event of a <see cref="DataGridControl"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <c>SelectedCellsChangedEventArgs</c> instance containing the event data.</param>
		private static void OnDatagridSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) {
			UpdateSelectedHeaders(sender as DataGridControl);
		}

		/// <summary>
		/// Called when <see cref="TrackingModesProperty"/> is changed.
		/// </summary>
		/// <param name="d">The dependency object that was changed.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void OnTrackingModesPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			DataGridControl datagrid = d as DataGridControl;
			if (null == datagrid)
				return;

			SelectionTrackingModes trackingModes = (SelectionTrackingModes)e.NewValue;
			if (SelectionTrackingModes.None != trackingModes)
				datagrid.SelectedCellsChanged += new SelectedCellsChangedEventHandler(OnDatagridSelectedCellsChanged);
			else
				datagrid.SelectedCellsChanged -= new SelectedCellsChangedEventHandler(OnDatagridSelectedCellsChanged);

			UpdateSelectedHeaders(datagrid);

		}

		/// <summary>
		/// Updates the selected <see cref="DataGridColumnHeader"/>.
		/// </summary>
		/// <param name="datagrid">The datagrid.</param>
		private static void UpdateSelectedHeaders(DataGridControl datagrid) {
			if (null == datagrid)
				return;

			// Get the list of headers
			IList<DependencyObject> headers = VisualTreeHelperExtended.GetAllDescendants(datagrid, typeof(DataGridColumnHeader));
			if (null == headers || 0 == headers.Count)
				return;

			// Update the selection based on the current tracking modes
			SelectionTrackingModes trackingModes = GetTrackingModes(datagrid);
			if (0 != (trackingModes & SelectionTrackingModes.Headers)) {
				
				// Update header selections, if any
				foreach (DataGridColumnHeader header in headers) {

					// Determine if the column associated with this header has any selected cells
					bool isSelected = false;
					foreach (DataGridCellInfo cellInfo in datagrid.SelectedCells) {
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
				foreach (DataGridColumnHeader header in headers) {
					if (GetIsSelectedHeader(header))
						header.ClearValue(IsSelectedHeaderPropertyKey);
				}
			}
		}

		#endregion // NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a value indicating whether a <see cref="DataGridColumnHeader"/> corresponds to the column
		/// of the a currently selected <see cref="DataGridCell"/>.
		/// </summary>
		/// <param name="obj">The object from which the property value is read.</param>
		/// <returns>The object's value.</returns>
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static bool GetIsSelectedHeader(DataGridColumnHeader obj) {
			if (obj == null)
				throw new ArgumentNullException("obj");
			return (bool)obj.GetValue(IsSelectedHeaderProperty);
		}

		/// <summary>
		/// Gets the value of the <see cref="TrackingModesProperty"/> attached property for a specified <see cref="DataGridControl"/>.
		/// </summary>
		/// <param name="obj">The object to which the attached property is retrieved.</param>
		/// <returns>
		/// The selections that should be tracked in a <see cref="DataGridControl"/>.
		/// </returns>
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static SelectionTrackingModes GetTrackingModes(DataGridControl obj) {
			if (null == obj) throw new ArgumentNullException("obj");
			return (SelectionTrackingModes)obj.GetValue(SelectionBehavior.TrackingModesProperty);
		}
		/// <summary>
		/// Sets the value of the <see cref="TrackingModesProperty"/> attached property to a specified <see cref="DataGridControl"/>.
		/// </summary>
		/// <param name="obj">The object to which the attached property is written.</param>
		/// <param name="value">
		/// A value indicating the selections that should be tracked in a <see cref="DataGridControl"/>.
		/// </param>
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static void SetTrackingModes(DataGridControl obj, SelectionTrackingModes value) {
			if (null == obj) throw new ArgumentNullException("obj");
			obj.SetValue(SelectionBehavior.TrackingModesProperty, value);
		}

		#endregion // PUBLIC PROCEDURES
	}
}