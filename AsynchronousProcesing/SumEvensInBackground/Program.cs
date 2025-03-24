

long sum = 0;

Task<long> task = Task.Run(() =>
{
    for (long i = 2; i <= 10_000_000_000; i += 2)
    {
        sum += i;
    }

    return sum;
});


string? input;
Console.Write("Please enter your command: ");

while ((input = Console.ReadLine()) != "show")
{
    Console.Write("Please enter your command again: ");
}
Console.WriteLine($"Your result is: {task.Result}");

