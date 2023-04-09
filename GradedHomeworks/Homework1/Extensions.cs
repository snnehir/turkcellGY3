public static class Extensions
{
    public static string InputValidate(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentNullException();
        }
        return input;
    }
    public static string ConstructTableHeader(this string columnsStr, char symbol = '-')
    {
        string[] columns = columnsStr.Split("\t");
        string line = string.Empty;
        foreach(string col in columns)
        {
            line += new string(symbol, col.Length) + "\t";
        }
        return columnsStr + "\n" + line;
    }

}
