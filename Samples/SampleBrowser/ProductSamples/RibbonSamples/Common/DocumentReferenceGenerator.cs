using System;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.DocumentManagement;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Common {

	/// <summary>
	/// Generates document references for use in samples.
	/// </summary>
	public static class DocumentReferenceGenerator {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Adds a <see cref="DocumentReference"/> to the <see cref="RecentDocumentManager"/>.
		/// </summary>
		/// <param name="manager">The <see cref="RecentDocumentManager"/> to update.</param>
		/// <param name="uri">The <see cref="Uri"/>.</param>
		/// <param name="isPinned">Whether it is pinned.</param>
		private static void AddDocumentReference(RecentDocumentManager manager, Uri uri, bool isPinned) {
			DocumentReference docRef = new DocumentReference(uri);
			docRef.LastOpenedDateTime = DateTime.Now.AddDays(-1 * manager.Documents.Count);;
			docRef.IsPinnedRecentDocument = isPinned;
			docRef.Description = "Rich-text file";
			docRef.ImageSourceSmall = new BitmapImage(new Uri("/Images/Icons/RichTextDocument16.png", UriKind.Relative));
			docRef.ImageSourceLarge = new BitmapImage(new Uri("/Images/Icons/RichTextDocument32.png", UriKind.Relative));
			manager.Documents.Add(docRef);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Adds a number of <see cref="DocumentReference"/> objects to a <see cref="RecentDocumentManager"/>.
		/// </summary>
		/// <param name="manager">The <see cref="RecentDocumentManager"/> to update.</param>
		public static void BindRecentDocumentManager(RecentDocumentManager manager) {
			manager.Documents.BeginUpdate();
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\EULA.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Resume.rtf"), true);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Vacation Planning.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Investment Notes.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Holiday Recipies.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\Release Notes.rtf"), true);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q4.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Birthday Gift Ideas.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q3.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Untitled Novel - A story about long file names and their impact on user interfaces.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Downloads\Actipro Software - Getting Started.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Downloads\How to Deliver Faster with Reusable Components.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Software\Feature Requests.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q2.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Bathroom Remodel Budget.rtf"), true);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Financial Report Q1.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Side Project Domain Names.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Sales Presentation Notes.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Personal\Wish List.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Work\Privacy Policy.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\TODO List.rtf"), true); // Oldest document, but pinned for importance
			manager.Documents.EndUpdate();
		}

	}
}

