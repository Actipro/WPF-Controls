using System;
using ActiproSoftware.Text;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UndoRedo {

	/// <summary>
	/// Represents a custom change type for an <see cref="ITextChange"/>.
	/// </summary>
	public class CustomChangeType : ITextChangeType {

		public static readonly ITextChangeType Instance = new CustomChangeType();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the description of the change type, which is displayed in the user interface in undo lists.
		/// </summary>
		/// <value>The description of the change type, which is displayed in the user interface in undo lists.</value>
		public string Description { 
			get {
				return "Append text (this is a custom change type)";
			}
		}

		/// <summary>
		/// Gets the string key that uniquely identifies the change type.
		/// </summary>
		/// <value>The string key that uniquely identifies the change type.</value>
		public string Key { 
			get {
				return "AppendText";
			}
		}

	}

}

