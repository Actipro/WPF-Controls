using System.Collections.Generic;
using System.ComponentModel;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory {

	/// <summary>
	/// Represents a custom data factory.
	/// </summary>
	public class CustomDataFactory : TypeDescriptorFactory {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the <see cref="IPropertyModel"/> collection for the specified data object.
		/// </summary>
		/// <param name="dataObject">The single object to examine.</param>
		/// <param name="request">An <see cref="IDataFactoryRequest"/> that contains data about the request.</param>
		/// <returns>A collection of <see cref="IPropertyModel"/> objects, if the specified data object has any properties.</returns>
		protected override IList<IPropertyModel> GetPropertyModels(object dataObject, IDataFactoryRequest request) {
			var customer = dataObject as CustomerViewModel;
			if (customer != null) {
				// Create a list of property model results
				var propertyModels = new List<IPropertyModel>();

				// Get the property descriptors
				var customerPropertyDescriptors = TypeDescriptor.GetProperties(customer);
				var phoneNumbersPropertyDescriptors = TypeDescriptor.GetProperties(customer.PhoneNumbers);

				// Add customer name, but don't allow editing of it, even though it's normally not read-only
				var propertyModel = new CustomPropertyModel(customer, customerPropertyDescriptors["CustomerName"]);
				propertyModel.CustomIsValueReadOnly = true;
				propertyModels.Add(propertyModel);
				
				// Add country name with a custom value template
				propertyModel = new CustomPropertyModel(customer, customerPropertyDescriptors["CountryName"]);
				propertyModel.CustomValueTemplateKey = "CountryNameValueTemplate";
				propertyModels.Add(propertyModel);
	
				// Add voice phone number (routed from child object)
				propertyModels.Add(this.CreatePropertyModel(customer.PhoneNumbers, phoneNumbersPropertyDescriptors["Voice"], request));

				// Add fax phone number (routed from child object)
				propertyModels.Add(this.CreatePropertyModel(customer.PhoneNumbers, phoneNumbersPropertyDescriptors["Fax"], request));

				// Add referred by, which uses some custom standard values supplied on the object itself
				propertyModel = new CustomPropertyModel(customer, customerPropertyDescriptors["ReferredBy"]);
				propertyModel.CustomStandardValues = customer.AvailableReferrals;
				propertyModel.StandardValuesDisplayMemberPath = "Name";
				propertyModels.Add(propertyModel);
	
				return propertyModels;
			}
			else {
				// Fall back to using the base method's results for nested objects
				return base.GetPropertyModels(dataObject, request);
			}
		}

	}

}
