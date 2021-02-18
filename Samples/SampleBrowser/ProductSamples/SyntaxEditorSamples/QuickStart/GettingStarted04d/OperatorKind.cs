using System;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d {

	/// <summary>
	/// Specifies the kind of an operator.
	/// </summary>
	public enum OperatorKind {

		/// <summary>
		/// No valid operator kind.
		/// </summary>
		None,

		/// <summary>
		/// An addition operator.
		/// </summary>
		Addition,

		/// <summary>
		/// A subtraction operator.
		/// </summary>
		Subtraction,
		
		/// <summary>
		/// A multiplication operator.
		/// </summary>
		Multiplication,

		/// <summary>
		/// A division operator.
		/// </summary>
		Division,

		/// <summary>
		/// An equality operator.
		/// </summary>
		Equality,

		/// <summary>
		/// An inequality operator.
		/// </summary>
		Inequality,
		
	}

}
