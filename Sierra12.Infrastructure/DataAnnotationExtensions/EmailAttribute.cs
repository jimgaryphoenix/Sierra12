namespace ProgressTen.Infrastructure.DataAnnotationExtensions
{
    using System.Text.RegularExpressions;

    public class EmailAttribute : RegexAttribute
    {
        #region Constructors and Destructors

        public EmailAttribute()
			: base(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase)
        {
        }

        #endregion
    }
}