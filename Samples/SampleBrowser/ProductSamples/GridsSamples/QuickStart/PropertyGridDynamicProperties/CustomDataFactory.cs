using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDynamicProperties;

/// <summary>
/// Represents a custom data factory.
/// </summary>
public class CustomDataFactory : TypeDescriptorFactory {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IPropertyModel CreatePropertyModel(object target, PropertyDescriptor propertyDescriptor, IDataFactoryRequest request)
		=> new CustomPropertyModel(target, propertyDescriptor);

}
