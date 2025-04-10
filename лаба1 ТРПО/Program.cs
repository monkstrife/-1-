using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Reflection;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            mainMenu();
        }

        static void mainMenu ()
        {
            string menu_text = "Информация по типам: \r\n1 – Общая информация по типам \r\n2 – Выбрать тип из списка \r\n3 – Параметры консоли \r\n0 - Выход из программы";
            string key;
           

            /*Создаем меню*/
            Console.Clear();
            Console.WriteLine(menu_text);

            while (true)
            {
                /*читаем первый символ и запускаем соответствующую подпрограмму*/
                key = Console.ReadLine() ?? string.Empty;
                if(key == string.Empty)
                {
                    continue;
                }

                /*Запускаем выбранную задачу*/
                switch (key[0])
                {
                    case '1':
                        genInfotype();
                        Console.WriteLine(menu_text);
                        break;
                    case '2':
                        Infotypemenu();
                        Console.WriteLine(menu_text);
                        break;
                    case '3':
                        consoleSettings();
                        Console.WriteLine(menu_text);
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("Введите нужный символ (1, 2, 3 или 0)");
                        break;
                }
            }
        }

        /*
         *
         *  === 1 ===
         *
         */
        /*Общая информация о типах*/
        static void genInfotype()
        {
            Console.Clear();
            Console.WriteLine("Общая информация по типам");

            /*Обозначим флаг для поиска всех элементов, а не только открытых*/
            BindingFlags flag = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            int nRefTypes = 0;
            int nValueTypes = 0;


            Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new();
            foreach (Assembly asm in refAssemblies)
                types.AddRange(asm.GetTypes());

            Console.WriteLine("Подключенные сборки: {0}", refAssemblies.Length);
            Console.WriteLine("Всего типов по всем подключенным сборкам: {0}", types.Count);

            foreach (var t in types)
            {
                if (t.IsClass)
                    nRefTypes++;
                else if (t.IsValueType)
                    nValueTypes++;
            }

            Console.WriteLine("Ссылочные типы (только классы): {0}", nRefTypes);
            Console.WriteLine("Значимые типы: {0}", nValueTypes);

            Console.WriteLine("\nИнформация в соответствии с вариантом V = {0}\n", 1 + ('D' + 'V') % 10);

            // Самое длинное название свойства
            Console.WriteLine("Самое длинное название свойства:");
            PropertyInfo property = TypeUtils.GetTheLongestPubProperty(types);
            Console.WriteLine("Длина имени: {0}  / Имя: {1}\n", property.Name.Length, property.Name);

            // Тип с наибольшим числом конструкторов
            Console.WriteLine("Тип с наибольшим числом конструкторов:");
            Type maxNumConstrucType = TypeUtils.GetTypeWithMaxConstructors(types);
            Console.WriteLine("Имя типа - {0}\nЧисло конструкторов - {1}\n", maxNumConstrucType.Name, maxNumConstrucType.GetConstructors(flag).Length);

            // Метод с наибольшим разнообразием аргументов по типам (больше разных типов аргументов)
            Console.WriteLine("Метод с наибольшим разнообразием аргументов по типам (больше разных типов аргументов):");
            MethodInfo method = TypeUtils.GetMethodWithArgVariety(types);

            Console.WriteLine("Имя метода: {0}",method.Name);
            Console.WriteLine("Типы аргументов:");
            foreach (ParameterInfo p in method.GetParameters())
            {
                Console.WriteLine(p.ParameterType.Name);
            }

            Console.WriteLine("\nНажмите, чтобы вернуться . . .");
            Console.ReadKey();
            Console.Clear();
        }


        /*
         *
         *  === 2 ===
         *
         */
        /*Информация по типам*/
        static void Infotypemenu()
        {
            Console.Clear();

            string text = "Информация по типам\r\nВыберите тип:\r\n----------------------------------------\r\n1 – uint\r\n2 – int\r\n3 – long\r\n4 – float\r\n5 – double\r\n6 – char\r\n7 - string\r\n8 – record\r\n9 – Tuple<int, string>\r\n0 – Выход в главное меню";
            string key;

            Console.WriteLine(text);

            while (true)
            {
                /*читаем первый символ и запускаем соответствующую подпрограмму*/
                key = Console.ReadLine() ?? string.Empty;
                if (key == string.Empty)
                {
                    continue;
                }

                /*Запускаем выбранную задачу*/
                switch (key[0])
                {
                    case '1':
                        if (Infotype(typeof(uint)) == 1) return;
                        else Console.WriteLine(text);
                        break;
                    case '2':
                        if (Infotype(typeof(int)) == 1) return;
                        else Console.WriteLine(text);
                        break;
                    case '3':
                        if (Infotype(typeof(long)) == 1) return;
                        else Console.WriteLine(text);
                        break;
                    case '4':
                        if (Infotype(typeof(float)) == 1) return;
                        else Console.WriteLine(text);
                        break;
                    case '5':
                        if (Infotype(typeof(double)) == 1) return;
                        else Console.WriteLine(text);
                        break;
                    case '6':
                        if (Infotype(typeof(char)) == 1) return;
                        else Console.WriteLine(text);
                        break;
                    case '7':
                        if (Infotype(typeof(string)) == 1) return;
                        else Console.WriteLine(text);
                        break;
                    case '8':
                        /*System.Runtime.InteropServices.Variant+Record*/
                        Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                        List<Type> types = new();
                        foreach (Assembly asm in refAssemblies)
                            types.AddRange(asm.GetTypes());

                        foreach (var t in types)
                        {
                            if (t.Name == "Record")
                            {
                                if (Infotype(t) == 1) return;
                                else Console.WriteLine(text);
                            }
                        }
                        break;
                    case '9':
                        if (Infotype(typeof(Tuple<int, string>)) == 1) return;
                        else Console.WriteLine(text);
                        break;
                    case '0':
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Введите нужный символ (1, 2, 3, 4, 5, 6, 7, 8, 9 или 0)");
                        break;
                }
            }
        }

        /*Информация по типу*/
        static int Infotype(Type t)
        {
            Console.Clear();

            /*Обозначим флаг для поиска всех элементов, а не только открытых*/
            BindingFlags flag = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public/* | BindingFlags.NonPublic*/;

            string[] Fields = new string[t.GetFields(flag).Length];
            string sFieldname;
            string[] Properts = new string[t.GetProperties(flag).Length];
            string sPropertsname;

            Console.WriteLine("Информация по типу: {0}", t);

            Console.Write("\tЗначимый тип: ");
            if (t.IsValueType)
            {
                Console.WriteLine("+");
            }
            else
            {
                Console.WriteLine("-");
            }

            Console.WriteLine("\tПространство имен: {0}", t.Namespace);

            Console.WriteLine("\tСборка: {0}", t.Assembly.GetName().Name);

            Console.WriteLine("\tОбщее число элементов: {0}", t.GetMembers(flag).Length);

            Console.WriteLine("\tЧисло методов: {0}", t.GetMethods(flag).Length);

            Console.WriteLine("\tЧисло свойств: {0}", t.GetProperties(flag).Length);

            Console.WriteLine("\tЧисло полей: {0}", t.GetFields(flag).Length);

            for (int i = 0; i < t.GetFields(flag).Length; i++)
            {
                Fields[i] = t.GetFields(flag)[i].Name;
            }
            sFieldname = String.Join(", ", Fields);
            if(sFieldname == string.Empty)
            {
                sFieldname = "-";
            }
            Console.WriteLine("\tСписок полей: {0}", sFieldname);

            for (int i = 0; i < t.GetProperties(flag).Length; i++)
            {
                Properts[i] = t.GetProperties(flag)[i].Name;
            }
            sPropertsname = String.Join(", ", Properts);
            if (sPropertsname == string.Empty)
            {
                sPropertsname = "-";
            }
            Console.WriteLine("\tСписок свойств: {0}", sPropertsname);

            string text = "Нажмите ‘M’ для вывода дополнительной информации по методам: \r\nНажмите ‘0’ для выхода в главное меню";
            string key;

            Console.WriteLine(text);
            while (true)
            {
                /*читаем первый символ и запускаем соответствующую подпрограмму*/
                key = Console.ReadLine() ?? string.Empty;
                if (key == string.Empty)
                {
                    continue;
                }

                /*Запускаем выбранную задачу*/
                switch (key[0])
                {
                    case 'M':
                        ShowMethods(t, flag);
                        Console.Clear();
                        return 0;
                    case '0':
                        Console.Clear();
                        return 1;
                    default:
                        Console.WriteLine("Введите нужный символ (M или 0)");
                        break;
                }
            }
        }

        static void ShowMethods(Type t, BindingFlags flag)
        {
            var allMethods = t.GetMethods(flag);
            var overloads = new Dictionary<string, int[/*число перегрузок, минимальное число параметров, максимальное*/]>();
            foreach (var m in allMethods)
            {
                if (overloads.ContainsKey(m.Name))
                {
                    // в словаре уже есть такое имя, обновляем статистику
                    overloads[m.Name][0]++;

                    //если у нового метода число параметров меньше, то он станет методом с самым мальним числом параметров
                    if(m.GetParameters().Length < overloads[m.Name][1])
                    {
                        overloads[m.Name][1] = m.GetParameters().Length;
                    }

                    //если у нового метода число параметров больше, то он станет методом с самым большим числом параметров
                    if (m.GetParameters().Length > overloads[m.Name][2])
                    {
                        overloads[m.Name][2] = m.GetParameters().Length;
                    }
                }
                else
                {
                    // в словаре нет такого имени, добавляем элемент
                    /*new int[3] { 1, m.GetParameters().Length, m.GetParameters().Length }*/
                    overloads.Add(m.Name, [1, m.GetParameters().Length, m.GetParameters().Length]);
                }
            }

            Console.WriteLine("Методы типа: {0}\r\nНазвание\t\tЧисло перегрузок\tЧисло параметров", t);
            foreach (var item in overloads)
            {
                Console.Write("{0}\r\t\t\t{1}\t\t\t", item.Key, item.Value[0]);
                if (item.Value[1] == item.Value[2])
                {
                    Console.WriteLine(item.Value[1]);
                }
                else
                {
                    Console.WriteLine("{0}..{1}", item.Value[1], item.Value[2]);
                }
            }

            Console.WriteLine("\nНажмите, чтобы вернуться . . .");
            Console.ReadKey();
        }



        /*
         *
         *  === 3 ===
         *
         */
        /*Параметры консоли*/
        static void consoleSettings()
        {
            Console.Clear();

            string color;
            Type type = typeof(ConsoleColor);

            Console.WriteLine("Выберете цвет для фона (\"-\" - оставить без изменения):");

            /*Назначаем цвет для фона*/
            while(true)
            {
                color = Console.ReadLine() ?? string.Empty;
                if (color == "-")
                {
                    break;
                }
                try
                {
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(type, color, true);
                    break;
                }
                catch
                {
                    Console.WriteLine("Такого цвета нет, выберете другой!");
                }
            }

            Console.Clear();
            Console.WriteLine("Выберете цвет для шрифта (\"-\" - оставить без изменения):");

            /*Назначаем цвет для шрифта*/
            while (true)
            {
                color = Console.ReadLine() ?? string.Empty;
                if (color == "-")
                {
                    break;
                }
                try
                {
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, color, true);
                    break;
                }
                catch
                {
                    Console.WriteLine("Такого цвета нет, выберете другой!");
                }
            }

            Console.Clear();
        }
    }

    public static class TypeUtils
    {
        /*Задачи по вариантам*/
        // Самое длинное название свойства
        public static PropertyInfo GetTheLongestPubProperty(List<Type> types)
        {
            /*Обозначим флаг для поиска всех элементов, а не только открытых*/
            BindingFlags flag = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            PropertyInfo property = null;
            int maxLenName = 0;
            string nameProperties = "";
            PropertyInfo[] lProperties;

            foreach (Type t in types)
            {
                lProperties = t.GetProperties(flag);
                foreach (PropertyInfo p in lProperties)
                {
                    if (p.Name.Length > maxLenName)
                    {
                        maxLenName = p.Name.Length;
                        nameProperties = p.Name;
                        property = p;
                    }
                }
            }

            return property;
        }

        // Тип с наибольшим числом конструкторов
        public static Type GetTypeWithMaxConstructors(List<Type> types)
        {
            /*Обозначим флаг для поиска всех элементов, а не только открытых*/
            BindingFlags flag = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            Type type = null;
            int maxNumConstructors = 0;


            foreach (Type t in types)
            {
                if (t.GetConstructors(flag).Length > maxNumConstructors)
                {
                    type = t;
                    maxNumConstructors = t.GetConstructors(flag).Length;
                }
            }


            return type;
        }
        // Метод с наибольшим разнообразием аргументов по типам (больше разных типов аргументов)
        // Метод с аргументами int и string > метод с 10 аргументами типа int
        public static MethodInfo GetMethodWithArgVariety(List<Type> types)
        {
            /*Обозначим флаг для поиска всех элементов, а не только открытых*/
            BindingFlags flag = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            MethodInfo method = null;
            MethodInfo[] lMethods;
            ParameterInfo[] lParameters;
            int maxNumParameters = 0;

            foreach (Type t in types)
            {
                lMethods = t.GetMethods(flag);
                foreach (MethodInfo m in lMethods)
                {

                    lParameters = m.GetParameters();
                    var paramDic = new Dictionary<string, int>();

                    foreach (ParameterInfo p in lParameters)
                    {
                        if (paramDic.ContainsKey(p.ParameterType.Name))
                            // в словаре уже есть такое имя, значит ничего нового не добавляем
                            continue;
                        else
                            // в словаре нет такого имени, добавляем элемент
                            paramDic.Add(p.ParameterType.Name, 1);
                    }

                    if (paramDic.Count > maxNumParameters)
                    {
                        maxNumParameters = paramDic.Count;
                        method = m;
                    }
                }
            }

            return method;
        }
    }

}