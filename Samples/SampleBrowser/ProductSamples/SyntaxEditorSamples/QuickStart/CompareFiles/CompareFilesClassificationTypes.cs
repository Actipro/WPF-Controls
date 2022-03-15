using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles {

	/// <summary>
	/// Provides access to the built-in <see cref="IClassificationType"/> instances that are used for UI display items within a <see cref="SyntaxEditor"/>.
	/// </summary>
	public static class CompareFilesClassificationTypes {

		// NOTE: The following keys are defined to be consistent with keys used by Visual Studio for similar
		//		 styles and enable compatibility with importing a *.vssettings file
		private const string DiffAddedKey = "deltadiff.add.line";
		private const string DiffModifiedNewKey = "deltadiff.add.word";
		private const string DiffModifiedOldKey = "deltadiff.remove.word";
		private const string DiffRemovedKey = "deltadiff.remove.line";

		// NOTE: The following have no known Visual Studio equivalent
		private const string DiffImaginaryKey = "deltadiff.imaginary";

		private static IClassificationType diffAdded;
		private static IClassificationType diffImaginary;
		private static IClassificationType diffModifiedNew;
		private static IClassificationType diffModifiedOld;
		private static IClassificationType diffRemoved;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> for added lines in a file difference view.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> for an added line.</value>
		public static IClassificationType DifferenceAdded {
			get {
				if (diffAdded == null)
					diffAdded = new ClassificationType(DiffAddedKey, "Diff - Added (Latest)");
				return diffAdded;
			}
		}

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> for imaginary lines in a file difference view.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> for an imaginary line.</value>
		public static IClassificationType DifferenceImaginary {
			get {
				if (diffImaginary == null)
					diffImaginary = new ClassificationType(DiffImaginaryKey, "Diff - Imaginary");
				return diffImaginary;
			}
		}

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> for modified lines in a file difference view for the latest version.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> for a modified line in the latest version.</value>
		public static IClassificationType DifferenceModifiedNew {
			get {
				if (diffModifiedNew == null)
					diffModifiedNew = new ClassificationType(DiffModifiedNewKey, "Diff - Differences (Latest)");
				return diffModifiedNew;
			}
		}

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> for modified lines in a file difference view for the oldest version.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> for a modified line in the oldest version.</value>
		public static IClassificationType DifferenceModifiedOld {
			get {
				if (diffModifiedOld == null)
					diffModifiedOld = new ClassificationType(DiffModifiedOldKey, "Diff - Differences (Oldest)");
				return diffModifiedOld;
			}
		}

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> for removed lines in a file difference view.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> for a removed line.</value>
		public static IClassificationType DifferenceRemoved {
			get {
				if (diffRemoved == null)
					diffRemoved = new ClassificationType(DiffRemovedKey, "Diff - Removed (Oldest)");
				return diffRemoved;
			}
		}

	}

}
