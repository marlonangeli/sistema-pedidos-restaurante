using System.Text.RegularExpressions;
using Restaurante.Cheng.Domain.Dtos;

namespace Restaurante.Cheng.Data.Helpers;

public static class GarcomHelper
{
    public static bool IsGarcomValid(this CreateGarcom garcom)
    {
        return !string.IsNullOrEmpty(garcom.Nome) && 
               !string.IsNullOrEmpty(garcom.Sobrenome) && 
               !string.IsNullOrEmpty(garcom.NumeroTelefone) &&
               ValidadePhoneNumber(garcom.NumeroTelefone);
    }
    
    private static bool ValidadePhoneNumber(string phoneNumber)
    {
        return !string.IsNullOrEmpty(phoneNumber) && 
               Regex.IsMatch(phoneNumber, @"^\(\d{3}\)\s\d{3}-\d{4}$");
    }
}