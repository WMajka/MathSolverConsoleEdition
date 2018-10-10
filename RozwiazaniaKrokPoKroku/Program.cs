using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Obecna wersja pozwala na rozdzielanie łancucha znaków
//i utworzenia list które obliczają wynik
//pokazując krok po kroku obliczenia na chwilę obecną są tylko +, -, *, /, ^, oraz pierwiastki z zastosowaniem odwrotności potęg czyli np 0,5
//z zachownaniem odpowiedniej kolejności
//



namespace RozwiazaniaKrokPoKroku
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> UserList = new List<string>();
            UserList = SendToList(CollectDataFromeUser());
            SegregationInToTheList(UserList);
            Console.ReadKey();
        }
        //Pobieranie danych od uzytkownika i przekazywanie ich do łańcucha znaków
        private static string CollectDataFromeUser()
        {
            string DateToSolve = Console.ReadLine();
            return DateToSolve;
        }
        //Przekazanie łańcucha znaków do Listy
        private static List<string> SendToList(string Date)
        {
            //zdefiniowanie podstawowych zmiennych które sa potrzebne
            //pusty ciąg znaków który przekazywany jest do głównej listy
            string stringCutter = "";
            //utworzenie nowej listy
            List<string> MainList = new List<string>();
            //utworzenie tablicy char, która zawiera w sobie znaki podziału oraz znaki które mogą być wykorzystane przez użytkownika
            char[] symbols = new char[] { '+', '-', '*', '/', '^', '=', ' ' };
            // główna pętla która rozdziela i sprawdza wystepowanie konkretnych znaków
            for (int i = 0; i < Date.Length; i++)
            {
                int littleHelper = 0;
                for (int j = 0; j < symbols.Length; j++)
                {
                    // Console.WriteLine($"porównanie: {Dane[i]} z {znaki[j]}");
                    if (Date[i] != symbols[j])
                    {
                        //sprawdzanie
                        //Console.WriteLine("znaki są różne");
                        littleHelper++;
                        if (symbols.Length == littleHelper)
                        {
                            // Console.WriteLine("Nie ma takiego znaku, dodaje do łancucha");
                            stringCutter += Date[i];
                        }
                    }
                    else if (Date[i] == symbols[j])
                    {
                        //Console.WriteLine("trafił na ten sam znak, dotychczasowy łańcuch dodaje do listy, jeśli jakiś był");
                        if (stringCutter != "")
                        {
                            MainList.Add(stringCutter);
                        }

                        if (symbols[j] != ' ' && symbols[j] != '=')
                        {
                            stringCutter = symbols[j].ToString();
                            MainList.Add(stringCutter);
                            //Console.WriteLine("to nie jest spacja ani =, zatem dodaje symbol do listy, i usuwam łańcuch znaków");

                        }
                        stringCutter = "";
                    }

                }

            }


            return MainList;
        }
        //segregowanie na mnniejsze listy które są wykorzystywane do obliczeń
        private static double SegregationInToTheList(List<string> DataList)
        {
            List<string> TempList = new List<string>();
            double result = 0;
            string emptyString = "";


            //sprawdzenie za pomocą pętli czy dany symbol się pojawił
            //a jeśli tak to gdzie i jaką miał wartość
            //Jeśli tak, to zostaje wysłany do matody Dodawanie i Usuwanie Wartości, która operuje na oryginalnych wartościach
            //a nie na kopiach
            do
            {

            } while (DataList.Contains("("));
            do
            {
                if (DataList.Contains("^"))
                {
                    AddingAndRemovingValues("^", ref DataList, ref TempList, ref emptyString, ref result);
                }
            } while (DataList.Contains("^"));
            do
            {
                if (DataList.Contains("*"))
                {
                    AddingAndRemovingValues("*", ref DataList, ref TempList, ref emptyString, ref result);
                }
                if (DataList.Contains("/"))
                {
                    AddingAndRemovingValues("/", ref DataList, ref TempList, ref emptyString, ref result);
                }

            } while (DataList.Contains("*") || DataList.Contains("/"));
            do
            {

                if (DataList.Contains("+"))
                {
                    AddingAndRemovingValues("+", ref DataList, ref TempList, ref emptyString, ref result);
                }
                if (DataList.Contains("-"))
                {
                    AddingAndRemovingValues("-", ref DataList, ref TempList, ref emptyString, ref result);
                }

            } while (DataList.Contains("+") || DataList.Contains("-"));

            return result;
        }
        //Metoda obliczeniowa, która wyświetla dodatkowo kroki działania zwracając wynik
        private static double Calculations(List<string> Operation)
        {

            double a, b, c = 0;
            string token;
            //konwersja listy pomocniczej na jednostki double oraz string
            a = Convert.ToDouble(Operation[0].ToString());

            b = Convert.ToDouble(Operation[2].ToString());

            token = Convert.ToString(Operation[1].ToString());
            //sprawdzanie warunku, jaki znak został podany
            if (token == "+")
            {
                c = a + b;
            }
            if (token == "-")
            {
                c = a - b;
            }
            if (token == "*")
            {
                c = a * b;
            }
            if (token == "/")
            {
                c = a / b;
            }
            if (token == "^")
            {
                c = Math.Pow(a, b);
            }


            //wyświetlenie obecnego kroku działania
            Console.WriteLine($"\nObecny krok: {a} {token} {b} = {c}");
            return c;
        }
        //Metoda która pobiera z głównej listy trzy wartości np 12 + 1
        //I przekazuje je do tymczasowej lsty
        //Jednocześnie wartości które zostały pobrane z głównej listy zostają usunięte
        private static void AddingAndRemovingValues(string operationSing, ref List<string> MainList, ref List<string> HelpList, ref string variableMath, ref double resultMath)
        {
            
            {
                //sprawdzenie na którym indeksie jest znakdziałania
                int value = MainList.IndexOf(operationSing);
                // Console.WriteLine($"Wartość pomocna: {wartosc}");
                for (int i = -1; i <= 1; i++)
                {
                    //dodaje do listy temp kolejno -1, 0, 1
                    HelpList.Add(MainList[value + i]);
                }
                for (int j = 1; j >= -1; j--)
                {
                    //usuwa z listy głównej kolejno 1, 0, 1
                    MainList.RemoveAt(value + j);
                }
                //przekazanie z metody Obliczenia wynniku i zapisaniu jej w liście, w konkretnym miejscu, a nie tylko na końcu
                resultMath = Calculations(HelpList);
                variableMath = Convert.ToString(resultMath);
                MainList.Insert(value - 1, variableMath);
                HelpList.RemoveRange(0, 3);
                for (int k = 0; k < MainList.Count; k++)
                {
                    Console.Write($"{MainList[k]} ");
                }

            }
        }

    }
}