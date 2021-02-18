using System;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling {

	/// <summary>
	/// Stores information about an employee service task.
	/// </summary>
	public class ServiceModel : TaskModelBase {

		private string		description;
		private DateTime	dueDate;
		private int			itemCount;
		private int			itemNumber;
		private string		orderNumber;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>ServiceModel</c> class.
		/// </summary>
		/// <param name="description">The description.</param>
		/// <param name="orderNumber">The order number.</param>
		/// <param name="itemNumber">The item number.</param>
		/// <param name="itemCount">The item count.</param>
		/// <param name="dueDate">The due date.</param>
		/// <param name="hours">The number of hours for the task to complete.</param>
		public ServiceModel(string description, string orderNumber, int itemNumber, int itemCount, DateTime dueDate, int hours) : base(hours) {
			this.Description = description;
			this.OrderNumber = orderNumber;
			this.ItemNumber = itemNumber;
			this.ItemCount = itemCount;
			this.DueDate = dueDate;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description {
			get {
				return description;
			}
			set {
				if (description != value) {
					description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>The due date.</value>
		public DateTime DueDate {
			get {
				return dueDate;
			}
			set {
				if (dueDate != value) {
					dueDate = value;
					this.NotifyPropertyChanged("DueDate");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the item count.
		/// </summary>
		/// <value>The item count.</value>
		public int ItemCount {
			get {
				return itemCount;
			}
			set {
				if (itemCount != value) {
					itemCount = value;
					this.NotifyPropertyChanged("ItemCount");
				}
			}
		}

		/// <summary>
		/// Gets or sets the item number.
		/// </summary>
		/// <value>The item number.</value>
		public int ItemNumber {
			get {
				return itemNumber;
			}
			set {
				if (itemNumber != value) {
					itemNumber = value;
					this.NotifyPropertyChanged("ItemNumber");
				}
			}
		}

		/// <summary>
		/// Gets the name of the service.
		/// </summary>
		/// <value>The name of the service.</value>
		public override string Name {
			get {
				return this.Description;
			}
		}
		
		/// <summary>
		/// Gets or sets the order number.
		/// </summary>
		/// <value>The order number.</value>
		public string OrderNumber {
			get {
				return orderNumber;
			}
			set {
				if (orderNumber != value) {
					orderNumber = value;
					this.NotifyPropertyChanged("OrderNumber");
				}
			}
		}

	}

}
