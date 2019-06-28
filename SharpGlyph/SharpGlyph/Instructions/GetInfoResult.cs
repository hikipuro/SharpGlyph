using System;
namespace SharpGlyph {
	[Flags]
	public enum GetInfoResult {
		/// <summary>
		/// Version Requirements: All.
		/// </summary>
		Version = 0x000000FF,

		/// <summary>
		/// Version Requirements: All.
		/// </summary>
		GlyphRotation = 0x00000100,

		/// <summary>
		/// Version Requirements: All.
		/// </summary>
		GlyphStretched = 0x00000200,

		/// <summary>
		/// Version Requirements: (&gt;= 5 &amp;&amp; &lt;=32) || (&gt;= 41 &amp;&amp; &lt;= 64).
		/// </summary>
		FontVariations = 0x00000400,

		/// <summary>
		/// Version Requirements: (&gt;= 5 &amp;&amp; &lt;=32) || (&gt;= 41 &amp;&amp; &lt;= 64).
		/// </summary>
		VerticalPhantomPoints = 0x00000800,

		/// <summary>
		/// Version Requirements: (&gt;= 34 &amp;&amp; &lt;= 64).
		/// </summary>
		WindowsFontSmoothingGrayscale = 0x00001000,

		/// <summary>
		/// Version Requirements: (&gt;= 36 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeEnabled = 0x00002000,

		/// <summary>
		/// Version Requirements: (&gt;= 37 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeCompatibleWidthsEnabled = 0x00004000,

		/// <summary>
		/// Version Requirements: (&gt;= 37 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeHorizontalLCDStripeOrientation = 0x00008000,

		/// <summary>
		/// Version Requirements: (&gt;= 37 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeBGRLCDStripeOrder = 0x00010000,

		/// <summary>
		/// Version Requirements: (&gt;= 39 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeSubPixelPositionedTextEnabled = 0x00020000,

		/// <summary>
		/// Version Requirements: (&gt;= 40 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeSymmetricRenderingEnabled = 0x00040000,

		/// <summary>
		/// Version Requirements: (&gt;= 40 &amp;&amp; &lt;= 64).
		/// </summary>
		ClearTypeGrayRenderingEnabled = 0x00080000
	}

	public static class GetInfoResultExtensions {
		public static void ClearVersion(this GetInfoResult result) {
			result &= (GetInfoResult)0x7FFFFF00;
		}

		public static void SetVersion(this GetInfoResult result, int version) {
			version &= 0xFF;
			result &= (GetInfoResult)0x7FFFFF00;
			result |= (GetInfoResult)version;
		}
	}
}
