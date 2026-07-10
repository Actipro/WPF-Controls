using ActiproSoftware.ProductSamples.GridsSamples.Common;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox;

/// <summary>
/// Provides a base tree node model for use with a Toolbox.
/// </summary>
public abstract class ToolboxTreeNodeModel : TreeNodeModel {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ToolboxTreeNodeModel() {
		// Set default toolbox node properties
		IsDraggable = DefaultIsDraggable;
		IsEditable = false;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The text to be assigned to an <see cref="IDataObject"/> for the <see cref="DataFormats.Text"/> format.
	/// </summary>
	public virtual string DataObjectText
		=> Name;

	/// <summary>
	/// The default value to be assigned to <see cref="TreeNodeModel.IsDraggable"/>.
	/// </summary>
	protected virtual bool DefaultIsDraggable
		=> false;

	/// <summary>
	/// Executes the specified delegate asynchronously, at the specified priority, using the <see cref="Dispatcher" />
	/// for the current thread.
	/// </summary>
	/// <param name="action">The action to be performed.</param>
	/// <param name="priority">
	/// The priority, relative to the other pending operations in the <see cref="Dispatcher"/> event queue, the specified action is invoked.
	/// </param>
	protected static void DispatcherBeginInvoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
		=> Dispatcher.CurrentDispatcher.BeginInvoke(action, priority);

}
