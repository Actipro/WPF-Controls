using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using ActiproSoftware.Windows.Themes;
using DataGridControl = System.Windows.Controls.DataGrid;

namespace ActiproSoftware.Windows.Controls.DataGrid {

	/// <summary>
	/// Represents a <see cref="DataGridControl"/> that uses custom themes and integrates into the Actipro theme manager.
	/// </summary>
	public class ThemedDataGrid : DataGridControl {

		private Style defaultCheckBoxEditingStyle;
		private Style defaultCheckBoxStyle;
		private Style defaultComboBoxStyle;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <see cref="ThemedDataGrid"/> class.
		/// </summary>
		static ThemedDataGrid() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedDataGrid), new FrameworkPropertyMetadata(typeof(ThemedDataGrid)));
		}

		#endregion // OBJECT
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Applies a themed style to the element in the specified column, if the current style is defaulted.
		/// </summary>
		/// <param name="column">The column to examine.</param>
		protected virtual void ApplyThemedStyle(DataGridCheckBoxColumn column) {
			if (column == null)
				throw new ArgumentNullException("column");

			// If one of the styles is defaulted...
			if ((column.ElementStyle == DataGridCheckBoxColumn.DefaultElementStyle) || (column.EditingElementStyle == DataGridCheckBoxColumn.DefaultEditingElementStyle)) {
				if (column.ElementStyle == DataGridCheckBoxColumn.DefaultElementStyle) {
					// Create the default style as necessary
					if (defaultCheckBoxStyle == null) {
						var basedOnStyle = this.TryFindResource(SharedResourceKeys.CheckBoxStyleKey) as Style;
						if (basedOnStyle == null)
							return;

						defaultCheckBoxStyle = new Style(typeof(CheckBox)) {
							BasedOn = basedOnStyle,
							Setters = {
								new Setter(UIElement.IsHitTestVisibleProperty, false),
								new Setter(UIElement.FocusableProperty, false),
								new Setter(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center),
								new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top),
							}
						};
						defaultCheckBoxStyle.Seal();
					}

					column.ElementStyle = defaultCheckBoxStyle;
				}

				if (column.EditingElementStyle == DataGridCheckBoxColumn.DefaultEditingElementStyle) {
					// Create the default style as necessary
					if (defaultCheckBoxEditingStyle == null) {
						var basedOnStyle = this.TryFindResource(SharedResourceKeys.CheckBoxStyleKey) as Style;
						if (basedOnStyle == null)
							return;

						defaultCheckBoxEditingStyle = new Style(typeof(CheckBox)) {
							BasedOn = basedOnStyle,
							Setters = {
								new Setter(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center),
								new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top),
							}
						};
						defaultCheckBoxEditingStyle.Seal();
					}

					column.EditingElementStyle = defaultCheckBoxEditingStyle;
				}
			}
		}

		/// <summary>
		/// Applies a themed style to the element in the specified column, if the current style is defaulted.
		/// </summary>
		/// <param name="column">The column to examine.</param>
		protected virtual void ApplyThemedStyle(DataGridComboBoxColumn column) {
			if (column == null)
				throw new ArgumentNullException("column");

			// If one of the styles is defaulted...
			if ((column.ElementStyle == DataGridComboBoxColumn.DefaultElementStyle) || (column.EditingElementStyle == DataGridComboBoxColumn.DefaultEditingElementStyle)) {
				// Create the default style as necessary
				if (defaultComboBoxStyle == null) {
					var basedOnStyle = this.TryFindResource(SharedResourceKeys.ComboBoxStyleKey) as Style;
					if (basedOnStyle == null)
						return;

					defaultComboBoxStyle = new Style(typeof(ComboBox)) {
						BasedOn = basedOnStyle,
						Setters = {
							new Setter(Selector.IsSynchronizedWithCurrentItemProperty, false)
						}
					};
					defaultComboBoxStyle.Seal();
				}

				if (column.ElementStyle == DataGridComboBoxColumn.DefaultElementStyle)
					column.ElementStyle = defaultComboBoxStyle;

				if (column.EditingElementStyle == DataGridComboBoxColumn.DefaultEditingElementStyle)
					column.EditingElementStyle = defaultComboBoxStyle;
			}
		}

		/// <summary>
		/// Occurs when auto-generating a column.
		/// </summary>
		/// <param name="e">The <c>DataGridAutoGeneratingColumnEventArgs</c> that contains data related to this event.</param>
		protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e) {
			if (e == null)
				throw new ArgumentNullException("e");

			// Apply themed styles as appropriate
			var comboBoxColumn = e.Column as DataGridComboBoxColumn;
			if (comboBoxColumn != null)
				this.ApplyThemedStyle(comboBoxColumn);
			else {
				var checkBoxColumn = e.Column as DataGridCheckBoxColumn;
				if (checkBoxColumn != null)
					this.ApplyThemedStyle(checkBoxColumn);
			}

			// Call the base method
			base.OnAutoGeneratingColumn(e);
		}

		#endregion // PUBLIC PROCEDURES

	}
}