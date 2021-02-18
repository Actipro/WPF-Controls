using System;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted08 {
	
	/// <summary>
	/// Specifies the type of <see cref="SimpleContext"/>.
	/// </summary>
	public enum SimpleContextType {
		
		/// <summary>
		/// No context (in comment, etc.).
		/// </summary>
		None,

		/// <summary>
		/// Default context (between function declarations).
		/// </summary>
		Default,

		/// <summary>
		/// In the header of a function declaration.
		/// </summary>
		FunctionDeclarationHeader,

		/// <summary>
		/// In the block of a function declaration.
		/// </summary>
		FunctionDeclarationBlock,
		
		/// <summary>
		/// In the block of a function declaration, for a function reference.
		/// </summary>
		FunctionReference,

	}

}
