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
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\EULA.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Resume\Resume-2008.rtf"), true);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Resume\Resume-2006.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Resume\Resume-2005.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Resume\Resume-2004.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Actipro Software Notes.rtf"), true);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 1.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 2.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\A document with a very long filename that should be trimmed.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 3.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 4.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 5.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Actipro Software Products.rtf"), true);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 6.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 7.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 8.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 9.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 10.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 11.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Another document 12.rtf"), false);
			DocumentReferenceGenerator.AddDocumentReference(manager, new Uri(@"C:\Documents\Document opened a long time ago but is pinned.rtf"), true);
			manager.Documents.EndUpdate();
		}

	}
}

