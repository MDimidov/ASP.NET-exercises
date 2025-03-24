
string? input;
Console.Write("Please enter your command: ");

while ((input = Console.ReadLine()) != "show")
{
    Console.Write("Please enter your command again: ");
}
long result = SumAsync();
Console.WriteLine(result);


long SumAsync()
{
    return Task.Run(() =>
    {
        long sum = 0;
        for (int i = 2; i <= 1_000_000; i += 2)
        {
            sum += i;
        }

        return sum;
    }).Result;
}