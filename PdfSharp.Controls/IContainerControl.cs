using System;
using PdfSharp.Drawing;

namespace SharpPdf.Controls
{
    /// <summary>
    /// Container control interface
    /// </summary>
    public interface IContainerControl : IControl
    {
        int MarginLeft { get; set; }
        int MarginRight { get; set; }
        int MarginTop { get; set; }
        int MarginBottom { get; set; }

        /// <summary>
        /// Draws container controls in area [starY; startY + height]s
        /// </summary>
        /// <param name="gfx">The GFX.</param>
        /// <param name="startY">The start Y.</param>
        /// <param name="height">The height.</param>
        void Draw(XGraphics gfx, double startY, double height);
    }
}