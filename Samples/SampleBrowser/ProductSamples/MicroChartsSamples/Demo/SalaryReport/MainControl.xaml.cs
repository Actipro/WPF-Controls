using ActiproSoftware.Windows.Controls.MicroCharts;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.SalaryReport;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private readonly List<SalaryData> _salaryDataSet = [];

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
		DataContext = this;

		// Build the data
		var random = new Random(1);
		var branchNames = new string[] { "New York", "Los Angeles", "Chicago", "Miami", "Cleveland", "Detroit" };
		foreach (var branchName in branchNames) {
			var employeeCountBase = 20 + (int)(random.NextDouble() * 100);
			AddSalaryData(random, branchName, "Executive", 80000, 250000, (int)(0.05 * employeeCountBase));
			AddSalaryData(random, branchName, "Human Resources", 40000, 60000, (int)(0.1 * employeeCountBase));
			AddSalaryData(random, branchName, "IT", 50000, 120000, (int)(0.15 * employeeCountBase));
			AddSalaryData(random, branchName, "Legal", 150000, 180000, (int)(0.1 * employeeCountBase));
			AddSalaryData(random, branchName, "Operations", 30000, 60000, (int)(0.4 * employeeCountBase));
			AddSalaryData(random, branchName, "Sales", 60000, 120000, (int)(0.2 * employeeCountBase));
		}
		TargetEmployeeTotalAmount = 35000000;

		bulletGraph.QualitativeRanges.Add(new MicroQualitativeRange() { Value = BulletGraphRange1 });
		bulletGraph.QualitativeRanges.Add(new MicroQualitativeRange() { Value = BulletGraphRange2 });
		bulletGraph.QualitativeRanges.Add(new MicroQualitativeRange());
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

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
		for (var index = 0; index < count; index++) {
			var data = new SalaryData();

			var yearsWithCompany = (int)(rand.NextDouble() * 30);

			data.BranchName = branchName;
			data.DepartmentName = departmentName;
			data.HireYear = DateTime.Today.Year - yearsWithCompany;

			data.Amount = lowAmount + (yearsWithCompany * 1000) + ((highAmount - lowAmount) * rand.NextDouble());

			_salaryDataSet.Add(data);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// All employee salaries.
	/// </summary>
	public IEnumerable<double> AllEmployeeSalaries
		=> _salaryDataSet.Select(data => data.Amount);

	/// <summary>
	/// The total employee salary amount.
	/// </summary>
	public double AllEmployeeTotalAmount
		=> _salaryDataSet.Sum(data => data.Amount);

	/// <summary>
	/// The branch total salary data.
	/// </summary>
	public IEnumerable<Tuple<string, double>> BranchSalaryData {
		get => from data in _salaryDataSet
			   group data.Amount by data.BranchName into g
			   orderby g.Key
			   select Tuple.Create(g.Key, g.Sum());
	}

	/// <summary>
	/// The branch maximum total salary amount.
	/// </summary>
	public double BranchSalaryMaximum
		=> BranchSalaryData.Max(data => data.Item2);

	/// <summary>
	/// The bullet graph maximum.
	/// </summary>
	public double BulletGraphMaximum
		=> TargetEmployeeTotalAmount + 10000000;

	/// <summary>
	/// The bullet graph range 1.
	/// </summary>
	public double BulletGraphRange1
		=> BulletGraphMaximum * 0.5;

	/// <summary>
	/// The bullet graph range 2.
	/// </summary>
	public double BulletGraphRange2
		=> BulletGraphMaximum * 0.75;

	/// <summary>
	/// The department salary data.
	/// </summary>
	public IEnumerable<Tuple<string, IEnumerable<double>, double>> DepartmentSalaryData {
		get => from data in _salaryDataSet
			   group data.Amount by data.DepartmentName into g
			   orderby g.Key
			   select Tuple.Create(g.Key, g.AsEnumerable(), g.Average());
	}

	/// <summary>
	/// The experience total salary data.
	/// </summary>
	public IEnumerable<Tuple<string, IEnumerable<double>>> ExperienceSalaryData {
		get {
			int year = DateTime.Now.Year;
			return [
				Tuple.Create("0-4 years", from data in _salaryDataSet where data.HireYearSet == 0 select data.Amount),
				Tuple.Create("5-9 years", from data in _salaryDataSet where data.HireYearSet == 1 select data.Amount),
				Tuple.Create("10-14 years", from data in _salaryDataSet where data.HireYearSet == 2 select data.Amount),
				Tuple.Create("15-19 years", from data in _salaryDataSet where data.HireYearSet == 3 select data.Amount),
				Tuple.Create("20-24 years", from data in _salaryDataSet where data.HireYearSet == 4 select data.Amount),
				Tuple.Create("25+ years", from data in _salaryDataSet where data.HireYearSet >= 5 select data.Amount),
			];
		}
	}

	/// <summary>
	/// The experience maximum total salary amount.
	/// </summary>
	public double ExperienceSalaryMaximum
		=> ExperienceSalaryData.Max(data => data.Item2.Max());

	/// <summary>
	/// The experience minimum total salary amount.
	/// </summary>
	public double ExperienceSalaryMinimum
		=> ExperienceSalaryData.Min(data => data.Item2.Min());

	/// <summary>
	/// The maximum salary amount.
	/// </summary>
	public double SalaryMaximum
		=> _salaryDataSet.Max(data => data.Amount);

	/// <summary>
	/// The minimum salary amount.
	/// </summary>
	public double SalaryMinimum
		=> _salaryDataSet.Min(data => data.Amount);

	/// <summary>
	/// The target total employee salary amount.
	/// </summary>
	public double TargetEmployeeTotalAmount { get; }

}
