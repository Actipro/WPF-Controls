using ActiproSoftware.ProductSamples.GridsSamples.Common;
using System;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox {

	/// <summary>
	/// Provides a base tree node model for use with a Toolbox.
	/// </summary>
	public abstract class ToolboxTreeNodeModel : TreeNodeModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolboxTreeNodeModel"/> class.
		/// </summary>
		public ToolboxTreeNodeModel() {
			// Set default toolbox node properties
			IsDraggable = DefaultIsDraggable;
			IsEditable = false;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the text to be assigned to a <see cref="System.Windows.IDataObject"/> for the <see cref="System.Windows.DataFormats.Text"/> format.
		/// </summary>
		public virtual string DataObjectText {
			get {
				return this.Name;
			}
		}

		/// <summary>
		/// Gets the default value to be assigned to <see cref="TreeNodeModel.IsDraggable"/>.
		/// </summary>
		protected virtual bool DefaultIsDraggable {
			get {
				return false;
			}
		}

		/// <summary>
		/// Executes the specified delegate asynchronously, at the specified priority, using the <see cref="Dispatcher" />
		/// for the current thread.
		/// </summary>
		/// <param name="action">The action to be performed.</param>
		/// <param name="priority">
		/// The priority, relative to the other pending operations in the <see cref="Dispatcher"/> event queue, the specified action is invoked.
		/// </param>
		protected void DispatcherBeginInvoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal) {
			Dispatcher.CurrentDispatcher.BeginInvoke(action, priority);
		}

	}

}
