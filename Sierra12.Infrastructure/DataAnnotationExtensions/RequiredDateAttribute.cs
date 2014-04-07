using System;
using System.ComponentModel.DataAnnotations;

namespace ProgressTen.Infrastructure.DataAnnotationExtensions
{
	public class RequiredDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
        	string rawValue = (string) value;

        	try
        	{
        		var date = DateTime.Parse(rawValue);
        	}
        	catch (Exception)
        	{
        		return false;
        	}

        	return true;
        }
    }
}