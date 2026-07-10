namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

/// <summary>
/// Represents a base class for data-bound columns for use in a <c>DataGrid</c>.
/// </summary>
public abstract partial class DataGridBoundColumnBase : DataGridBoundColumn {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Compares the default values of the specified metadata.
	/// </summary>
	/// <param name="metadata1">The first metadata.</param>
	/// <param name="metadata2">The second metadata.</param>
	/// <returns><c>true</c> if the the default values of the specified metadata are equal; otherwise, <c>false</c>.</returns>
	private static bool DefaultValuesEqual(PropertyMetadata? metadata1, PropertyMetadata? metadata2) {
		if ((metadata1 is null) || (metadata2 is null))
			return true;
		return Equals(metadata1.DefaultValue, metadata2.DefaultValue);
	}

	/// <summary>
	/// Returns the <c>DependencyProperty</c> associated with the specified name.
	/// </summary>
	/// <param name="ownerType">The owner type.</param>
	/// <param name="propertyName">Name of the property.</param>
	private static DependencyProperty? GetProperty(Type ownerType, string propertyName)
		=> DependencyPropertyDescriptor.FromName(propertyName, ownerType, ownerType)?.DependencyProperty;

	/// <summary>
	/// Notifies that a property changed and the content needs to refresh.
	/// </summary>
	/// <param name="obj">The <see cref="DependencyObject"/> whose property is changed.</param>
	/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
	internal static void NotifyPropertyChangeForRefreshContent(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		=> ((DataGridBoundColumnBase)obj).NotifyPropertyChanged(e.Property.Name);

	/// <summary>
	/// Updates the source of the <c>BindingExpression</c> associated with the specified element/property.
	/// </summary>
	/// <param name="element">The element.</param>
	/// <param name="property">The property.</param>
	internal static void UpdateBindingSource(FrameworkElement element, DependencyProperty property)
		=>  element?.GetBindingExpression(property)?.UpdateSource();

	/// <summary>
	/// Updates the target of the <c>BindingExpression</c> associated with the specified element/property.
	/// </summary>
	/// <param name="element">The element.</param>
	/// <param name="property">The property.</param>
	internal static void UpdateBindingTarget(FrameworkElement element, DependencyProperty property)
		=> element?.GetBindingExpression(property)?.UpdateTarget();

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Associates the binding expression, defined by the <c>Binding</c> property, with the specified target property.
	/// </summary>
	/// <param name="targetElement">The target element.</param>
	/// <param name="targetProperty">The target property.</param>
	protected void ApplyBinding(FrameworkElement targetElement, DependencyProperty targetProperty) {
		if (Binding is { } binding)
			BindingOperations.SetBinding(targetElement, targetProperty, binding);
		else
			BindingOperations.ClearBinding(targetElement, targetProperty);
	}

	/// <summary>
	/// Sets the value of the specified target object and property, based on the specified source property.
	/// </summary>
	/// <param name="sourceProperty">The source property.</param>
	/// <param name="targetElement">The target element.</param>
	/// <param name="targetProperty">The target property.</param>
	protected virtual void ApplyValue(DependencyProperty sourceProperty, FrameworkElement targetElement, DependencyProperty targetProperty) {
		if (sourceProperty is null)
			throw new ArgumentNullException(nameof(sourceProperty));
		if (targetElement is null)
			throw new ArgumentNullException(nameof(targetElement));
		if (targetProperty is null)
			throw new ArgumentNullException(nameof(targetProperty));

		// 8/22/2011 - If the property is not set on the column, then do not pass down to the element (10F-15E35C2E-480E)
		// 1/27/2012 - Fixed issue with column default that differ from the column not getting passed down properly (197-16B573BD-0301)
		if (
			DependencyPropertyHelper.GetValueSource(this, sourceProperty).BaseValueSource == BaseValueSource.Default
			&& DefaultValuesEqual(sourceProperty.GetMetadata(this), targetProperty.GetMetadata(targetElement))
		) {
			targetElement.ClearValue(targetProperty);
		}
		else {
			targetElement.SetValue(targetProperty, GetValue(sourceProperty));
		}
	}

	/// <inheritdoc/>
	// 10/26/2011 - Ensure that changes to the columns are passed down to the underlying editors (http://www.actiprosoftware.com/support/forums/viewforumtopic.aspx?ForumTopicID=6087)
	protected override void RefreshCellContent(FrameworkElement element, string propertyName) {
		if (
			element is DataGridCell { Content: FrameworkElement targetElement }
			&& GetProperty(GetType(), propertyName) is { } sourceProperty
			&& GetProperty(targetElement.GetType(), propertyName) is { } targetProperty
		) {
			ApplyValue(sourceProperty, targetElement, targetProperty);
		}

		// Call the base method
		base.RefreshCellContent(element, propertyName);
	}

}
