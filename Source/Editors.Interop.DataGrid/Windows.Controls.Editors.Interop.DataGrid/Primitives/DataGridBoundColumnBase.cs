using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives {

	/// <summary>
	/// Represents a base class for data-bound columns for use in a <c>DataGrid</c>.
	/// </summary>
	public abstract partial class DataGridBoundColumnBase : DataGridBoundColumn {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Compares the default values of the specified metadata.
		/// </summary>
		/// <param name="metadata1">The first metadata.</param>
		/// <param name="metadata2">The second metadata.</param>
		/// <returns><c>true</c> if the the default values of the specified metadata are equal; otherwise, <c>false</c>.</returns>
		private static bool DefaultValuesEqual(PropertyMetadata metadata1, PropertyMetadata metadata2) {
			if ((metadata1 == null) || (metadata2 == null))
				return true;
			return object.Equals(metadata1.DefaultValue, metadata2.DefaultValue);
		}

		/// <summary>
		/// Gets the <c>DependencyProperty</c> associated with the specified name.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>The <c>DependencyProperty</c> associated with the specified name.</returns>
		private DependencyProperty GetProperty(string propertyName) {
			var propertyDescriptor = DependencyPropertyDescriptor.FromName(propertyName, this.GetType(), this.GetType());
			return (propertyDescriptor != null ? propertyDescriptor.DependencyProperty : null);
		}
		
		/// <summary>
		/// Updates the source of the <c>BindingExpression</c> associated with the specified element/property.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="property">The property.</param>
		internal static void UpdateBindingSource(FrameworkElement element, DependencyProperty property) {
			if (element != null) {
				var bindingExpression = element.GetBindingExpression(property);
				if (bindingExpression != null)
					bindingExpression.UpdateSource();
			}
		}

		/// <summary>
		/// Updates the target of the <c>BindingExpression</c> associated with the specified element/property.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="property">The property.</param>
		internal static void UpdateBindingTarget(FrameworkElement element, DependencyProperty property) {
			if (element != null) {
				var bindingExpression = element.GetBindingExpression(property);
				if (bindingExpression != null)
					bindingExpression.UpdateTarget();
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Associates the binding expression, defined by the <c>Binding</c> property, with the specified target property.
		/// </summary>
		/// <param name="targetElement">The target element.</param>
		/// <param name="targetProperty">The target property.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		protected void ApplyBinding(FrameworkElement targetElement, DependencyProperty targetProperty) {
			var binding = this.Binding;
			if (binding != null)
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
			if (sourceProperty == null)
				throw new ArgumentNullException("sourceProperty");
			else if (targetElement == null)
				throw new ArgumentNullException("targetElement");
			else if (targetProperty == null)
				throw new ArgumentNullException("targetProperty");

			// 8/22/2011 - If the property is not set on the column, then do not pass down to the element (10F-15E35C2E-480E)
			// 1/27/2012 - Fixed issue with column default that differ from the column not getting passed down properly (197-16B573BD-0301)
			if (DependencyPropertyHelper.GetValueSource(this, sourceProperty).BaseValueSource == BaseValueSource.Default &&
				DefaultValuesEqual(sourceProperty.GetMetadata(this), targetProperty.GetMetadata(targetElement)))
				targetElement.ClearValue(targetProperty);
			else
				targetElement.SetValue(targetProperty, this.GetValue(sourceProperty));
		}

		/// <summary>
		/// Refreshes the contents of a cell in the column in response to a column property value change.
		/// </summary>
		/// <param name="element">The element returned by the <c>Content</c> property of the cell to refresh.</param>
		/// <param name="propertyName">The name of the column property that has changed.</param>
		// 10/26/2011 - Ensure that changes to the columns are passed down to the underlying editors (http://www.actiprosoftware.com/support/forums/viewforumtopic.aspx?ForumTopicID=6087)
		protected override void RefreshCellContent(FrameworkElement element, string propertyName) {
			var cell = element as DataGridCell;
			if (cell != null) {
				var targetElement = cell.Content as FrameworkElement;
				if (targetElement != null) {
					var property = this.GetProperty(propertyName);
					if (property != null)
						this.ApplyValue(property, targetElement, property);
				}
			}

			// Call the base method
			base.RefreshCellContent(element, propertyName);
		}

	}

}
