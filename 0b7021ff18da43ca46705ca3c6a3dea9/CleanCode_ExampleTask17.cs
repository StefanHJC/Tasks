public static void CreateNewObject()
{
    //Создание объекта на карте
}

public static void GenerateChance()
{
    _chance = Random.Range(0, 100);
}

public static int CalculateAndSetSalary(int hoursWorked)
{
    return _hourlyRate * hoursWorked;
}