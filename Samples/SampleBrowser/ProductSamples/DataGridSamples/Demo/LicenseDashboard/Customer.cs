using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace ActiproSoftware.ProductSamples.DataGridSamples.Demo.LicenseDashboard {

	/// <summary>
	/// Represents a simple set of data for demonstration purposes.
	/// </summary>
	public class Customer {

		private static ObservableCollection<Customer> customers = new ObservableCollection<Customer>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <see cref="Customer"/> class.
		/// </summary>
		static Customer() {
			customers.Add(new Customer() {
				Id = 1,
				Name = "Doe, John",
				Email = "jdoe@example.com",
				Phone = "(703) 555-1054",
				LicenseExpiration = DateTime.Now.AddDays(61)
			});
			customers.Add(new Customer() {
				Id = 2,
				Name = "Smith, Jim",
				Email = "jim@example.com",
				Phone = "(458) 555-6548",
				LicenseExpiration = DateTime.Now.AddMonths(1)
			});
			customers.Add(new Customer() {
				Id = 3,
				Name = "Clarke, Jane",
				Email = "jclarke@example.com",
				Phone = "(202) 555-1342",
				LicenseExpiration = DateTime.Now.AddDays(49)
			});
			customers.Add(new Customer() {
				Id = 4,
				Name = "Roberts, Bob",
				Email = "rroberts@example.com",
				Phone = "(703) 555-8977",
				LicenseExpiration = DateTime.Now.AddDays(-7)
			});
			customers.Add(new Customer() {
				Id = 5,
				Name = "Scotts, Samual",
				Email = "sam.scotts@example.com",
				Phone = "(212) 555-5487",
				LicenseExpiration = DateTime.Now.AddDays(20)
			});
			customers.Add(new Customer() {
				Id = 6,
				Name = "Bean, Jason",
				Email = "jbean@example.com",
				Phone = "(267) 555-5678",
				LicenseExpiration = DateTime.Now.AddDays(75)
			});
			customers.Add(new Customer() {
				Id = 7,
				Name = "Hendersion, Eileen",
				Email = "eileen@example.com",
				Phone = "(455) 555-9871",
				LicenseExpiration = DateTime.Now
			});
			customers.Add(new Customer() {
				Id = 8,
				Name = "Killington, Issac",
				Email = "issac@example.com",
				Phone = "(754) 555-5653",
				LicenseExpiration = DateTime.Now.AddDays(600)
			});
			customers.Add(new Customer() {
				Id = 9,
				Name = "Abbott, Robert",
				Email = "rabbott@example.com",
				Phone = "(302) 555-6547",
				LicenseExpiration = DateTime.Now.AddDays(800)
			});
			customers.Add(new Customer() {
				Id = 10,
				Name = "Charles, Will",
				Email = "will@example.com",
				Phone = "(571) 555-2358",
				LicenseExpiration = DateTime.Now.AddDays(10)
			});
		}

		#endregion // OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the customers.
		/// </summary>
		/// <value>The customers.</value>
		public static ObservableCollection<Customer> Customers {
			get { return customers; }
		}

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the date of license expiration.
		/// </summary>
		/// <value>The date of license expiration.</value>
		public DateTime LicenseExpiration { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the phone.
		/// </summary>
		/// <value>The phone.</value>
		public string Phone { get; set; }

		#endregion // PUBLIC PROCEDURES
	}
}
