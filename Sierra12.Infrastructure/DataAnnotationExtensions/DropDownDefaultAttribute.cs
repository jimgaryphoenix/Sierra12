using System.ComponentModel.DataAnnotations;

namespace ProgressTen.Infrastructure.DataAnnotationExtensions
{
	public class DropDownDefaultAsIntAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
        	int selection = (int) value;

			if (selection == -1)
            {
                return false;
            }

        	return true;
        }
    }

	public class DropDownDefaultAsStringAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			string selection = (string)value;

			if (selection.Equals("-1"))
			{
				return false;
			}

			return true;
		}
	}

	public class DropDownSelectedNullableIntAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			int selection = (int)value;

			if (selection == -1)
			{
				return false;
			}

			return true;
		}
	}
}