namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.SalaryReport;

/// <summary>
/// Stores data about a salary.
/// </summary>
public class SalaryData {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The salary amount.
	/// </summary>
	public double Amount { get; set; }

	/// <summary>
	/// The branch name.
	/// </summary>
	public string? BranchName { get; set; }

	/// <summary>
	/// The department name.
	/// </summary>
	public string? DepartmentName { get; set; }

	/// <summary>
	/// The year the employee was hired.
	/// </summary>
	public int HireYear { get; set; }

	/// <summary>
	/// The hire year set index.
	/// </summary>
	public int HireYearSet
		=> (DateTime.Now.Year - HireYear) / 5;

}
