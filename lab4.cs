using System;

namespace lab4
{
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("Ввыберите номер операции над массивом");
            Console.WriteLine("1.Удалить все четные элементы");
            Console.WriteLine("2. Добавить несколько элементов в начало массива");
            Console.WriteLine("3. Четные элементы переставить в начало массива, нечетные - в конец");
            Console.WriteLine("4. Найти первый четный элемент");
            Console.WriteLine("5. Отсортировать массив");
            Console.WriteLine("6. Найти элемент в отсортированном массиве");
            Console.WriteLine("7. Создать новый массив");
            Console.WriteLine("8. Выход");
        }
        static int[] EnterArray(int elementsCount)
        {
            int[] NewArray = new int[elementsCount];
            Boolean IsCorrect;
            for (int i = 0; i < elementsCount; i++)
            {
                do
                {
                    Console.WriteLine("Введите {0} значение массива", i + 1);
                    IsCorrect = int.TryParse(Console.ReadLine(), out NewArray[i]);
                } while (!IsCorrect);
            }
            Console.WriteLine();
            return NewArray;
        }
        static int[] RandomArray(int elementsCount)
        {
            int[] NewArray = new int[elementsCount];
            Random a = new Random(0);
            for (int i = 0; i < elementsCount; i++)
                NewArray[i] = a.Next(0, 100);
            Console.WriteLine();
            return NewArray;
        }
        static void PrintArray(int[] thisArray)
        {
            if (thisArray.Length == 0)
                Console.WriteLine("Ваш массив пуст");
            else
            {
                Console.WriteLine("Ваш массив:");
                for (int i = 0; i < thisArray.Length; i++)
                    Console.Write(thisArray[i] + " ");
                Console.WriteLine();
            }
        }
        static int[] DeleteEven(ref int[] thisArray)
        {
            int countOdd = 0, j = 0;
            for (int i = 0; i < thisArray.Length; i++)
                if (thisArray[i] % 2 == 1)
                    countOdd++;
            int[] newArray = new int[countOdd];    
                for (int i = 0; i < thisArray.Length; i++)
                    if (thisArray[i] % 2 == 1)
                    {
                        newArray[j] = thisArray[i];
                        j++;
                    }
            return newArray;
        }
        static int[] InsertArray(ref int[] thisArray)
        {
            int countNumbers = 1, operationEnter;
            int[] additionalArray = new int[1] {0};
            Boolean IsCorrect;
            do
            {
                Console.WriteLine("Введите колчество чисел, которые вы хотите поставить в начало массива:");
                IsCorrect = int.TryParse(Console.ReadLine(), out countNumbers);
                if (countNumbers < 0)
                    IsCorrect = false;
            } while (!IsCorrect);
            if (countNumbers == 0) return thisArray;
            int[] NewArray = new int[countNumbers + thisArray.Length];
            do
            {
                Console.WriteLine("Ввыберите, как будете вводить значения: " +
                    "\n1. Ввести самостоятельно" +
                    "\n2. Присвоить случайные значения");
                IsCorrect = int.TryParse(Console.ReadLine(), out operationEnter);
                if (!(operationEnter == 1 || operationEnter == 2))
                    IsCorrect = false;
            } while (!IsCorrect);
            if (operationEnter == 1)
                additionalArray = EnterArray(countNumbers);
            else 
                additionalArray = RandomArray(countNumbers);
            for (int i = 0; i < countNumbers; i++)
                NewArray[i] = additionalArray[i];
            for (int i = countNumbers; i < countNumbers + thisArray.Length; i++)
                NewArray[i] = thisArray[i - countNumbers];
            return NewArray;
        }
        static int[] SortCondition(ref int[] thisArray)
        {
            int countEven = 0, indexEven = 0, indexOdd = 0;
            for (int i = 0; i < thisArray.Length; i++)
                if (thisArray[i] % 2 == 0)
                    countEven++;
            if (countEven == 0)
                return thisArray;     
            int[] evenArray = new int[countEven];
            int[] oddArray = new int[thisArray.Length - countEven];
            int[] NewArray = new int[thisArray.Length];
            for (int i = 0; i < thisArray.Length; i++)
            {
                if (thisArray[i] % 2 == 0)
                {
                    evenArray[indexEven] = thisArray[i];
                    indexEven++;
                }
                else
                {
                    oddArray[indexOdd] = thisArray[i];
                    indexOdd++;
                }
            }

            for (int i = 0; i < thisArray.Length; i++)
            {
                if (i + 1 <= countEven)
                    NewArray[i] = evenArray[i];
                else
                    NewArray[i] = oddArray[i - countEven];
            }
            return NewArray;
        }
        static void FindFirstEven(int[] thisArray)
        {
            Boolean IsFound = false;
            PrintArray(thisArray);
            for (int i = 0; i < thisArray.Length; i++)
            {
                if (thisArray[i] % 2 == 0)
                {
                    Console.WriteLine("Первы четный элемент: " + thisArray[i] +
                        "\nНомер числа: " + (i + 1) +
                        "\nКол-во сравнений, необходимых для поиска: " + (i + 1));
                    IsFound = true;
                    break;
                }
            }
            if (!IsFound)
                Console.WriteLine("В последовательности нет четных элементов");
        }
        static int[] SortArray(ref int[] thisArray)
        {
            int temp;
            for (int i = 0; i < thisArray.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < thisArray.Length; j++)
                {
                    if (thisArray[j] < thisArray[min])
                        min = j;
                }
                if (min != i)
                {
                    temp = thisArray[min];
                    thisArray[min] = thisArray[i];
                    thisArray[i] = temp;
                }
            }
            return thisArray;
        }
        static void SearchNumber(int[] thisArray)
        {
            Boolean IsCorrect, IsFound=false;
            int numberForFind, left = 0, right = thisArray.Length - 1, average, countComparing = 0;
            do
            {
                Console.WriteLine("Введите элемент, который хотите найти в отсортированном массиве: ");
                IsCorrect = int.TryParse(Console.ReadLine(), out numberForFind);
            } while (!IsCorrect);
            thisArray = SortArray(thisArray);
            PrintArray(thisArray);
            if (thisArray.Length != 0)
            {
                do
                {
                    average = (left + right) / 2;
                    countComparing++;
                    if (thisArray[average] < numberForFind)
                        left = average + 1;
                    else
                        right = average;
                } while (left != right);
                if (thisArray[left] == numberForFind)
                {
                    Console.WriteLine($"Элемент {numberForFind} найден \nНомер элемента: {left + 1}" +
                        $" \nКоличество понадобившихся сравнений: {countComparing}");
                    IsFound = true;
                }
                else
                    IsFound = false;
            }
            if (!IsFound)
                Console.WriteLine($"Элемент {numberForFind} не найден");
        }
        static int NextAction()
        {
            Boolean IsCorrect;
            int goNext;
            do
            {
                Console.WriteLine("Хотите продолжить операции над массивом?"+
                    "\nВыберите, что хотите сделать \n1. Продолжить работать с этим массивом \n2. Ввести новый массив \n3. Завершить");
                IsCorrect = int.TryParse(Console.ReadLine(), out goNext);
                if (!(goNext == 1 || goNext == 2|| goNext == 3))
                    IsCorrect = false;
            } while (!IsCorrect);
            return goNext;
        }
        static void Main(string[] args)
        {
            int n, goNext, operationEnter, operationArray;
            int[] MyArray = new int[1] { 0 };
            Boolean IsCorrect, finish = false;
            Console.WriteLine("Работа с одномерным массивом");
            mainPart:
            do
            {
                Console.WriteLine("\nВведите длину массива");
                IsCorrect = int.TryParse(Console.ReadLine(), out n);
                if (n <= 0)
                    IsCorrect = false;
            } while (!IsCorrect);
            Console.WriteLine();
            do
            {
                Console.WriteLine("Ввыберите, как заполнить массив значениями: " +
                    "\n1. Ввести массив самостоятельно" +
                    "\n2. Присвоить массиву случайные значения");
                IsCorrect = int.TryParse(Console.ReadLine(), out operationEnter);
                if (!(operationEnter == 1 || operationEnter == 2))
                    IsCorrect = false;
            } while (!IsCorrect);

            switch (operationEnter)
            {
                case 1:
                    {
                        MyArray = EnterArray(n);
                        PrintArray(MyArray);
                        break;
                    }
                case 2:
                    {
                        MyArray = RandomArray(n);
                        PrintArray(MyArray);
                        break;
                    }
            }
            do
            {
                do
                {
                    //PrintMenu();
                    IsCorrect = int.TryParse(Console.ReadLine(), out operationArray);
                    if (!(operationArray >= 1 && operationArray <= 8))
                        IsCorrect = false;
                } while (!IsCorrect);
                Console.WriteLine();
                switch (operationArray)
                {
                    case 1:
                        {
                            MyArray = DeleteEven(ref MyArray);
                            PrintArray(MyArray);
                            break;
                        }
                    case 2:
                        {
                            MyArray = InsertArray(ref MyArray);
                            PrintArray(MyArray);
                            break;
                        }
                    case 3:
                        {
                            MyArray = SortCondition(ref MyArray);
                            PrintArray(MyArray);
                            break;
                        }
                    case 4:
                        {
                            FindFirstEven(MyArray);
                            break;
                        }
                    case 5:
                        {
                            MyArray = SortArray(ref MyArray);
                            PrintArray(MyArray);
                            break;
                        }
                    case 6:
                        {
                            SearchNumber(MyArray);
                            break;
                        }
                    case 7:
                        {
                            goto mainPart;
                        }
                    case 8:
                        {
                            finish = true;
                            break;
                        }
                }
                if (!finish)
                {
                    goNext = NextAction();
                    if (goNext == 2)
                        goto mainPart;
                }
                else goNext = -1;
            } while (goNext == 1);
        }
    }
}
