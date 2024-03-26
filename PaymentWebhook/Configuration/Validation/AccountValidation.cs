using System.Text.RegularExpressions;

namespace CarRentalPlatform.Configuration.Validation;

public static class AccountValidation
{
    public static bool IsValidEmail(string? email)
    {
        if (email == null) return false;
        const string regex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,6})$";
        return Regex.IsMatch(email, regex);
    }
    
    public static bool IsValidPhoneNumber(string? phoneNumber)
    {
        if ((phoneNumber == null) || phoneNumber.Substring(0, 1) != "0") return false;
        const string regex = @"^\(?([0-9]{4})\)?[-  ]?([0-9]{3})[-  ]?([0-9]{3})$";
        return Regex.IsMatch(phoneNumber, regex);
    }

    public static bool IsValidPassword(string? password)
    {
        return password != null && password.Length >= 8;
    }

    
}