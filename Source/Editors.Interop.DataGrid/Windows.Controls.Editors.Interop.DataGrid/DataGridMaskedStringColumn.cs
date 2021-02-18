using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	/// <summary>
	/// Represents a base class for data-bound columns for use in a <c>DataGrid</c> that utilize <c>MaskedTextBox</c> controls.
	/// </summary>
	public partial class DataGridMaskedStringColumn : DataGridBoundColumnBase {

		private static Style defaultElementStyle;
		private static Style defaultEditingElementStyle;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <c>DataGridMaskedStringColumn</c> class.
		/// </summary>
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static DataGridMaskedStringColumn() {
			DataGridBoundColumn.EditingElementStyleProperty.OverrideMetadata(typeof(DataGridMaskedStringColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
			DataGridBoundColumn.ElementStyleProperty.OverrideMetadata(typeof(DataGridMaskedStringColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Generates a <see cref="Style"/> that can be used as the basis for the element styles.
		/// </summary>
		/// <param name="targetType">Type of the target.</param>
		/// <param name="isEditing">if set to <c>true</c> the style will be used for the editing element.</param>
		/// <returns>A <see cref="Style"/> that can be used as the basis for the element styles.</returns>
		private static Style GenerateBaseStyle(Type targetType, bool isEditing) {
			var style = new Style(targetType);
			style.Setters.Add(new Setter(MaskedTextBox.BorderThicknessProperty, new Thickness(0.0)));
			style.Setters.Add(new Setter(MaskedTextBox.MinHeightProperty, 20.0));
			style.Setters.Add(new Setter(MaskedTextBox.PaddingProperty, new Thickness(0.0)));
			style.Setters.Add(new Setter(MaskedTextBox.VerticalContentAlignmentProperty, VerticalAlignment.Center));

			if (!isEditing) {
				style.Setters.Add(new Setter(MaskedTextBox.BackgroundProperty, Brushes.Transparent));
				style.Setters.Add(new Setter(MaskedTextBox.IsHitTestVisibleProperty, false));
				style.Setters.Add(new Setter(MaskedTextBox.IsTabStopProperty, false));
			}

			return style;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Commits the cell edit.
		/// </summary>
		/// <param name="editingElement">The editing element.</param>
		/// <returns></returns>
		protected override bool CommitCellEdit(FrameworkElement editingElement) {
			var textBox = editingElement as MaskedTextBox;
			if (textBox != null) {
				if (!textBox.IsMatched) {
					UpdateBindingTarget(textBox, MaskedTextBox.TextProperty);
					return false;
				}
			}
			
			return base.CommitCellEdit(editingElement);
		}

		/// <summary>
		/// Gets the default value for the <c>EditingElementStyleProperty</c> dependency property.
		/// </summary>
		/// <value>The default value for the <c>EditingElementStyleProperty</c> dependency property.</value>
		public static Style DefaultEditingElementStyle {
			get {
				if (defaultEditingElementStyle == null) {
					defaultEditingElementStyle = GenerateBaseStyle(typeof(MaskedTextBox), true);
					defaultEditingElementStyle.Seal();
				}
				return defaultEditingElementStyle;
			}
		}

		/// <summary>
		/// Gets the default value for the <c>EditingElementStyleProperty</c> dependency property.
		/// </summary>
		/// <value>The default value for the <c>EditingElementStyleProperty</c> dependency property.</value>
		public static Style DefaultElementStyle {
			get {
				if (defaultElementStyle == null) {
					defaultElementStyle = GenerateBaseStyle(typeof(MaskedTextBox), false);
					defaultElementStyle.Seal();
				}
				return defaultElementStyle;
			}
		}

		/// <summary>
		/// Generates an instance of a <c>MaskedTextBox</c> object.
		/// </summary>
		/// <param name="isEditing">Whether the text box will be used for editing.</param>
		/// <param name="cell">The cell.</param>
		/// <returns>An instance of a <c>MaskedTextBox</c> object.</returns>
		protected virtual MaskedTextBox GenerateMaskedTextBox(bool isEditing, DataGridCell cell) {
			MaskedTextBox textBox = null;
			if ((cell != null) && (typeof(MaskedTextBox).IsInstanceOfType(cell.Content)))
				textBox = (MaskedTextBox)cell.Content;

			if (textBox == null)
				textBox = (MaskedTextBox)Activator.CreateInstance(typeof(MaskedTextBox));

			this.ApplyStandardValues(textBox);
			if (isEditing)
				textBox.Style = this.EditingElementStyle;
			else
				textBox.Style = this.ElementStyle;
			this.ApplyBinding(textBox, MaskedTextBox.TextProperty);

			return textBox;
		}

		/// <summary>
		/// Generates the editing element.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="dataItem">The data item.</param>
		/// <returns>The <c>FrameworkElement</c> to use for editing.</returns>
		protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem) {
			return this.GenerateMaskedTextBox(true, cell);
		}

		/// <summary>
		/// Generates the display element.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="dataItem">The data item.</param>
		/// <returns>
		/// The <c>FrameworkElement</c> to use for display.
		/// </returns>
		protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem) {
			return this.GenerateMaskedTextBox(false, cell);
		}
		
		/// <summary>
		/// Prepares the cell for edit.
		/// </summary>
		/// <param name="editingElement">The editing element.</param>
		/// <param name="editingEventArgs">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs) {
			var textBox = editingElement as MaskedTextBox;
			if (textBox == null)
				return null;

			textBox.Focus();
			return textBox.Text;
		}

	}

}
