using System;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.SalaryReport {

	/// <summary>
	/// Stores data about a salary.
	/// </summary>
	public class SalaryData {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the salary amount.
		/// </summary>
		/// <value>The salary amount.</value>
		public double Amount { get; set; }
		
		/// <summary>
		/// Gets or sets the branch name.
		/// </summary>
		/// <value>The branch name.</value>
		public string BranchName { get; set; }
		
		/// <summary>
		/// Gets or sets the department name.
		/// </summary>
		/// <value>The department name.</value>
		public string DepartmentName { get; set; }

		/// <summary>
		/// Gets or sets the year the employee was hired.
		/// </summary>
		/// <value>The year the employee was hired.</value>
		public int HireYear { get; set; }

		/// <summary>
		/// Gets the hire year set index.
		/// </summary>
		/// <value>The hire year set index.</value>
		public int HireYearSet {
			get {
				return (DateTime.Now.Year - this.HireYear) / 5;
			}
		}

	}

}
