

int firstNum = int.Parse(Console.ReadLine()!);
int lastNum = int.Parse(Console.ReadLine()!);

Thread events = new Thread(() => PrintEventNumbers(firstNum, lastNum));
events.Start();
events.Join();
Console.WriteLine("All nums are present!");

void PrintEventNumbers(int firstNum, int lastNum)
{
    if (firstNum % 2 != 0)
    {
        firstNum++;
    }

    for (int i = firstNum; i <= lastNum; i += 2)
    {
        Console.WriteLine(i);
    }
}