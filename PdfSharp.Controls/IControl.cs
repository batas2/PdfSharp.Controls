using System;
using PdfSharp.Drawing;

namespace SharpPdf.Controls
{
    /// <summary>
    /// Base control interface
    /// </summary>
    public interface IControl : ICloneable
    {
        /// <summary>
        /// Gets or sets the rect.
        /// </summary>
        /// <value>
        /// Rect, control position relative to parent
        /// </value>
        XRect Rect { get; set; }


        /// <summary>
        /// Draws control
        /// </summary>
        /// <param name="gfx">The GFX.</param>
        void Draw(XGraphics gfx);
    }
}