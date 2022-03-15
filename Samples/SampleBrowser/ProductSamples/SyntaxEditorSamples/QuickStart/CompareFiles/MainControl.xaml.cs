using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine.Implementation;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using System.Linq;
using System.Windows;
using System;
using System.Text;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		private bool					ignoreWhiteSpace			= true;
		private bool					isSynchronizingEditorView;
		private IDifferenceCollection	newDifferences;
		private IDifferenceCollection	oldDifferences;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			this.InitializeComponent();

			// Listen for events to be raised by each editor to keep them in sync
			this.AddEditorViewEventHandlers(oldEditor.ActiveView);
			this.AddEditorViewEventHandlers(newEditor.ActiveView);

			// Compare files and live update as files are changed
			Compare();
			oldEditor.DocumentTextChanged += (s, e) => Compare();
			newEditor.DocumentTextChanged += (s, e) => Compare();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Adds event handlers for an editor's view.
		/// </summary>
		/// <param name="editorView">The editor's view.</param>
		private void AddEditorViewEventHandlers(IEditorView editorView) {
			if (editorView == null)
				return;

			// The TextAreaLayout event is raised after a layout has been updated (which can be caused by scrolling) and
			// will be used to trigger synchronization of the views
			editorView.TextAreaLayout += this.OnViewTextAreaLayout;
		}

		/// <summary>
		/// Applies a collection of differences to the given editor.
		/// </summary>
		/// <param name="differences">The collection of differences.</param>
		/// <param name="editor">The editor.</param>
		private void ApplyDifferences(IDifferenceCollection differences, SyntaxEditor editor) {
			// Resolve if the differences are for the latest version
			var isLatest = (editor == newEditor);

			// Get the view for the editor
			var view = editor.ActiveView;

			// Get the tagger for real lines and apply the differences
			var realLinesProvider = editor.Document.GetServices<TextViewTaggerProvider<CompareFilesRealLinesTagger>>().FirstOrDefault();
			var realLinesTagger = realLinesProvider?.GetTagger<RealDifferenceTag>(view) as CompareFilesRealLinesTagger;
			realLinesTagger?.ApplyDifferences(differences, isLatest);

			// Get the tagger for imaginary lines and apply the differences
			var imaginaryLinesProvider = editor.Document.GetServices<TextViewTaggerProvider<CompareFilesImaginaryLinesTagger>>().FirstOrDefault();
			var imaginaryLinesTagger = imaginaryLinesProvider?.GetTagger<IIntraLineSpacerTag>(view) as CompareFilesImaginaryLinesTagger;
			imaginaryLinesTagger?.ApplyDifferences(differences);
		}

		/// <summary>
		/// Compares the old and new documents.
		/// </summary>
		private void Compare() {
			// Get the text of the old and new documents
			var oldText = oldEditor.Document.CurrentSnapshot.Text;
			var newText = newEditor.Document.CurrentSnapshot.Text;

			// Compare the documents
			var differenceBuilder = CreateDifferenceBuilder();
			differenceBuilder.Compare(oldText, newText, IgnoreWhiteSpace);
			oldDifferences = differenceBuilder.OldDifferences;
			newDifferences = differenceBuilder.NewDifferences;

			// Apply the differences to the editor
			ApplyDifferences(oldDifferences, oldEditor);
			ApplyDifferences(newDifferences, newEditor);
		}

		/// <summary>
		/// Creates a new <see cref="IDocumentDifferenceBuilder"/> for comparing files.
		/// </summary>
		/// <returns>A new instance of <see cref="IDocumentDifferenceBuilder"/></returns>.
		private IDocumentDifferenceBuilder CreateDifferenceBuilder() {
			#if DIFFPLEX
			// Use a difference builder based on DiffPlex
			return new DiffPlexDocumentDifferenceBuilder();
			#else
			// Use a custom difference builder created for this sample
			return new DocumentDifferenceBuilder();
			#endif
		}

		/// <summary>
		/// Creates a <see cref="TextViewScrollState"/> that will result in a specific difference index appearing at the top of the view.
		/// </summary>
		/// <param name="view">The view.</param>
		/// <param name="topLineDifferenceIndex">The zero-based index of the difference to appear at the top of the view.</param>
		/// <returns>A new <see cref="TextViewScrollState"/>.</returns>
		private TextViewScrollState CreateScrollStateForTopLineDifferenceIndex(IEditorView view, int topLineDifferenceIndex) {
			// Get the differences associated with the view
			var differences = GetViewDifferences(view);

			// Initialize scroll state
			var scrollState = new TextViewScrollState() {
				CanScrollPastDocumentEnd = true,
				VerticalAnchorPlacement = TextViewVerticalAnchorPlacement.Top,
				VerticalAmountOffset = 0,
			};

			// Quit if the view should be positioned at the very top
			if (topLineDifferenceIndex == 0) {
				scrollState.VerticalAnchorTextPosition = new TextPosition(0, 0);
				return scrollState;
			}

			// NOTE: A view line is composed of a real difference line plus any imaginary difference lines which typically appear
			//		 below it in a spacer. For the first line only, imaginary difference lines may also appear above the real line.
			//		 Multiple, adjacent imaginary difference lines are represented by a single spacer.
			//		 
			//		----------[View Line 0]----------
			//			[Diff 0] Imaginary Difference Spacer for View Line 0 (only the first line can have spacers above the real line)
			//			[Diff 1] Real Difference Text Line
			//			[Diff 2] Imaginary Difference Spacer for View Line 0
			//		----------[View Line 2]----------
			//			[Diff 3] Real Difference Text Line
			//		----------[View Line 3]----------
			//			[Diff 4] Real Difference Text Line
			//			[Diff 5] Imaginary Difference Spacer for View Line 3
			//			[Diff 6] Imaginary Difference Spacer for View Line 3
			//		----------[View Line 4]----------
			//			[Diff 7] Real Difference Text Line
			//			[Diff 8] Imaginary Difference Spacer for View Line 4

			var diffList = differences.ToList();

			// Find the difference line at the desired index
			var differenceLine = differences[topLineDifferenceIndex];

			// Special handling must be given when there are imaginary lines at the top of the document and the scroller
			// is anchored to the first or second line
			//int topImaginaryLinesAdjustment = 0;
			bool hasTopImaginaryLines = ((diffList.Count > 0) && (diffList[0].Kind == DifferenceKind.Imaginary));
			if (hasTopImaginaryLines) {
				// Get the index of the first real difference line
				int firstRealDifferenceIndex = diffList.FindIndex(x => x.Kind != DifferenceKind.Imaginary);

				if (topLineDifferenceIndex <= firstRealDifferenceIndex) {
					// Anchor to the first line
					scrollState.VerticalAnchorTextPosition = new TextPosition(0, 0);

					// Offset the view to scroll the desired line to the top
					scrollState.VerticalAmountOffset = topLineDifferenceIndex * view.DefaultLineHeight;

					// Return the scroll state
					return scrollState;
				}
			}

			bool isImaginary = (differenceLine.Kind == DifferenceKind.Imaginary);
			if (!isImaginary) {
				// Anchor to the real difference line. No vertical offset will be necessary
				// since the top of the view line always corresponds to a real difference line.
				scrollState.VerticalAnchorTextPosition = new TextPosition(differenceLine.Position.Value, 0);
			}
			else {
				// Find the real difference line which appears before the imaginary difference line
				var previousRealDifferenceLineIndex = -1;
				for (int i = topLineDifferenceIndex - 1; i >= 0; i--) {
					if (diffList[i].Kind != DifferenceKind.Imaginary) {
						previousRealDifferenceLineIndex = i;
						break;
					}
				}

				if (previousRealDifferenceLineIndex >= 0) {
					// Anchor to the real difference line
					var realDifferenceLine = diffList[previousRealDifferenceLineIndex];
					scrollState.VerticalAnchorTextPosition = new TextPosition(realDifferenceLine.Position.Value, 0);

					var topLineOffset = 0;
					if (hasTopImaginaryLines && (realDifferenceLine.Position.Value == 0)) {
						// Indicate an offset for the number of imaginary lines (e.g. real line index 2 has 2 imaginary lines above it)
						topLineOffset = previousRealDifferenceLineIndex;
					}

					// Offset the scroller vertically for every difference line that is scrolled off the top of the view port
					// so the desired difference line will appear at the top
					int differenceLineOffset = topLineDifferenceIndex - previousRealDifferenceLineIndex + topLineOffset;
					scrollState.VerticalAmountOffset = (differenceLineOffset) * view.DefaultLineHeight;
				}
			}

			return scrollState;
		}

		/// <summary>
		/// Gets the collection of differences associated with the given <paramref name="view"/>.
		/// </summary>
		/// <param name="view">The view whose differences should be returned.</param>
		/// <returns>The <see cref="IDifferenceCollection"/> for the given <paramref name="view"/>.</returns>
		private IDifferenceCollection GetViewDifferences(IEditorView view) {
			if (view is null)
				throw new ArgumentNullException(nameof(view));

			return (view.SyntaxEditor == oldEditor)
				? oldDifferences
				: newDifferences;
		}

		/// <summary>
		/// Gets the zero-based index of the difference that is displayed on the top line of the given <paramref name="view"/>.
		/// </summary>
		/// <param name="view">The view to examine.</param>
		/// <returns></returns>
		private int GetViewTopLineDifferenceIndex(IEditorView view) {
			// Get the list of differences associated with this view
			var differences = GetViewDifferences(view).ToList();

			// Get the current scroll state
			var scrollState = view.ScrollState;

			// NOTE: A view line is composed of a real difference line plus any imaginary difference lines which typically appear
			//		 below it in a spacer. For the first line only, imaginary difference lines may also appear above the real line.
			//		 Multiple, adjacent imaginary difference lines are represented by a single spacer.
			//		 
			//		----------[View Line 0]----------
			//			[Diff 0] Imaginary Difference Spacer for View Line 0 (only the first line can have spacers above the real line)
			//			[Diff 1] Real Difference Text Line
			//			[Diff 2] Imaginary Difference Spacer for View Line 0
			//		----------[View Line 2]----------
			//			[Diff 3] Real Difference Text Line
			//		----------[View Line 3]----------
			//			[Diff 4] Real Difference Text Line
			//			[Diff 5] Imaginary Difference Spacer for View Line 3
			//			[Diff 6] Imaginary Difference Spacer for View Line 3
			//		----------[View Line 4]----------
			//			[Diff 7] Real Difference Text Line
			//			[Diff 8] Imaginary Difference Spacer for View Line 4

			// Get the index of the view line used to anchor the vertical scroll
			var anchorLineIndex = scrollState.VerticalAnchorTextPosition.Line;

			// Special handling must be given when there are imaginary lines at the top of the document and the scroller
			// is anchored to the first or second line
			int topImaginaryLinesAdjustment = 0;
			bool hasTopImaginaryLines = ((differences.Count > 0) && (differences[0].Kind == DifferenceKind.Imaginary));
			if (hasTopImaginaryLines
				&& (anchorLineIndex == 0)
				&& (scrollState.VerticalAmountOffset == 0)) {
				// Get the number of imaginary lines before the first real line
				var topImaginaryLineCount = differences.TakeWhile(x => x.Kind == DifferenceKind.Imaginary).Count();

				// All imaginary lines are visible above the anchor view line
				topImaginaryLinesAdjustment = topImaginaryLineCount;
			}

			// Find the difference line index of the line represented by the anchor view line
			var anchorLineDiffIndex = differences.FindIndex(diff => diff.Position.HasValue && (diff.Position.Value == anchorLineIndex));

			// The vertical offset is the number of pixels the scroller has offset from the top of the view line represented by the anchor line
			//	- Zero offset means the top of the view port is the same as the top of the anchor view line
			//	- Negative offset means the top of the view port is higher than the anchor view line (e.g. anchor line is below the top of the view port)
			//	- Positive offset means the top of the view port is lower than the anchor view line (e.g. anchor line is scrolled beyond the top of the view port)
			var offsetLineCount = (int)Math.Floor(scrollState.VerticalAmountOffset / view.DefaultLineHeight);

			// Adjust the anchor difference line index to the difference line at the top of the view port
			var topLineDiffIndex = anchorLineDiffIndex + offsetLineCount - topImaginaryLinesAdjustment;
			return topLineDiffIndex;

		}

		/// <summary>
		/// Occurs when the layout of the lines in the text area has been updated.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="TextViewEventArgs"/> event data.</param>
		private void OnViewTextAreaLayout(object sender, TextViewTextAreaLayoutEventArgs e) {
			// Get the view which was updated
			var sourceView = (IEditorView)sender;

			// Get the editor which owns the updated view
			var sourceEditor = sourceView.SyntaxEditor;

			// Get the other editor to be synchronized
			var targetEditor = this.ResolveOtherEditor(sourceEditor);

			// Get the view in the other other which is located at the same position as the source view (to support split views)
			var targetView = targetEditor.GetView(sourceView.Placement);

			// Synchronize the views
			if (sourceView != null && targetView != null)
				this.SynchronizeEditorView(sourceView, targetView);
		}

		/// <summary>
		/// Resolve the editor (old or new) which is different from the reference editor.
		/// </summary>
		/// <param name="referenceEditor">The editor to examine.</param>
		/// <returns>
		/// The old editor when <paramref name="referenceEditor"/> is the new editor or
		/// the new editor when <paramref name="referenceEditor"/> is the old editor.
		/// </returns>
		private SyntaxEditor ResolveOtherEditor(SyntaxEditor referenceEditor) {
			// Return the editor which was not passed as the reference (old returns new, new returns old)
			return (referenceEditor == newEditor)
				? oldEditor
				: newEditor;
		}

		/// <summary>
		/// Updates the the zoom level and scroll state of the target editor view to match the source editor view.
		/// </summary>
		/// <param name="sourceView">The source view whose state is to be copied.</param>
		/// <param name="targetView">The target view to be updated.</param>
		private void SynchronizeEditorView(IEditorView sourceView, IEditorView targetView) {
			// Prevent re-entry
			if (isSynchronizingEditorView)
				return;

			isSynchronizingEditorView = true;
			try {
				// Sync zoom level
				if (targetView.ZoomLevel != sourceView.ZoomLevel)
					targetView.SyntaxEditor.ZoomLevel = sourceView.SyntaxEditor.ZoomLevel;

				// Sync scroll state
				int topDifferenceIndex = GetViewTopLineDifferenceIndex(sourceView);				
				if (topDifferenceIndex >= 0) {
					var targetViewScrollState = CreateScrollStateForTopLineDifferenceIndex(targetView, topDifferenceIndex);
					targetViewScrollState.HorizontalAmount = sourceView.ScrollState.HorizontalAmount;

					if (targetView.ScrollState != targetViewScrollState)
						targetView.Scroller.ScrollTo(targetViewScrollState);
				}
			}
			finally {
				isSynchronizingEditorView = false;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets if insignificant white space should be ignored when comparing documents.
		/// </summary>
		public bool IgnoreWhiteSpace {
			get => ignoreWhiteSpace;
			set {
				// Ignore if the value has not changed
				if (ignoreWhiteSpace == value)
					return;

				// Update the value
				ignoreWhiteSpace = value;

				// Re-compare with new setting
				Compare();
			}
		}

	}

}