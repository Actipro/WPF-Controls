namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Specifies the kind of numbering.
	/// </summary>
	public enum NumberingKind {
		
		/// <summary>
		/// No numbering.
		/// </summary>
		None,

		/// <summary>
		/// <c>1</c>, <c>2</c>, <c>3</c>, etc.
		/// </summary>
		ArabicNumeral,

		/// <summary>
		/// <c>I</c>, <c>II</c>, <c>III</c>, etc.
		/// </summary>
		UpperRomanNumeral,

		/// <summary>
		/// <c>i</c>, <c>ii</c>, <c>iii</c>, etc.
		/// </summary>
		LowerRomanNumeral,
		
		/// <summary>
		/// <c>A</c>, <c>B</c>, <c>C</c>, etc.
		/// </summary>
		UpperAlpha,

		/// <summary>
		/// <c>a</c>, <c>b</c>, <c>c</c>, etc.
		/// </summary>
		LowerAlpha,

	}

}
