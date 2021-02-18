using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted08 {
    
	/// <summary>
	/// Provides information about the <c>Simple</c> context of a certain offset within an <see cref="ITextSnapshot"/>.
	/// </summary>
	public class SimpleContext {

		private int?					argumentIndex;
		private TextSnapshotOffset?		argumentListSnapshotOffset;
		private TextSnapshotOffset?		argumentSnapshotOffset;
		private FunctionDeclaration		containingFunctionDeclaration;
		private TextSnapshotRange?		initializationSnapshotRange;
		private TextSnapshotOffset		snapshotOffset;
		private FunctionDeclaration		targetFunction;
		private SimpleContextType		type							= SimpleContextType.Default;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SimpleContext</c> class.
		/// </summary>
		/// <param name="snapshotOffset">The <see cref="TextSnapshotOffset"/> that indicates the location to examine.</param>
		public SimpleContext(TextSnapshotOffset snapshotOffset) {
			if (snapshotOffset.IsDeleted)
				throw new ArgumentNullException("snapshotOffset");

			// Initialize
			this.snapshotOffset = snapshotOffset;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the index of the current argument, if known, when the context is for the containing invocation.
		/// </summary>
		/// <value>The index of the current argument, if known, when the context is for the containing invocation.</value>
		public int? ArgumentIndex {
			get {
				return argumentIndex;
			}
			set {
				argumentIndex = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TextSnapshotOffset"/> of the argument list, if known, when the context is for the containing invocation.
		/// </summary>
		/// <value>The <see cref="TextSnapshotOffset"/> of the argument list, if known, when the context is for the containing invocation.</value>
		public TextSnapshotOffset? ArgumentListSnapshotOffset { 
			get {
				return argumentListSnapshotOffset;
			}
			set {
				argumentListSnapshotOffset = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TextSnapshotOffset"/> of the current argument, if known, when the context is for the containing invocation.
		/// </summary>
		/// <value>The <see cref="TextSnapshotOffset"/> of the current argument, if known, when the context is for the containing invocation.</value>
		public TextSnapshotOffset? ArgumentSnapshotOffset { 
			get {
				return argumentSnapshotOffset;
			}
			set {
				argumentSnapshotOffset = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="IAstNode"/> for the function declaration that contains the offset, if any.
		/// </summary>
		/// <value>The <see cref="IAstNode"/> for the function declaration that contains the offset, if any.</value>
		public FunctionDeclaration ContainingFunctionDeclaration { 
			get {
				return containingFunctionDeclaration;
			}
			set {
				containingFunctionDeclaration = value;
			}
		}
		
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is equal to the current <see cref="Object"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="Object"/>.</param>
		/// <returns>
		/// true if the specified <see cref="Object"/> is equal to the current <see cref="Object"/>; otherwise, false.
		/// </returns>
		/// <exception cref="NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
		public override bool Equals(object obj) {
			SimpleContext context = obj as SimpleContext;
			if (context != null) {
				// Used for IntelliPrompt quick info
				bool similar = (context.InitializationSnapshotRange == this.InitializationSnapshotRange) &&
					(context.Type == this.Type);
				return similar;
			}
			else
				return false;
		}
			
		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="Object"/>.
		/// </returns>
		public override int GetHashCode() {
			return this.SnapshotOffset.GetHashCode();
		}

		/// <summary>
		/// Gets or sets the <see cref="TextSnapshotRange"/> with which the context was initialized.
		/// </summary>
		/// <value>The <see cref="TextSnapshotRange"/> with which the context was initialized.</value>
		public TextSnapshotRange? InitializationSnapshotRange { 
			get {
				return initializationSnapshotRange;
			}
			set {
				initializationSnapshotRange = value;
			}
		}

		/// <summary>
		/// Gets the <see cref="TextSnapshotOffset"/> for which this context was created.
		/// </summary>
		/// <value>The <see cref="TextSnapshotOffset"/> for which this context was created.</value>
		public TextSnapshotOffset SnapshotOffset {
			get {
				return snapshotOffset;
			}
		}
		
		/// <summary>
		/// Gets or sets an <see cref="IAstNode"/> that specifies the target function for this context, if any.
		/// </summary>
		/// <value>An <see cref="IAstNode"/> that specifies the target function for this context, if any.</value>
		public FunctionDeclaration TargetFunction { 
			get {
				return targetFunction;
			}
			set {
				targetFunction = value;
			}
		}

		/// <summary>
		/// Creates and returns a string representation of the current object.
		/// </summary>
		/// <returns>A string representation of the current object.</returns>
		public override string ToString() {
			return String.Format("SimpleContext[Type={0}]", this.Type);
		}

		/// <summary>
		/// Gets or sets an <see cref="SimpleContextType"/> that specifies the type of context.
		/// </summary>
		/// <value>An <see cref="SimpleContextType"/> that specifies the type of context.</value>
		public SimpleContextType Type { 
			get {
				return type;
			}
			set {
				type = value;
			}
		}

    }
}
