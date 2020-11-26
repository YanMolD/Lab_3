using System;
using System.Text.RegularExpressions;

namespace Lab3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int action;
            int index;
            string buf;
            int buf_int;
            int type = 0;
            List_of_contacts Contacts = new List_of_contacts();
            Contacts.Add_new_contact("AB");
            Contacts.Add_new_contact("AC");
            Contacts.Add_new_contact("ABC");
            Contacts.Add_new_contact("ABCD");
            Contacts.Add_new_contact("ABDE");
            Contacts.Add_new_contact("ABCDE");
        Choose_action:
            Console.WriteLine("Выберите действие, которое хотели бы выполнить:\n");
            Console.WriteLine("1. Просмотр списка контактов\n");
            Console.WriteLine("2. Поиск контактов\n");
            Console.WriteLine("3. Редактирование контакта\n");
            Console.WriteLine("4. Добавление контакта\n");
            Console.WriteLine("5. Завершение работы программы\n");
            try
            {
                action = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Пожалуйста, используйте цифры для выбора пункта меню. Повторите попытку\n");
                goto Choose_action;
            }
            switch (action)
            {
                case 1:
                    Console.WriteLine(Contacts);
                    Console.WriteLine("Для возвращения к выбору действий нажмите любую клавишу\n");
                    Console.ReadKey();
                    goto Choose_action;
                case 2:
                    Console.WriteLine("Введите данные, для поиска контактов: ");
                    buf = Console.ReadLine();
                    if (Contacts.Find_contacts(buf).people.Count == 0)
                        Console.WriteLine("Нет контактов, удовлетворяющих вашему условию\n");
                    else
                        Console.WriteLine(Contacts.Find_contacts(buf));
                    Console.WriteLine("Для возвращения к выбору действий нажмите любую клавишу\n");
                    Console.ReadKey();
                    goto Choose_action;
                case 3:
                    Console.WriteLine(Contacts);
                    Console.WriteLine("Выберите номер контакта\n");
                    try
                    {
                        index = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Пожалуйста, используйте цифры для выбора пункта меню. Повторите попытку\n");
                        goto case 3;
                    }
                    if ((Contacts.people.Count < index) || (index < 1))
                        throw new IndexOutOfRangeException();
                    Console.WriteLine(Contacts.people[index - 1]);
                    Console.WriteLine("Выберите действие:\n");
                    Console.WriteLine("1. Удалить номер\n");
                    Console.WriteLine("2. Удалить контакт\n");
                    Console.WriteLine("3. Добавить номер\n");
                    Console.WriteLine("4. Вернуться к списку/поиску контактов\n");
                Wrong_action:
                    try
                    {
                        action = Convert.ToInt32(Console.ReadLine());
                        buf = "";
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Пожалуйста, используйте цифры для выбора пункта меню. Повторите попытку\n");
                        goto Wrong_action;
                    }
                    switch (action)
                    {
                        case 1:
                            Console.WriteLine(Contacts.people[index - 1]);
                            Console.WriteLine("Выберите номер, который хотите удалить\n");
                            try
                            {
                                buf_int = Convert.ToInt32(Console.ReadLine());
                                if ((buf_int > Contacts.people[index - 1].list_of_numbers.Count) || (buf_int < 1))
                                    throw new IndexOutOfRangeException("Такого варианта не существует");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Пожалуйста, используйте цифры для выбора пункта меню. Повторите попытку\n");
                                goto case 1;
                            }

                            Contacts.people[index - 1].Delete_number(buf_int - 1);
                            Console.WriteLine("Номер удалён\n");
                            break;

                        case 2:
                            Contacts.people.RemoveAt(index - 1);
                            Console.WriteLine("Контакт удалён\n");
                            Console.WriteLine("Для возвращения к выбору действий нажмите любую клавишу\n");
                            Console.ReadKey();
                            break;

                        case 3:
                            try
                            {
                                if (buf == "")
                                {
                                    Console.WriteLine("Выберите тип номера:\n1. Домашний\n2. Личный\n3. Рабочий\n");
                                    try
                                    {
                                        type = Convert.ToInt32(Console.ReadLine());
                                        if ((type > 3) || (type < 1))
                                            throw new ArgumentException("Такого типа не существует");
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Пожалуйста, используйте цифры для выбора пункта меню. Повторите попытку\n");
                                        goto case 3;
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Console.WriteLine($"{ex.Message}, повторите попытку");
                                        goto case 3;
                                    }
                                }
                                Console.WriteLine("Введите номер, который хотели бы добавить\n");
                                buf = Console.ReadLine();
                                if (!(Regex.IsMatch(buf, @"^[+][\d]+$") || Regex.IsMatch(buf, @"^[\d]+$")))
                                    throw new FormatException("Неправильно введён номер");
                                Number buf_number = new Number(type - 1, buf);
                                Contacts.Add_number(buf_number, index - 1);
                                Console.WriteLine("Номер добавлен\n");
                                Console.WriteLine("Для возвращения к выбору действий нажмите любую клавишу\n");
                                Console.ReadKey();
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"{ex.Message}, попробуйте ещё раз\n");
                                goto case 3;
                            }
                            break;

                        case 4:
                            break;

                        default:
                            Console.WriteLine("Нет такого действия, повторите попытку");
                            goto Wrong_action;
                    }
                    goto Choose_action;
                case 4:
                    Console.WriteLine("Введите данные");
                    buf = Console.ReadLine();
                    Contacts.Add_new_contact(buf);
                    Console.WriteLine("Контакт успешно добавлен, для возвращения к выбору действий нажмите любую клавишу\n");
                    Console.ReadKey();
                    goto Choose_action;
                case 5:
                    break;

                default:
                    Console.WriteLine("Нет такого действия, повторите попытку");
                    goto Choose_action;
            }
        }
    }
}