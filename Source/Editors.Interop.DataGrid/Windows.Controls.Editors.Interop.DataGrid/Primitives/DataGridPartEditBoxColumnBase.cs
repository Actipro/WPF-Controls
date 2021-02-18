using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives {

	/// <summary>
	/// Represents a base class for data-bound columns for use in a <c>DataGrid</c> that utilize <c>PartEditBoxBase</c>-derived controls.
	/// </summary>
	/// <typeparam name="T">The type of value being edited.</typeparam>
	public abstract partial class DataGridPartEditBoxColumnBase<T> : DataGridBoundColumnBase {

		private static Style defaultElementStyle;
		private static Style defaultEditingElementStyle;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <c>DataGridPartEditBoxColumnBase</c> class.
		/// </summary>
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static DataGridPartEditBoxColumnBase() {
			DataGridBoundColumn.EditingElementStyleProperty.OverrideMetadata(typeof(DataGridPartEditBoxColumnBase<T>), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
			DataGridBoundColumn.ElementStyleProperty.OverrideMetadata(typeof(DataGridPartEditBoxColumnBase<T>), new FrameworkPropertyMetadata(DefaultElementStyle));
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
			style.Setters.Add(new Setter(PartEditBoxBase<T>.BorderThicknessProperty, new Thickness(0.0)));
			style.Setters.Add(new Setter(PartEditBoxBase<T>.MinHeightProperty, 20.0));
			style.Setters.Add(new Setter(PartEditBoxBase<T>.PaddingProperty, new Thickness(0.0)));

			if (!isEditing) {
				style.Setters.Add(new Setter(PartEditBoxBase<T>.BackgroundProperty, Brushes.Transparent));
				style.Setters.Add(new Setter(PartEditBoxBase<T>.IsHitTestVisibleProperty, false));
				style.Setters.Add(new Setter(PartEditBoxBase<T>.IsTabStopProperty, false));
			}

			return style;
		}

		/// <summary>
		/// Determines whether the left mouse button is down based on the specified <see cref="MouseButtonEventArgs"/>.
		/// </summary>
		/// <param name="args">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
		/// <returns>
		/// <c>true</c> if the left mouse button is down based on the specified <see cref="MouseButtonEventArgs"/>; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsMouseLeftButtonDown(MouseButtonEventArgs args) {
			return ((args != null) && (args.ChangedButton == MouseButton.Left) && (args.ButtonState == MouseButtonState.Pressed));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the default value for the <c>EditingElementStyleProperty</c> dependency property.
		/// </summary>
		/// <value>The default value for the <c>EditingElementStyleProperty</c> dependency property.</value>
		[SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static Style DefaultEditingElementStyle {
			get {
				if (defaultEditingElementStyle == null) {
					defaultEditingElementStyle = GenerateBaseStyle(typeof(PartEditBoxBase<T>), true);
					defaultEditingElementStyle.Seal();
				}
				return defaultEditingElementStyle;
			}
		}

		/// <summary>
		/// Gets the default value for the <c>EditingElementStyleProperty</c> dependency property.
		/// </summary>
		/// <value>The default value for the <c>EditingElementStyleProperty</c> dependency property.</value>
		[SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static Style DefaultElementStyle {
			get {
				if (defaultElementStyle == null) {
					defaultElementStyle = GenerateBaseStyle(typeof(PartEditBoxBase<T>), false);
					defaultElementStyle.Seal();
				}
				return defaultElementStyle;
			}
		}

		/// <summary>
		/// Generates an instance of a <c>PartEditBoxBase</c>-derived object.
		/// </summary>
		/// <param name="isEditing">Whether the edit box will be used for editing.</param>
		/// <param name="cell">The cell.</param>
		/// <returns>An instance of a <c>PartEditBoxBase</c>-derived object.</returns>
		protected virtual PartEditBoxBase<T> GenerateEditBox(bool isEditing, DataGridCell cell) {
			var type = this.GetEditBoxType();

			PartEditBoxBase<T> editBox = null;
			if ((cell != null) && (type.IsInstanceOfType(cell.Content)))
				editBox = (PartEditBoxBase<T>)cell.Content;

			if (editBox == null)
				editBox = (PartEditBoxBase<T>)Activator.CreateInstance(type);

			this.ApplyStandardValues(editBox);
			if (isEditing)
				editBox.Style = this.EditingElementStyle;
			else
				editBox.Style = this.ElementStyle;
			this.ApplyBinding(editBox, PartEditBoxBase<T>.ValueProperty);

			return editBox;
		}

		/// <summary>
		/// Generates the editing element.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="dataItem">The data item.</param>
		/// <returns>The <c>FrameworkElement</c> to use for editing.</returns>
		protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem) {
			return this.GenerateEditBox(true, cell);
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
			return this.GenerateEditBox(false, cell);
		}

		/// <summary>
		/// Gets the type of the associated <c>PartEditBoxBase</c>-derived control.
		/// </summary>
		/// <returns>The type of the associated <c>PartEditBoxBase</c>-derived control.</returns>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected abstract Type GetEditBoxType();
		
		/// <summary>
		/// Prepares the cell for edit.
		/// </summary>
		/// <param name="editingElement">The editing element.</param>
		/// <param name="editingEventArgs">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs) {
			var editBox = editingElement as PartEditBoxBase<T>;
			if (editBox == null)
				return null;

			editBox.Focus();
			var uneditedValue = editBox.Value;

			var mouseArgs = editingEventArgs as MouseButtonEventArgs;
			if (IsMouseLeftButtonDown(mouseArgs)) {
				// Declare and implement the filter callback method
				HitTestFilterCallback filterCallback = new HitTestFilterCallback(delegate(DependencyObject target) {
					FrameworkElement element = target as FrameworkElement;
					if ((element != null) && (element.IsVisible) && (element is DropDownButton)) {
						if (!editBox.IsPopupOpen)
							editBox.IsPopupOpen = true;
						return HitTestFilterBehavior.Stop;
					}
					return HitTestFilterBehavior.Continue;
				});

				// Declare and implement the result callback method, which simply defaults to returning Stop
				HitTestResultCallback resultCallback = new HitTestResultCallback(delegate(HitTestResult result) {
					return HitTestResultBehavior.Stop;
				});

				// Perform the hit-testing starting with the Breadcrumb control
				VisualTreeHelper.HitTest(editBox, filterCallback, resultCallback, new PointHitTestParameters(mouseArgs.GetPosition(editBox)));
			}

			return uneditedValue;
		}

	}

}
