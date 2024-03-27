using System.Diagnostics;

namespace Repository.Repository.Utils;

public static class ConvertUtilization
{
    public static string GetGender(bool gender)
    {
        switch (gender)
        {
            case true:
                return EnumUtility.GenderMale;
            case false:
                return EnumUtility.GenderFemale;
        }
    }

    public static string GetRole(int role)
    {
        switch (role)
        {
            case 1:
                return EnumUtility.RoleAdmin;
            case 2:
                return EnumUtility.RoleEmployee;
            case 3:
                return EnumUtility.RoleCustomer;
            default:
                return string.Empty;
        }
    }

    public static string GetCarStatus(int status)
    {
        switch (status)
        {
            case 1:
                return EnumUtility.CarStatusAvailable;
            case 2:
                return EnumUtility.CarStatusOnBooking;
            case 3:
                return EnumUtility.CarStatusOnChecking;
            case 4:
                return EnumUtility.CarStatusUnavailable;
            default:
                return string.Empty;
        }
    }
    
    public static bool GetGender_2(int gender)
    {
        return gender == 1? false : true;
    } 
}