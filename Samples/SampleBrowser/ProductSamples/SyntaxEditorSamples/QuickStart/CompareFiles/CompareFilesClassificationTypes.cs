using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles;

/// <summary>
/// Provides access to the built-in <see cref="IClassificationType"/> instances that are used for UI display items within a <see cref="SyntaxEditor"/>.
/// </summary>
public static class CompareFilesClassificationTypes {

	// NOTE: The following keys are defined to be consistent with keys used by Visual Studio for similar
	//   styles and enable compatibility with importing a *.vssettings file
	private const string DiffAddedKey = "deltadiff.add.line";
	private const string DiffModifiedNewKey = "deltadiff.add.word";
	private const string DiffModifiedOldKey = "deltadiff.remove.word";
	private const string DiffRemovedKey = "deltadiff.remove.line";

	// NOTE: The following have no known Visual Studio equivalent
	private const string DiffImaginaryKey = "deltadiff.imaginary";

	private static IClassificationType? _diffAdded;
	private static IClassificationType? _diffImaginary;
	private static IClassificationType? _diffModifiedNew;
	private static IClassificationType? _diffModifiedOld;
	private static IClassificationType? _diffRemoved;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="IClassificationType"/> for added lines in a file difference view.
	/// </summary>
	public static IClassificationType DifferenceAdded
		=> _diffAdded ??= new ClassificationType(DiffAddedKey, "Diff - Added (Latest)");

	/// <summary>
	/// The <see cref="IClassificationType"/> for imaginary lines in a file difference view.
	/// </summary>
	public static IClassificationType DifferenceImaginary
		=> _diffImaginary ??= new ClassificationType(DiffImaginaryKey, "Diff - Imaginary");

	/// <summary>
	/// The <see cref="IClassificationType"/> for modified lines in a file difference view for the latest version.
	/// </summary>
	public static IClassificationType DifferenceModifiedNew
		=> _diffModifiedNew ??= new ClassificationType(DiffModifiedNewKey, "Diff - Differences (Latest)");

	/// <summary>
	/// The <see cref="IClassificationType"/> for modified lines in a file difference view for the oldest version.
	/// </summary>
	public static IClassificationType DifferenceModifiedOld
		=> _diffModifiedOld ??= new ClassificationType(DiffModifiedOldKey, "Diff - Differences (Oldest)");

	/// <summary>
	/// The <see cref="IClassificationType"/> for removed lines in a file difference view.
	/// </summary>
	public static IClassificationType DifferenceRemoved
		=> _diffRemoved ??= new ClassificationType(DiffRemovedKey, "Diff - Removed (Oldest)");

}
