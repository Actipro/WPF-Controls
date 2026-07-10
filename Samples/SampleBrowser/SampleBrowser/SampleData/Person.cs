using ActiproSoftware.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.SampleBrowser.SampleData;

/// <summary>
/// Represents a person.
/// </summary>
/// <param name="id">The ID.</param>
/// <param name="lastName">The last name.</param>
/// <param name="firstName">The first name.</param>
/// <param name="emailAddress">The e-mail address.</param>
/// <param name="position">The position.</param>
/// <param name="hireDate">The hire date.</param>
/// <param name="photoUri">The photo URI.</param>
public class Person(int id, string lastName, string firstName, string emailAddress, string position, DateTime hireDate, Uri photoUri) {

	private BitmapImage? _photo;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The e-mail address.
	/// </summary>
	public string EmailAddress { get; } = emailAddress;

	/// <summary>
	/// The first name.
	/// </summary>
	public string FirstName { get; } = firstName;

	/// <summary>
	/// The hire date.
	/// </summary>
	public DateTime HireDate { get; } = hireDate;

	/// <summary>
	/// The last name.
	/// </summary>
	public string LastName { get; } = lastName;

	/// <summary>
	/// The full name.
	/// </summary>
	public string FullName
		=> $"{FirstName} {LastName}";

	/// <summary>
	/// The ID.
	/// </summary>
	public int Id { get; } = id;

	/// <summary>
	/// The photo loaded from the <see cref="PhotoUri"/>.
	/// </summary>
	public ImageSource Photo {
		get {
			if (_photo is null) {
				// Create the ImageSource
				_photo = new BitmapImage();
				_photo.BeginInit();
				_photo.UriSource = PhotoUri;
				_photo.EndInit();

				// Prevent the photo from being adapted for dark themes
				ImageProvider.SetCanAdapt(_photo, false);
			}

			return _photo;
		}
	}

	/// <summary>
	/// The photo URI.
	/// </summary>
	public Uri PhotoUri { get; } = photoUri;

	/// <summary>
	/// The position.
	/// </summary>
	public string Position { get; } = position;

}
