using System;

namespace ActiproSoftware.SampleBrowser.SampleData {

	/// <summary>
	/// Represents a person.
	/// </summary>
	public class Person {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>Person</c> class.
		/// </summary>
		/// <param name="id">The ID.</param>
		/// <param name="lastName">The last name.</param>
		/// <param name="firstName">The first name.</param>
		/// <param name="emailAddress">The e-mail address.</param>
		/// <param name="position">The position.</param>
		/// <param name="hireDate">The hire date.</param>
		/// <param name="photoUri">The photo URI.</param>
		public Person(int id, string lastName, string firstName, string emailAddress, string position, DateTime hireDate, Uri photoUri) {
			this.Id = id;
			this.LastName = lastName;
			this.FirstName = firstName;
			this.EmailAddress = emailAddress;
			this.Position = position;
			this.HireDate = hireDate;
			this.PhotoUri = photoUri;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the e-mail address.
		/// </summary>
		/// <value>The e-mail address.</value>
		public string EmailAddress { get; private set; }
		
		/// <summary>
		/// Gets the first name.
		/// </summary>
		/// <value>The first name.</value>
		public string FirstName { get; private set; }
		
		/// <summary>
		/// Gets the hire date.
		/// </summary>
		/// <value>The hire date.</value>
		public DateTime HireDate { get; private set; }

		/// <summary>
		/// Gets the last name.
		/// </summary>
		/// <value>The last name.</value>
		public string LastName { get; private set; }

		/// <summary>
		/// Gets the full name.
		/// </summary>
		/// <value>The full name.</value>
		public string FullName => this.FirstName + " " + this.LastName;

		/// <summary>
		/// Gets the ID.
		/// </summary>
		/// <value>The ID.</value>
		public int Id { get; private set; }

		/// <summary>
		/// Gets the photo URI.
		/// </summary>
		/// <value>The photo URI.</value>
		public Uri PhotoUri { get; private set; }
		
		/// <summary>
		/// Gets the position.
		/// </summary>
		/// <value>The position.</value>
		public string Position { get; private set; }

	}

}
