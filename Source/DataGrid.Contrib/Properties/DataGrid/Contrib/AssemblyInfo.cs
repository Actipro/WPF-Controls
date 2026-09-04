namespace ActiproSoftware.Properties.DataGrid.Contrib;

/// <summary>
/// Retrieves information about the assembly.
/// </summary>
public sealed partial class AssemblyInfo : UIAssemblyInfoBase {

	private static AssemblyInfo? _instance;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	private AssemblyInfo() { }

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The instance of the <see cref="AssemblyInfo"/> class for this assembly.
	/// </summary>
	public static AssemblyInfo Instance => _instance ??= new();

	/// <inheritdoc/>
	public sealed override int ProductId => 0x0;

}
