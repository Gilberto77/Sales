// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Infrastructure type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Sales.Backend
{
    using System.Diagnostics;

    /// <summary>
    /// Defines the App type.
    /// </summary>
    public static class Infrastructure
    {
	    /// <summary>
        /// Initializes this instance.
        /// </summary>
		public static void Init()
		{
			Debug.WriteLine("Infrastructure::Init");
		}
    }
}
