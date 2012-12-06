namespace SBIP.Helper
{
    /// <summary> Representing possible types of <see cref="Figure"/>s. 
    /// A <see cref="FigureType"/> characterizes the position of 
    /// a <see cref="Figure"/>.  </summary>
    [System.Serializable]
    public enum FigureType
    {
        /// <summary>Common  <see cref="Figure"/> type. </summary>
        Common,

        /// <summary><see cref="Figure"/> as a line polygon. </summary>
        Line,
        
        /// <summary>Orthogonal to camera (photograph's position) 
        /// placed <see cref="Figure"/> type. </summary>
        Orthogonal,
        
        /// <summary>Same depth like 3D position of image plane. </summary>
        Background
    }
}
