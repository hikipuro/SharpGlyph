using System;
namespace SharpGlyph {
	public enum GetInfoSelector {
		/// <summary>
		/// Version Requirements: All.
		/// </summary>
		Version = 0x00000001,

		/// <summary>
		/// Version Requirements: All.
		/// </summary>
		GlyphRotation = 0x00000002,

		/// <summary>
		/// Version Requirements: All.
		/// </summary>
		GlyphStretched = 0x00000004,

		/// <summary>
		/// Version Requirements: (&gt;= 5 &amp;&amp; &lt;=32) || (&gt;= 41 &amp;&amp; &lt;= 64).
		/// </summary>
		FontVariations = 0x00000008,

		/// <summary>
		/// Version Requirements: (&gt;= 5 &amp;&amp; &lt;=32) || (&gt;= 41 &amp;&amp; &lt;= 64).
		/// </summary>
		VerticalPhantomPoints = 0x00000010,

		/// <summary>
		/// Version Requirements: (&gt;= 34 &amp;&amp; &lt;= 64).
		/// </summary>
		WindowsFontSmoothingGrayscale = 0x00000020,

		/// <summary>
		/// Version Requirements: (&gt;= 36 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeEnabled = 0x00000040,

		/// <summary>
		/// Version Requirements: (&gt;= 37 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeCompatibleWidthsEnabled = 0x00000080,

		/// <summary>
		/// Version Requirements: (&gt;= 37 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeHorizontalLCDStripeOrientation = 0x00000100,

		/// <summary>
		/// Version Requirements: (&gt;= 37 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeBGRLCDStripeOrder = 0x00000200,

		/// <summary>
		/// Version Requirements: (&gt;= 39 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeSubPixelPositionedTextEnabled = 0x00000400,

		/// <summary>
		/// Version Requirements: (&gt;= 40 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeSymmetricRenderingEnabled = 0x00000800,

		/// <summary>
		/// Version Requirements: (&gt;= 40 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeGrayRenderingEnabled = 0x00001000
	}
}
