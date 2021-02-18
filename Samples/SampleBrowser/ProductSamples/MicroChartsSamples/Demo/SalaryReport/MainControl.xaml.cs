using System;
using System.Collections.Generic;
using System.Linq;
using ActiproSoftware.Windows.Controls.MicroCharts;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.SalaryReport {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private List<SalaryData> salaryDataSet;
		private double targetTotalSalary;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			this.DataContext = this;
			
			// Build the data
			Random rand = new Random(1);
			string[] branchNames = new string[] { "New York", "Los Angeles", "Chicago", "Miami", "Cleveland", "Detroit" };
			salaryDataSet = new List<SalaryData>();
			foreach (string branchName in branchNames) {
				int employeeCountBase = 20 + (int)(rand.NextDouble() * 100);
				AddSalaryData(rand, branchName, "Executive", 80000, 250000, (int)(0.05 * employeeCountBase));
				AddSalaryData(rand, branchName, "Human Resources", 40000, 60000, (int)(0.1 * employeeCountBase));
				AddSalaryData(rand, branchName, "IT", 50000, 120000, (int)(0.15 * employeeCountBase));
				AddSalaryData(rand, branchName, "Legal", 150000, 180000, (int)(0.1 * employeeCountBase));
				AddSalaryData(rand, branchName, "Operations", 30000, 60000, (int)(0.4 * employeeCountBase));
				AddSalaryData(rand, branchName, "Sales", 60000, 120000, (int)(0.2 * employeeCountBase));
			}
			targetTotalSalary = 35000000;

			bulletGraph.QualitativeRanges.Add(new MicroQualitativeRange() { Value = this.BulletGraphRange1 });
			bulletGraph.QualitativeRanges.Add(new MicroQualitativeRange() { Value = this.BulletGraphRange2 });
			bulletGraph.QualitativeRanges.Add(new MicroQualitativeRange());
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds salary data.
		/// </summary>
		/// <param name="rand">The random number generator.</param>
		/// <param name="branchName">The branch name.</param>
		/// <param name="departmentName">The department name.</param>
		/// <param name="lowAmount">The low amount.</param>
		/// <param name="highAmount">The high amount.</param>
		/// <param name="count">The employee count.</param>
		private void AddSalaryData(Random rand, string branchName, string departmentName, double lowAmount, double highAmount, int count) {
			for (int index = 0; index < count; index++) {
				SalaryData data = new SalaryData();

				int yearsWithCompany = (int)(rand.NextDouble() * 30);

				data.BranchName = branchName;
				data.DepartmentName = departmentName;
				data.HireYear = DateTime.Today.Year - yearsWithCompany;

				data.Amount = lowAmount + (yearsWithCompany * 1000) + ((highAmount - lowAmount) * rand.NextDouble());

				salaryDataSet.Add(data);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets all employee salaries.
		/// </summary>
		/// <value>All employee salaries.</value>
		public IEnumerable<double> AllEmployeeSalaries {
			get {
				return from data in salaryDataSet
					   select data.Amount; 
			}
		}

		/// <summary>
		/// Gets the total employee salary amount.
		/// </summary>
		/// <value>The total employee salary amount.</value>
		public double AllEmployeeTotalAmount {
			get {
				return salaryDataSet.Sum(data => data.Amount);
			}
		}
		
		/// <summary>
		/// Gets the branch total salary data.
		/// </summary>
		/// <value>The branch total salary data.</value>
		public IEnumerable<Tuple<string, double>> BranchSalaryData {
			get {
				return from data in salaryDataSet
					   group data.Amount by data.BranchName into g
					   orderby g.Key
					   select Tuple.Create(g.Key, g.Sum());
			}
		}
		
		/// <summary>
		/// Gets the branch maximum total salary amount.
		/// </summary>
		/// <value>The branch maximum total salary amount.</value>
		public double BranchSalaryMaximum {
			get {
				return this.BranchSalaryData.Max(data => data.Item2);
			}
		}
		
		/// <summary>
		/// Gets the bullet graph maximum.
		/// </summary>
		/// <value>The bullet graph maximum.</value>
		public double BulletGraphMaximum {
			get {
				return targetTotalSalary + 10000000;
			}
		}
		
		/// <summary>
		/// Gets the bullet graph range 1.
		/// </summary>
		/// <value>The bullet graph range 1.</value>
		public double BulletGraphRange1 {
			get {
				return this.BulletGraphMaximum * 0.5;
			}
		}
		
		/// <summary>
		/// Gets the bullet graph range 2.
		/// </summary>
		/// <value>The bullet graph range 2.</value>
		public double BulletGraphRange2 {
			get {
				return this.BulletGraphMaximum * 0.75;
			}
		}
		
		/// <summary>
		/// Gets the department salary data.
		/// </summary>
		/// <value>The department salary data.</value>
		public IEnumerable<Tuple<string, IEnumerable<double>, double>> DepartmentSalaryData {
			get {
				return from data in salaryDataSet
					   group data.Amount by data.DepartmentName into g
					   orderby g.Key
					   select Tuple.Create(g.Key, g.AsEnumerable(), g.Average());
			}
		}
		
		/// <summary>
		/// Gets the experience total salary data.
		/// </summary>
		/// <value>The experience total salary data.</value>
		public IEnumerable<Tuple<string, IEnumerable<double>>> ExperienceSalaryData {
			get {
				int year = DateTime.Now.Year;
				return new Tuple<string, IEnumerable<double>>[] {
					Tuple.Create("0-4 years", from data in salaryDataSet where data.HireYearSet == 0 select data.Amount),
					Tuple.Create("5-9 years", from data in salaryDataSet where data.HireYearSet == 1 select data.Amount),
					Tuple.Create("10-14 years", from data in salaryDataSet where data.HireYearSet == 2 select data.Amount),
					Tuple.Create("15-19 years", from data in salaryDataSet where data.HireYearSet == 3 select data.Amount),
					Tuple.Create("20-24 years", from data in salaryDataSet where data.HireYearSet == 4 select data.Amount),
					Tuple.Create("25+ years", from data in salaryDataSet where data.HireYearSet >= 5 select data.Amount),
				};
			}
		}
		
		/// <summary>
		/// Gets the experience maximum total salary amount.
		/// </summary>
		/// <value>The experience maximum total salary amount.</value>
		public double ExperienceSalaryMaximum {
			get {
				return this.ExperienceSalaryData.Max(data => data.Item2.Max());
			}
		}

		/// <summary>
		/// Gets the experience minimum total salary amount.
		/// </summary>
		/// <value>The experience minimum total salary amount.</value>
		public double ExperienceSalaryMinimum {
			get {
				return this.ExperienceSalaryData.Min(data => data.Item2.Min());
			}
		}

		/// <summary>
		/// Gets the maximum salary amount.
		/// </summary>
		/// <value>The maximum salary amount.</value>
		public double SalaryMaximum {
			get {
				return salaryDataSet.Max(data => data.Amount);
			}
		}

		/// <summary>
		/// Gets the minimum salary amount.
		/// </summary>
		/// <value>The minimum salary amount.</value>
		public double SalaryMinimum {
			get {
				return salaryDataSet.Min(data => data.Amount);
			}
		}
		
		/// <summary>
		/// Gets the target total employee salary amount.
		/// </summary>
		/// <value>The target total employee salary amount.</value>
		public double TargetEmployeeTotalAmount {
			get {
				return targetTotalSalary;
			}
		}
		
	}
}
