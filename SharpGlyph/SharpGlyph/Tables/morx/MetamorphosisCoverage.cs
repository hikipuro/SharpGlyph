using System;
namespace SharpGlyph {
	[Flags]
	public enum MetamorphosisCoverage : uint {
		/// <summary>
		/// If set, this subtable will only be applied to vertical text.
		/// If clear, this subtable will only be applied to horizontal text.
		/// </summary>
		Vertical = 0x80000000,

		/// <summary>
		/// If set, this subtable will process glyphs in descending order.
		/// If clear, it will process the glyphs in ascending order.
		/// </summary>
		DescendingOrder = 0x40000000,

		/// <summary>
		/// If set, this subtable will be applied to both horizontal
		/// and vertical text (i.e. the state of bit 0x80000000 is ignored).
		/// </summary>
		HorizontalAndVertical = 0x20000000,

		/// <summary>
		/// If set, this subtable will process glyphs in logical order
		/// (or reverse logical order, depending on the value of bit 0x80000000).
		/// </summary>
		LogicalOrder = 0x10000000,

		/// <summary>
		/// Subtable type.
		/// </summary>
		Type = 0x000000FF,

		/// <summary>
		/// Rearrangement subtable.
		/// </summary>
		TypeRearrangement = 0,

		/// <summary>
		/// Contextual subtable.
		/// </summary>
		TypeContextual = 1,

		/// <summary>
		/// Ligature subtable.
		/// </summary>
		TypeLigature = 2,

		/// <summary>
		/// (Reserved)
		/// </summary>
		TypeReserved = 3,

		/// <summary>
		/// Noncontextual (“swash”) subtable.
		/// </summary>
		TypeNoncontextual = 4,

		/// <summary>
		/// Insertion subtable.
		/// </summary>
		TypeInsertion = 5
	}
}
