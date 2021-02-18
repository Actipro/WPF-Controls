using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DataGridControl = System.Windows.Controls.DataGrid;
using System.Windows.Input;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.Windows.Controls.DataGrid {

	/// <summary>
	/// Provides attached behavior for <see cref="DataGridControl"/> controls to track the focus.
	/// </summary>
	public static class FocusBehavior {

		#region Dependency Property Keys

		/// <summary>
		/// Identifies the <c>IsFocusedHeader</c> dependency property key.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <c>IsFocusedHeader</c> dependency property key.</value>
		private static readonly DependencyPropertyKey IsFocusedHeaderPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsFocusedHeader",
			typeof(bool), typeof(FocusBehavior), new FrameworkPropertyMetadata(false));

		#endregion // Dependency Property Keys

		#region Dependency Properties

		/// <summary>
		/// Identifies the <c>IsFocusedHeader</c> attached dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <c>IsFocusedHeader</c> attached dependency property.</value>
		public static readonly DependencyProperty IsFocusedHeaderProperty = IsFocusedHeaderPropertyKey.DependencyProperty;

		/// <summary>
		/// Identifies the <c>TrackingModes</c> attached dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <c>TrackingModes</c> attached dependency property.</value>
		public static readonly DependencyProperty TrackingModesProperty = DependencyProperty.RegisterAttached("TrackingModes",
			typeof(FocusTrackingModes), typeof(FocusBehavior), new FrameworkPropertyMetadata(FocusTrackingModes.None, OnTrackingModesPropertyValueChanged));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when an element in a <see cref="DataGridControl"/> gets the keyboard focus.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private static void OnDataGridGotFocus(object sender, RoutedEventArgs e) {
			UpdateFocusedHeader(sender as DataGridControl);
		}

		/// <summary>
		/// Occurs when an element in a <see cref="DataGridControl"/> loses the keyboard focus.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private static void OnDataGridLostFocus(object sender, RoutedEventArgs e) {
			UpdateFocusedHeader(sender as DataGridControl);
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

			FocusTrackingModes trackingModes = (FocusTrackingModes)e.NewValue;
			if (FocusTrackingModes.None != trackingModes) {
				datagrid.AddHandler(UIElement.GotFocusEvent, (RoutedEventHandler)OnDataGridGotFocus, true);
				datagrid.AddHandler(UIElement.LostFocusEvent, (RoutedEventHandler)OnDataGridLostFocus, true);
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
			if (null == datagrid)
				return;

			// Get the list of headers
			IList<DependencyObject> headers = VisualTreeHelperExtended.GetAllDescendants(datagrid, typeof(DataGridColumnHeader));
			if (null == headers || 0 == headers.Count)
				return;

			// Update the focus based on the current tracking Modes
			FocusTrackingModes trackingModes = GetTrackingModes(datagrid);
			if (0 != (trackingModes & FocusTrackingModes.Headers)) {

				// Get the focused cell, if any, and look for an ancestor cell when editing cell
				DataGridCell cell = Keyboard.FocusedElement as DataGridCell;
				if (cell == null)
					cell = VisualTreeHelperExtended.GetAncestor(Keyboard.FocusedElement as FrameworkElement, typeof(DataGridCell)) as DataGridCell;

				// Update header focus, if any
				foreach (DataGridColumnHeader header in headers) {

					// Determine if the column associated with this header if focused
					bool isFocused = (null != cell && cell.Column == header.Column);

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
				foreach (DataGridColumnHeader header in headers) {
					if (GetIsFocusedHeader(header))
						header.ClearValue(IsFocusedHeaderPropertyKey);
				}
			}
		}

		#endregion // NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a value indicating whether a <see cref="DataGridColumnHeader"/> corresponds to the column
		/// of the a currently focused <see cref="DataGridCell"/>.
		/// </summary>
		/// <param name="obj">The object from which the property value is read.</param>
		/// <returns>The object's value.</returns>
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static bool GetIsFocusedHeader(DataGridColumnHeader obj) {
			if (obj == null)
				throw new ArgumentNullException("obj");
			return (bool)obj.GetValue(IsFocusedHeaderProperty);
		}

		/// <summary>
		/// Gets the value of the <see cref="TrackingModesProperty"/> attached property for a specified <see cref="DataGridControl"/>.
		/// </summary>
		/// <param name="obj">The object to which the attached property is retrieved.</param>
		/// <returns>
		/// The focus that should be tracked in a <see cref="DataGridControl"/>.
		/// </returns>
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static FocusTrackingModes GetTrackingModes(DataGridControl obj) {
			if (null == obj) throw new ArgumentNullException("obj");
			return (FocusTrackingModes)obj.GetValue(FocusBehavior.TrackingModesProperty);
		}
		/// <summary>
		/// Sets the value of the <see cref="TrackingModesProperty"/> attached property to a specified <see cref="DataGridControl"/>.
		/// </summary>
		/// <param name="obj">The object to which the attached property is written.</param>
		/// <param name="value">
		/// A value indicating the focus that should be tracked in a <see cref="DataGridControl"/>.
		/// </param>
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static void SetTrackingModes(DataGridControl obj, FocusTrackingModes value) {
			if (null == obj) throw new ArgumentNullException("obj");
			obj.SetValue(FocusBehavior.TrackingModesProperty, value);
		}

		#endregion // PUBLIC PROCEDURES
	}
}