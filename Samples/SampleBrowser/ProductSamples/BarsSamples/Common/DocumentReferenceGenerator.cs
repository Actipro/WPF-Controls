using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.DocumentManagement;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

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
			LastOpenedDateTime = DateTime.Now.AddDays(-0.3 * manager.Documents.Count).AddMinutes(new Random().NextDouble() * -200),
			IsPinnedRecentDocument = isPinned,
		};

		var fileExt = Path.GetExtension(uri.LocalPath).ToLowerInvariant();
		switch (fileExt) {
			case ".rtf":
				docRef.Description = "Rich-text file";
				docRef.ImageSourceSmall = ImageLoader.GetIcon("RichTextDocument16.png");
				docRef.ImageSourceLarge = ImageLoader.GetIcon("RichTextDocument32.png");
				break;
			default:
				docRef.Description = fileExt.Substring(1).ToUpperInvariant() + " file";
				docRef.ImageSourceSmall = ImageLoader.GetIcon("BlankPage16.png");
				docRef.ImageSourceLarge = ImageLoader.GetIcon("BlankPage32.png");
				break;
		}

		manager.Documents.Add(docRef);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds several sample <see cref="DocumentReference"/> objects to a <see cref="RecentDocumentManager"/>.
	/// </summary>
	/// <param name="manager">The <see cref="RecentDocumentManager"/> to update.</param>
	public static void BindRecentDocumentManager(RecentDocumentManager manager) {
		manager.Documents.BeginUpdate();
		try {
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\EULA.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Resume.rtf"), isPinned: true);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Vacation Planning.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Investment Notes.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Holiday Recipes.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\Release Notes.rtf"), isPinned: true);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q4.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Birthday Gift Ideas.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q3.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Untitled Novel - A story about long file names and their impact on user interfaces.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Downloads\Actipro Software - Getting Started.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Downloads\How to Deliver Faster with Reusable Components.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\Feature Requests.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q2.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Bathroom Remodel Budget.rtf"), isPinned: true);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q1.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Side Project Domain Names.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Sales Presentation Notes.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Wish List.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Privacy Policy.rtf"), isPinned: false);
			AddDocumentReference(manager, new Uri(@"C:\Documents\TODO List.rtf"), isPinned: true); // Oldest document, but pinned for importance
		}
		finally {
			manager.Documents.EndUpdate();
		}
	}

}
