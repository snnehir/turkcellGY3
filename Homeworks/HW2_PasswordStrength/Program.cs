/*
 *  1. En az 6 karakter
 *  2. Sadece harf ya da sadece sayı ise ZAYIF ya da sadece alfanümerik olmayan ise
 *  3. Hem harf hem sayı ise ORTA
 *  4. Hem sayı, hem harf hem de alfanümerik olmayan bir karakter varsa GÜÇLÜ şifre desin.
 *  
 *  İpucu: 
 *  char(.)
 */

Console.Write("> Enter a password (enter 'q' to quit): ");
string password = Console.ReadLine();
bool isTerminated = false;
while (!isTerminated)
{
    isTerminated = IsTerminationInput(password);
    if (isTerminated)
    {
        Console.WriteLine("Operation is terminated...");
        break;
    }
    // display password strength
    if (IsCorrectPasswordFormat(password))
    {
        Console.WriteLine($"Password strength: {GetPasswordStrength(password)}");
    }
    // wrong password format
    else
    {
        Console.WriteLine("Password must be at least 6 characters!");
    }
    Console.Write("\n> Enter a password (enter 'q' to quit): ");
    password = Console.ReadLine();
}

// Determines the strength of a password. 
// If the password contains only numbers, letters or symbols it returns "Weak."
// If the password contains number, letters and symbols it returns "Very strong". Else, it returns "Strong". 
string GetPasswordStrength(string password)
{
    int letterCount = password.Count(char.IsLetter);
    int numberCount = password.Count(char.IsDigit);
    int symbolCount = password.Length - letterCount - numberCount;
    if (letterCount > 0 && numberCount > 0 && symbolCount > 0)
    {
        return "Very strong";
    }
    else if ((letterCount > 0 && numberCount + symbolCount == 0) || (numberCount > 0 && letterCount + symbolCount == 0)
              || (symbolCount > 0 && numberCount + letterCount == 0))
    {
        return "Weak";
    }
    else
    {
        return "Strong";
    } 
}

// Checks the password format. (Password must contain at least 6 characters.)
bool IsCorrectPasswordFormat(string password)
{
    if (string.IsNullOrEmpty(password) || password.Length < 6)
    {
        return false;
    }
    return true;

}

// Checks if the input is 'q', which indicates that the program should terminate.
bool IsTerminationInput(string input)
{
    if (input == "q")
    {
        return true;
    }
    return false;
}
