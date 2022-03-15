using System;
using System.Diagnostics;
using System.Threading;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine.Implementation {

	/// <summary>
	/// Tracks a difference between a source and destination.
	/// </summary>
	[DebuggerDisplay("Difference[{Position}]; Kind={Kind}; Length={Length}")]
	public class Difference : IDifference {

		private readonly Lazy<DifferenceCollection> lazyChildren = new Lazy<DifferenceCollection>(() => new DifferenceCollection(), LazyThreadSafetyMode.PublicationOnly);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="Difference"/> class.
		/// </summary>
		/// <param name="kind">The kind of difference.</param>
		public Difference(DifferenceKind kind) : this(kind, null) {}

		/// <summary>
		/// Initializes a new instance of the <see cref="Difference"/> class.
		/// </summary>
		/// <param name="kind">The kind of difference.</param>
		/// <param name="position">The zero-based position of the difference within the source or <c>null</c> if the difference is imaginary.</param>
		public Difference(DifferenceKind kind, int? position) {
			this.Kind = kind;
			this.Position = position;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		IDifferenceCollection IDifference.Children => this.Children;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IDifference.Children"/>
		public DifferenceCollection Children => lazyChildren.Value;

		/// <inheritdoc cref="IDifference.Kind"/>
		public DifferenceKind Kind { get; }

		/// <summary>
		/// Gets or sets the length of the difference.
		/// </summary>
		public int Length { get; set; }

		/// <summary>
		/// Gets or sets the zero-based position of the difference relative to other, non-imaginary differences in the same collection (e.g. line index within a document).
		/// </summary>
		/// <remarks>
		/// The value is <c>null</c> for <see cref="DifferenceKind.Imaginary"/> differences.
		/// </remarks>
		public int? Position { get; set; }

	}
}
