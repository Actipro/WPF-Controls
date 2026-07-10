using ActiproSoftware.Text;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UndoRedo;

/// <summary>
/// Represents a custom change type for an <see cref="ITextChange"/>.
/// </summary>
public class CustomChangeType : ITextChangeType {

	public static readonly ITextChangeType Instance = new CustomChangeType();

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="ITextChangeType.Description"/>
	public string Description
		=> "Append text (this is a custom change type)";

	/// <summary>
	/// The string key that uniquely identifies the change type.
	/// </summary>
	public string Key
		=> "AppendText";

}
