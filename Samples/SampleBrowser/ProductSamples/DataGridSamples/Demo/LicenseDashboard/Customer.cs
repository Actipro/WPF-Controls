namespace ActiproSoftware.ProductSamples.DataGridSamples.Demo.LicenseDashboard;

/// <summary>
/// Represents a simple set of data for demonstration purposes.
/// </summary>
public class Customer {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

/// <summary>
	/// Initializes the class.
	/// </summary>
	static Customer() {
		Customers.Add(new Customer() {
			Id = 1,
			Name = "Doe, John",
			Email = "jdoe@example.com",
			Phone = "(703) 555-1054",
			LicenseExpiration = DateTime.Now.AddDays(61)
		});
		Customers.Add(new Customer() {
			Id = 2,
			Name = "Smith, Jim",
			Email = "jim@example.com",
			Phone = "(458) 555-6548",
			LicenseExpiration = DateTime.Now.AddMonths(1)
		});
		Customers.Add(new Customer() {
			Id = 3,
			Name = "Clarke, Jane",
			Email = "jclarke@example.com",
			Phone = "(202) 555-1342",
			LicenseExpiration = DateTime.Now.AddDays(49)
		});
		Customers.Add(new Customer() {
			Id = 4,
			Name = "Roberts, Bob",
			Email = "rroberts@example.com",
			Phone = "(703) 555-8977",
			LicenseExpiration = DateTime.Now.AddDays(-7)
		});
		Customers.Add(new Customer() {
			Id = 5,
			Name = "Scotts, Samual",
			Email = "sam.scotts@example.com",
			Phone = "(212) 555-5487",
			LicenseExpiration = DateTime.Now.AddDays(20)
		});
		Customers.Add(new Customer() {
			Id = 6,
			Name = "Bean, Jason",
			Email = "jbean@example.com",
			Phone = "(267) 555-5678",
			LicenseExpiration = DateTime.Now.AddDays(75)
		});
		Customers.Add(new Customer() {
			Id = 7,
			Name = "Hendersion, Eileen",
			Email = "eileen@example.com",
			Phone = "(455) 555-9871",
			LicenseExpiration = DateTime.Now
		});
		Customers.Add(new Customer() {
			Id = 8,
			Name = "Killington, Issac",
			Email = "issac@example.com",
			Phone = "(754) 555-5653",
			LicenseExpiration = DateTime.Now.AddDays(600)
		});
		Customers.Add(new Customer() {
			Id = 9,
			Name = "Abbott, Robert",
			Email = "rabbott@example.com",
			Phone = "(302) 555-6547",
			LicenseExpiration = DateTime.Now.AddDays(800)
		});
		Customers.Add(new Customer() {
			Id = 10,
			Name = "Charles, Will",
			Email = "will@example.com",
			Phone = "(571) 555-2358",
			LicenseExpiration = DateTime.Now.AddDays(10)
		});
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The customers.
	/// </summary>
	public static ObservableCollection<Customer> Customers { get; } = [];

	/// <summary>
	/// The email.
	/// </summary>
	public string? Email { get; set; }

	/// <summary>
	/// The identifier.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// The date of license expiration.
	/// </summary>
	public DateTime LicenseExpiration { get; set; }

	/// <summary>
	/// The name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// The phone.
	/// </summary>
	public string? Phone { get; set; }

}
