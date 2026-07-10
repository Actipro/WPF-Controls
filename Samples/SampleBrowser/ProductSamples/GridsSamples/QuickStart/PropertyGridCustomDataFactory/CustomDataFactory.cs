using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory;

/// <summary>
/// Represents a custom data factory.
/// </summary>
public class CustomDataFactory : TypeDescriptorFactory {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IList<IPropertyModel>? GetPropertyModels(object dataObject, IDataFactoryRequest request) {
		if (dataObject is not CustomerViewModel customer) {
			// Fall back to using the base method's results for nested objects
			return base.GetPropertyModels(dataObject, request);
		}

		// Create a list of property model results
		var propertyModels = new List<IPropertyModel>();

		// Get the property descriptors
		var customerPropertyDescriptors = TypeDescriptor.GetProperties(customer);
		var phoneNumbersPropertyDescriptors = TypeDescriptor.GetProperties(customer.PhoneNumbers!);

		// Add customer name, but don't allow editing of it, even though it's normally not read-only
		var propertyModel = new CustomPropertyModel(customer, customerPropertyDescriptors[nameof(CustomerViewModel.CustomerName)]!) {
			CustomIsValueReadOnly = true
		};
		propertyModels.Add(propertyModel);

		// Add country name with a custom value template
		propertyModel = new CustomPropertyModel(customer, customerPropertyDescriptors[nameof(CustomerViewModel.CountryName)]!) {
			CustomValueTemplateKey = "CountryNameValueTemplate"
		};
		propertyModels.Add(propertyModel);

		// Add voice phone number (routed from child object)
		propertyModels.Add(CreatePropertyModel(customer.PhoneNumbers!, phoneNumbersPropertyDescriptors[nameof(PhoneNumbersViewModel.Voice)]!, request));

		// Add fax phone number (routed from child object)
		propertyModels.Add(CreatePropertyModel(customer.PhoneNumbers!, phoneNumbersPropertyDescriptors[nameof(PhoneNumbersViewModel.Fax)]!, request));

		// Add referred by, which uses some custom standard values supplied on the object itself
		propertyModel = new CustomPropertyModel(customer, customerPropertyDescriptors[nameof(CustomerViewModel.ReferredBy)]!) {
			CustomStandardValues = customer.AvailableReferrals,
			StandardValuesDisplayMemberPath = nameof(ReferralSourceViewModel.Name)
		};
		propertyModels.Add(propertyModel);

		// Add data items dictionary as read-only, also with read-only item values
		propertyModels.Add(new CollectionPropertyDescriptorPropertyModel(customer, customerPropertyDescriptors[nameof(CustomerViewModel.DataItems)]!, isCollectionReadOnly: true, areItemsReadOnly: true));

		return propertyModels;
	}

}
