using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSelectiveExpansion;

/// <summary>
/// Represents a custom data factory.
/// </summary>
public class CustomDataFactory : TypeDescriptorFactory {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IPropertyModel CreatePropertyModel(object target, PropertyDescriptor propertyDescriptor, IDataFactoryRequest request) {
		// Ensure the Address property is expanded
		var propertyModel = base.CreatePropertyModel(target, propertyDescriptor, request);
		if (propertyModel.Name == "Address")
			propertyModel.IsExpanded = true;

		return propertyModel;
	}

}
