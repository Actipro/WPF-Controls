namespace ActiproSoftware.Windows.Themes;

/// <summary>
/// Represents a <see cref="ResourceDictionary"/> related to the template resources objects defined in this assembly.
/// </summary>
public sealed partial class BarsMvvmResourceDictionary : ResourceDictionary {

	[ThreadStatic]
	private static BarsMvvmResourceDictionary? _instance;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public BarsMvvmResourceDictionary() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The singleton instance of the resource dictionary.
	/// </summary>
	/// <remarks>
	/// The instance is not shared between threads.
	/// </remarks>
	public static BarsMvvmResourceDictionary Instance
		=> _instance ??= [];

}
