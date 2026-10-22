using dz1lat.Grannies;
using dz1lat.Students;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace dz1lat
{
    internal class Program
    {

        static void Ex1(string targetFolder)
        {
            if (!Directory.Exists(targetFolder))
            {
                Console.WriteLine($"Ошибка: Папка '{targetFolder}' не найдена.");
                return;
            }
            List<string> foundFiles = new List<string>();
            foundFiles.AddRange(Directory.GetFiles(targetFolder, "*.jpeg"));

            if (foundFiles.Count < 32)
            {
                Console.WriteLine($"Ошибка: В папке найдено только {foundFiles.Count} картинок. Требуется минимум 32.");
                return;
            }
            List<string> imageList = new List<string>(64);
            for (int i = 0; i < 32; i++)
            {
                imageList.Add(foundFiles[i]);
                imageList.Add(foundFiles[i]);
            }
            Dictionary<string, int> fileToNumber = new Dictionary<string, int>();
            for (int i = 0; i < 32; i++)
            {
                fileToNumber[foundFiles[i]] = i + 1;
            }
            Console.WriteLine("ИЗНАЧАЛЬНЫЙ СПИСОК");
            for (int i = 0; i < imageList.Count; i++)
            {
                Console.Write($"{fileToNumber[imageList[i]]}\t");
                if ((i + 1) % 8 == 0) Console.WriteLine();
            }
            Random random = new Random();
            int n = imageList.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string temp = imageList[k];
                imageList[k] = imageList[n];
                imageList[n] = temp;
            }
            Console.WriteLine("\nПОЛУЧЕННЫЙ ПЕРЕМЕШАННЫЙ СПИСОК");
            for (int i = 0; i < imageList.Count; i++)
            {
                Console.Write($"{fileToNumber[imageList[i]]}\t");
                if ((i + 1) % 8 == 0) Console.WriteLine();
            }
        }
        static void Ex2(string inputStudents)
        {
            Dictionary<string, Studentstruct> studentDict = new Dictionary<string, Studentstruct>();

            string[] lines = File.ReadAllLines(inputStudents);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 5)
                {
                    string lastName = parts[0].Trim();
                    string firstName = parts[1].Trim();
                    int birthYear = int.Parse(parts[2].Trim());
                    string examName = parts[3].Trim();
                    int score = int.Parse(parts[4].Trim());

                    Studentstruct student = new Studentstruct(lastName, firstName, birthYear, examName, score);
                    string key = $"{lastName} {firstName}";

                    if (!studentDict.ContainsKey(key))
                    {
                        studentDict.Add(key, student);
                    }
                }
            }
            string[] commands = new string[] { "Новый студент", "Удалить", "Сортировать", "Выход" };
            byte flag = 1;
            while (flag == 1)
            {
                Console.WriteLine("Доступные команды: [Новый студент], [Удалить], [Сортировать], [Выход]");
                Console.Write("Введите команду: ");
                string command = Console.ReadLine().Trim();

                switch (command)
                {
                    case "Новый студент":
                        Console.Write("Введите фамилию: ");
                        string lName = Console.ReadLine();
                        Console.Write("Введите имя: ");
                        string fName = Console.ReadLine();
                        Console.Write("Введите год рождения: ");
                        int year = int.TryParse(Console.ReadLine(), out int y) ? y : 0;
                        Console.Write("Введите вступительный экзамен: ");
                        string exam = Console.ReadLine();
                        Console.Write("Введите баллы: ");
                        int points = int.TryParse(Console.ReadLine(), out int p) ? p : 0;

                        Studentstruct newStudent = new Studentstruct(lName, fName, year, exam, points);
                        string newKey = $"{lName} {fName}";

                        if (!studentDict.ContainsKey(newKey))
                        {
                            studentDict.Add(newKey, newStudent);
                            Console.WriteLine("Студент успешно добавлен.");
                        }
                        else
                        {
                            Console.WriteLine("Такой студент уже есть в базе данных!");
                        }
                        break;

                    case "Удалить":
                        Console.Write("Введите фамилию удаляемого студента: ");
                        string deleteLName = Console.ReadLine().Trim();
                        Console.Write("Введите имя удаляемого студента: ");
                        string deleteFName = Console.ReadLine().Trim();

                        string searchKey = $"{deleteLName} {deleteFName}";

                        if (studentDict.Remove(searchKey))
                        {
                            Console.WriteLine($"Студент {searchKey} успешно удален.");
                        }
                        else
                        {
                            Console.WriteLine("Студент с такими данными не найден.");
                        }
                        break;

                    case "Сортировать":
                        Console.WriteLine("\nСписок студентов, отсортированный по баллам:");
                        List<Studentstruct> sortedStudents = studentDict.Values.OrderBy(s => s.Score).ToList();

                        foreach (var student in sortedStudents)
                        {
                            Console.WriteLine(student);
                        }
                        break;

                    case "Выход":
                        flag = 0;
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда. Проверьте правильность написания (учитывайте регистр букв).");
                        break;
                }
            }
        }
        static void Ex3()
        {
            Stack<Hospital> hospitals = new Stack<Hospital>();
            hospitals.Push(new Hospital("Городская Клиника №1", new List<string> { "Грипп", "Радикулит", "Давление" }, 2));
            hospitals.Push(new Hospital("Областная Больница №2", new List<string> { "Остеохондроз", "Давление", "Кашель" }, 3));
            hospitals.Push(new Hospital("Частный Госпиталь", new List<string> { "Грипп", "Мигрень", "Склероз" }, 1));

            List<Granny> allGrannies = new List<Granny>();
            Queue<Granny> grannyQueue = new Queue<Granny>();

            Console.WriteLine("Ввод данных о бабулях с клавиатуры");
            bool entering = true;
            while (entering)
            {
                Console.Write("\nВведите имя бабули (или 'стоп' для завершения ввода): ");
                string nameInput = Console.ReadLine();
                string name = nameInput != null ? nameInput.Trim() : "";

                if (name.Equals("стоп", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(name))
                {
                    entering = false;
                    continue;
                }

                Console.Write("Введите возраст: ");
                int age;
                int.TryParse(Console.ReadLine(), out age);

                List<Disease> diseases = new List<Disease>();
                while (true)
                {
                    Console.Write("Введите название болезни (или нажмите Enter, если болезней больше нет): ");
                    string diseaseInput = Console.ReadLine();
                    string diseaseName = diseaseInput != null ? diseaseInput.Trim() : "";

                    if (string.IsNullOrEmpty(diseaseName)) break;

                    Console.Write("Введите лекарство от болезни '" + diseaseName + "': ");
                    string medicineInput = Console.ReadLine();
                    string medicine = medicineInput != null ? medicineInput.Trim() : "";

                    diseases.Add(new Disease(diseaseName, medicine));
                }

                Granny g = new Granny(name, age, diseases);
                grannyQueue.Enqueue(g);
                allGrannies.Add(g);
            }

            Console.WriteLine("\nНачинается распределение бабуль");

            List<Hospital> temporaryHospitalList = hospitals.ToList();

            while (grannyQueue.Count > 0)
            {
                Granny currentGranny = grannyQueue.Dequeue();
                bool distributed = false;

                if (currentGranny.Diseases.Count == 0)
                {
                    for (int i = 0; i < temporaryHospitalList.Count; i++)
                    {
                        Hospital hospital = temporaryHospitalList[i];
                        if (hospital.Patients.Count < hospital.Capacity)
                        {
                            hospital.Patients.Add(currentGranny);
                            Console.WriteLine("[Успех] " + currentGranny.Name + " пришла 'только спросить' и зашла в '" + hospital.Name + "'");
                            distributed = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < temporaryHospitalList.Count; i++)
                    {
                        Hospital hospital = temporaryHospitalList[i];
                        if (hospital.Patients.Count >= hospital.Capacity) continue;

                        int matchingDiseasesCount = 0;
                        foreach (Disease d in currentGranny.Diseases)
                        {
                            if (hospital.TreatableDiseases.Contains(d.Name))
                            {
                                matchingDiseasesCount++;
                            }
                        }

                        if (matchingDiseasesCount * 2 > currentGranny.Diseases.Count)
                        {
                            hospital.Patients.Add(currentGranny);
                            Console.WriteLine("[Успех] " + currentGranny.Name + " госпитализирована в '" + hospital.Name + "' (вылечат " + matchingDiseasesCount + " из " + currentGranny.Diseases.Count + " болезней)");
                            distributed = true;
                            break;
                        }
                    }
                }

                if (!distributed)
                {
                    Console.WriteLine("[Отказ] Для бабули " + currentGranny.Name + " не нашлось места или подходящей больницы. Она осталась на улице плакать...");
                }
            }

            hospitals.Clear();
            for (int i = temporaryHospitalList.Count - 1; i >= 0; i--)
            {
                hospitals.Push(temporaryHospitalList[i]);
            }
            Console.WriteLine("\nСПИСОК ВСЕХ БАБУЛЬ:");
            foreach (Granny g in allGrannies)
            {
                Console.WriteLine("- " + g.ToString());
            }

            Console.WriteLine("\nСПИСОК БОЛЬНИЦ И ИХ ЗАПОЛНЕННОСТЬ:");
            foreach (Hospital hospital in hospitals)
            {
                Console.WriteLine("\n" + hospital.ToString());
                if (hospital.Patients.Count > 0)
                {
                    Console.WriteLine("  Пациенты внутри:");
                    foreach (Granny patient in hospital.Patients)
                    {
                        Console.WriteLine("    * " + patient.Name);
                    }
                }
                else
                {
                    Console.WriteLine("  В данный момент пациентов нет.");
                }
            }
        }
        static void Ex4()
        {
            string[] vertexNames = new string[] { "А", "Б", "В", "Г", "Д", "Е" };
            int[,] adjacencyMatrix = new int[,]
            {
                {0, 1, 1, 0, 0, 0},
                {1, 0, 1, 1, 0, 0}, 
                {1, 1, 0, 0, 1, 0}, 
                {0, 1, 0, 0, 0, 1}, 
                {0, 0, 1, 0, 0, 0}, 
                {0, 0, 0, 1, 0, 0}
            };
            int startIdx = 0;  
            int targetIdx = 5; 

            Console.WriteLine("Поиск кратчайшего пути от '" + vertexNames[startIdx] + "' до '" + vertexNames[targetIdx] + "':\n");

            Queue<int> queue = new Queue<int>();
            HashSet<int> visited = new HashSet<int>();
            Dictionary<int, int> parents = new Dictionary<int, int>();
            queue.Enqueue(startIdx);
            visited.Add(startIdx);

            bool isPathFound = false;

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                if (current == targetIdx)
                {
                    isPathFound = true;
                    break;
                }

                for (int neighbor = 0; neighbor < vertexNames.Length; neighbor++)
                 { 
                    if (adjacencyMatrix[current, neighbor] == 1 && !visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        parents[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }
       
            if (isPathFound)
            {
                List<string> path = new List<string>();
                int curr = targetIdx;

                while (curr != startIdx)
                {
                    path.Add(vertexNames[curr]);
                    curr = parents[curr];
                }
                path.Add(vertexNames[startIdx]);

                path.Reverse();

                Console.WriteLine("Кратчайший путь найден: " + string.Join(" -> ", path));
                Console.WriteLine("Количество переходов (ребер): " + (path.Count - 1));
            }
            else
            {
                Console.WriteLine("Пути между вершинами не существует.");
            }
        }
        static void Main(string[] args)
                    {
            Console.WriteLine("Задание 1");
            Ex1("..\\..\\Images");
            Console.ReadKey();
            Console.WriteLine("Задание 2");
            Ex2("..\\..\\Students\\StudentsList.txt");
            Console.ReadKey();
            Ex3();
            Console.ReadKey();
            Ex4();

                    }
                }
            }
        
    



        
    

        

    
    

