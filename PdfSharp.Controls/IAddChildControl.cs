using System.Collections.Generic;

namespace SharpPdf.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAddChildControl : IControl
    {
        /// <summary>
        /// Gets the controls.
        /// </summary>
        ICollection<IControl> Controls { get; }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="control">The control.</param>
        void AddChild(IControl control);
    }
}