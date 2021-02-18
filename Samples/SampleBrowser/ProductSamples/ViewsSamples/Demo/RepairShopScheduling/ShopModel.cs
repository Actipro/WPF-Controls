using System.Collections.ObjectModel;

#if WINRT
using ActiproSoftware.UI.Xaml;
#else
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling {

	/// <summary>
	/// Stores information about a repair shop.
	/// </summary>
	public class ShopModel : ObservableObjectBase {

		private ObservableCollection<EmployeeModel> employees = new ObservableCollection<EmployeeModel>();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the collection of shop employees.
		/// </summary>
		/// <value>The collection of shop employees.</value>
		public ObservableCollection<EmployeeModel> Employees {
			get {
				return employees;
			}
		}

	}

}
