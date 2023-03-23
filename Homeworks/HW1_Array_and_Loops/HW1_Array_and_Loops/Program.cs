// HW1 (09.03.2023): Find min, sum and average

int[] numbers = { 21, 13, 18, 15, 0, -12, -10 };

// While loop
Console.WriteLine("While loop results: ");
int i = 0;
int sum = 0;
int min = numbers[0];
while (i < numbers.Length)
{
    if (numbers[i] < min)
    {
        min = numbers[i];
    }
    sum += numbers[i++];
}
double avg = (double)sum / numbers.Length;
Console.WriteLine($"- Min: {min} \n- Sum: {sum} \n- Average: {avg}");

// For loop
Console.WriteLine("\nFor loop results: ");
sum = 0;
min = numbers[0];
for (int j = 0; j < numbers.Length; j++)
{
    if (numbers[j] < min)
    {
        min = numbers[j];
    }
    sum += numbers[j];
}
avg = (double)sum / numbers.Length;
Console.WriteLine($"- Min: {min} \n- Sum: {sum} \n- Average: {avg}");

// Foreach loop
Console.WriteLine("\nForeach loop results: ");
sum = 0;
min = numbers[0];
foreach (int number in numbers)
{
    if (number < min)
    {
        min = number;
    }
    sum += number;
}
avg = (double)sum / numbers.Length;
Console.WriteLine($"- Min: {min} \n- Sum: {sum} \n- Average: {avg}");

/*
// Additional practice: do while loop
Console.WriteLine("\n- - - - - - - - - - - - - - - - - \n");
bool isTerminated = false;
string[] tens = { "", "on", "yirmi", "otuz", "kırk", "elli", "altmış", "yetmiş", "seksen", "doksan" };
string[] ones = { "", "bir", "iki", "üç", "dört", "beş", "altı", "yedi", "sekiz", "dokuz" };
do
{
    try
    {
        Console.Write("1-999 arasında bir sayı giriniz (Çıkmak için -1 giriniz): ");
        int number = Convert.ToInt32(Console.ReadLine());
        if(number == -1)
        {
            isTerminated = true;
            Console.WriteLine("\nİşlem sonlandırıldı.");
            break;
        }
        else if (number <= 0 || number > 999)
        {
            throw new Exception();
        }
        int hundreds = number / 100;
        string hundreds_str = hundreds == 0 ? "": hundreds == 1 ? " yüz" : ones[hundreds] + " yüz";
        int idx_tens = (number % 100) / 10;
        int idx_ones = (number % 100) % 10;

        Console.WriteLine($"{hundreds_str} {tens[idx_tens]} {ones[idx_ones]}");

    }
    catch (Exception)
    {
        Console.Write("\nLütfen geçerli bir sayı giriniz! ");
    }

} while (!isTerminated);
*/
