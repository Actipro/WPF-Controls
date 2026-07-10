using ActiproSoftware.Security;
using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

/// <summary>
/// Represents a base class for data-bound columns for use in a <c>DataGrid</c> that utilize <c>PartEditBoxBase</c>-derived controls.
/// </summary>
/// <typeparam name="T">The type of value being edited.</typeparam>
public abstract partial class DataGridPartEditBoxColumnBase<T> : DataGridBoundColumnBase {

	private static Style? _defaultElementStyle;
	private static Style? _defaultEditingElementStyle;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridPartEditBoxColumnBase() {
		EditingElementStyleProperty.OverrideMetadata(typeof(DataGridPartEditBoxColumnBase<T>), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
		ElementStyleProperty.OverrideMetadata(typeof(DataGridPartEditBoxColumnBase<T>), new FrameworkPropertyMetadata(DefaultElementStyle));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Generates a <see cref="Style"/> that can be used as the basis for the element styles.
	/// </summary>
	/// <param name="targetType">Type of the target.</param>
	/// <param name="isEditing">if set to <c>true</c> the style will be used for the editing element.</param>
	private static Style GenerateBaseStyle(Type targetType, bool isEditing) {
		var style = new Style(targetType);
		style.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0.0)));
		style.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(0.0)));
		style.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 20.0));

		if (!isEditing) {
			style.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.Transparent));
			style.Setters.Add(new Setter(Control.IsTabStopProperty, false));
			style.Setters.Add(new Setter(UIElement.IsHitTestVisibleProperty, false));
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
	private static bool IsMouseLeftButtonDown(MouseButtonEventArgs args)
		=> (args is not null) && (args.ChangedButton == MouseButton.Left) && (args.ButtonState == MouseButtonState.Pressed);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The default value for the <c>EditingElementStyleProperty</c> dependency property.
	/// </summary>
	public static Style DefaultEditingElementStyle {
		get {
			if (_defaultEditingElementStyle is null) {
				_defaultEditingElementStyle = GenerateBaseStyle(typeof(PartEditBoxBase<T>), isEditing: true);
				_defaultEditingElementStyle.Seal();
			}
			return _defaultEditingElementStyle;
		}
	}

	/// <summary>
	/// The default value for the <c>EditingElementStyleProperty</c> dependency property.
	/// </summary>
	public static Style DefaultElementStyle {
		get {
			if (_defaultElementStyle is null) {
				_defaultElementStyle = GenerateBaseStyle(typeof(PartEditBoxBase<T>), isEditing: false);
				_defaultElementStyle.Seal();
			}
			return _defaultElementStyle;
		}
	}

	/// <summary>
	/// Generates an instance of a <c>PartEditBoxBase</c>-derived object.
	/// </summary>
	/// <param name="isEditing">Whether the edit box will be used for editing.</param>
	/// <param name="cell">The cell.</param>
	/// <returns>An instance of a <c>PartEditBoxBase</c>-derived object.</returns>
	protected virtual PartEditBoxBase<T> GenerateEditBox(bool isEditing, DataGridCell cell) {
		var type = GetEditBoxType();

		var editBox = ((cell?.Content is { } cellContent) && (type.IsInstanceOfType(cellContent)))
			? (PartEditBoxBase<T>)cellContent
			: (PartEditBoxBase<T>)TrustedCodeService.CreateInstance(type);

		ApplyStandardValues(editBox);
		if (isEditing)
			editBox.Style = EditingElementStyle;
		else
			editBox.Style = ElementStyle;
		ApplyBinding(editBox, PartEditBoxBase<T>.ValueProperty);

		return editBox;
	}

	/// <inheritdoc/>
	protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
		=> GenerateEditBox(isEditing: true, cell);

	/// <inheritdoc/>
	protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
		=> GenerateEditBox(isEditing: false, cell);

	/// <summary>
	/// The type of the associated <c>PartEditBoxBase</c>-derived control.
	/// </summary>
	protected abstract Type GetEditBoxType();

	/// <inheritdoc/>
	protected override object? PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs) {
		if (editingElement is not PartEditBoxBase<T> editBox)
			return null;

		editBox.Focus();
		var uneditedValue = editBox.Value;

		if ((editingEventArgs is MouseButtonEventArgs mouseArgs) && IsMouseLeftButtonDown(mouseArgs)) {
			// Declare and implement the filter callback method
			var filterCallback = new HitTestFilterCallback((target) => {
				if (target is DropDownButton { IsVisible: true } element) {
					if (!editBox.IsPopupOpen)
						editBox.IsPopupOpen = true;
					return HitTestFilterBehavior.Stop;
				}
				return HitTestFilterBehavior.Continue;
			});

			// Declare and implement the result callback method, which simply defaults to returning Stop
			var resultCallback = new HitTestResultCallback(_ => {
				return HitTestResultBehavior.Stop;
			});

			// Perform the hit-testing starting with the Breadcrumb control
			VisualTreeHelper.HitTest(editBox, filterCallback, resultCallback, new PointHitTestParameters(mouseArgs.GetPosition(editBox)));
		}

		return uneditedValue;
	}

}
