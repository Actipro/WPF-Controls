using System.ComponentModel;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSelectiveExpansion {

	/// <summary>
	/// Represents a custom data factory.
	/// </summary>
	public class CustomDataFactory : TypeDescriptorFactory {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates an <see cref="IPropertyModel"/> for the specified mutable data object.
		/// </summary>
		/// <param name="target">The target object that owns the property.</param>
		/// <param name="propertyDescriptor">The <see cref="PropertyDescriptor"/> for the property be accessed on the <paramref name="target"/>.</param>
		/// <param name="request">An <see cref="IDataFactoryRequest"/> that contains data about the request.</param>
		/// <returns>The <see cref="IPropertyModel"/> that was created.</returns>
		protected override IPropertyModel CreatePropertyModel(object target, PropertyDescriptor propertyDescriptor, IDataFactoryRequest request) {
			// Ensure the Address property is expanded
			var propertyModel = base.CreatePropertyModel(target, propertyDescriptor, request);
			if ((propertyModel != null) && (propertyModel.Name == "Address"))
				propertyModel.IsExpanded = true;

			return propertyModel;
		}

	}

}
