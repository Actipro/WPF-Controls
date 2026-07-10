using ActiproSoftware.Windows.DocumentManagement;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Common;

/// <summary>
/// Generates document references for use in samples.
/// </summary>
public static class DocumentReferenceGenerator {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds a <see cref="DocumentReference"/> to the <see cref="RecentDocumentManager"/>.
	/// </summary>
	/// <param name="manager">The <see cref="RecentDocumentManager"/> to update.</param>
	/// <param name="uri">The <see cref="Uri"/>.</param>
	/// <param name="isPinned">Whether it is pinned.</param>
	private static void AddDocumentReference(RecentDocumentManager manager, Uri uri, bool isPinned) {
		var docRef = new DocumentReference(uri) {
			LastOpenedDateTime = DateTime.Now.AddDays(-1 * manager.Documents.Count),
			IsPinnedRecentDocument = isPinned,
			Description = "Rich-text file",
			ImageSourceSmall = new BitmapImage(new Uri("/Images/Icons/RichTextDocument16.png", UriKind.Relative)),
			ImageSourceLarge = new BitmapImage(new Uri("/Images/Icons/RichTextDocument32.png", UriKind.Relative))
		};
		manager.Documents.Add(docRef);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds a number of <see cref="DocumentReference"/> objects to a <see cref="RecentDocumentManager"/>.
	/// </summary>
	/// <param name="manager">The <see cref="RecentDocumentManager"/> to update.</param>
	public static void BindRecentDocumentManager(RecentDocumentManager manager) {
		manager.Documents.BeginUpdate();
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\EULA.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Resume.rtf"), true);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Vacation Planning.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Investment Notes.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Holiday Recipes.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\Release Notes.rtf"), true);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q4.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Birthday Gift Ideas.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q3.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Untitled Novel - A story about long file names and their impact on user interfaces.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Downloads\Actipro Software - Getting Started.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Downloads\How to Deliver Faster with Reusable Components.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\Feature Requests.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q2.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Bathroom Remodel Budget.rtf"), true);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q1.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Side Project Domain Names.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Sales Presentation Notes.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Wish List.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Privacy Policy.rtf"), false);
		AddDocumentReference(manager, new Uri(@"C:\Documents\TODO List.rtf"), true); // Oldest document, but pinned for importance
		manager.Documents.EndUpdate();
	}

}
