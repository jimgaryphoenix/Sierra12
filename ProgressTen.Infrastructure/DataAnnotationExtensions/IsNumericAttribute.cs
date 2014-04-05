using System;
using System.ComponentModel.DataAnnotations;

namespace ProgressTen.Infrastructure.DataAnnotationExtensions
{
	public class IsNumericGreaterThenZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
        	int number;
        	try
        	{
				number = int.Parse(value.ToString());
        	}
        	catch (Exception)
        	{
        		return false;
        	}

        	return number > 0;
        }
    }
}