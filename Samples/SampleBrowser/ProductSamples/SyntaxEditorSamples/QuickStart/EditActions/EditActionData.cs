using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditActions;

/// <summary>
/// Stores data about an edit action.
/// </summary>
/// <param name="category">The category.</param>
/// <param name="action">The associated action.</param>
public class EditActionData(string category, IEditAction action) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="IEditAction"/> associated with this data.
	/// </summary>
	public IEditAction Action { get; } = action ?? throw new ArgumentNullException(nameof(action));

	/// <summary>
	/// The category.
	/// </summary>
	public string Category { get; } = category ?? throw new ArgumentNullException(nameof(category));

	/// <summary>
	/// The key that, by default, executes the edit action.
	/// </summary>
	public string? Key { get; set; }

	/// <summary>
	/// The string key that uniquely identifies the <see cref="Action"/>.
	/// </summary>
	public string? Name
		=> Action.Key;

}
