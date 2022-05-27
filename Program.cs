using System;
using System.Collections.Generic; //For Lists
namespace KAHUGHT_sorting_arrays
{
    class Program
    {
        static public List<List<int>> TotalList = new List<List<int>>(); //For merge sort, varible needs to be global
        static void Main(string[] args)
        {
            
            List<int> OddNumbers = new List<int>();
            List<int> evenNumbers = new List<int>();
            List<int> Characters = new List<int>();
            List<string> UserInput = new List<string>();

            //Take user input with babyproofing
            //------------------------------------------------------------------------------------------------------
            while (true)
            {
                Console.WriteLine("Enter numbers or letters, at least 10. Enter ! to end entering numbers ");
                string input = Console.ReadLine();
                if(input == "!" & OddNumbers.Count + evenNumbers.Count + Characters.Count < 10)
                {
                    Console.WriteLine("Enter more data");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                if(input == "!")
                {
                    break;
                }
                if (int.TryParse(input, out int result))
                {
                    if (result % 2 == 0)
                    {
                        evenNumbers.Add(result);
                    }
                    else if (result % 2 == 1)
                    {
                        OddNumbers.Add(result);
                    }
                    UserInput.Add(input);
                    Console.Clear();
                }
                else if(input.Length == 1)//Will only trigger if input is a character
                {
                    if (char.IsLetter(char.Parse(input)))
                    {
                        Characters.Add(char.Parse(input.ToLower()));
                        UserInput.Add(input.ToLower());
                    }
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Couldnt be converted to integer or character, make sure you enter a number or character and make sure the number isnt too big");
                    Console.WriteLine("Press enter to get rid of this error message and continue entering data");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
            }
            bool PerformMergeSort = false;
            Console.WriteLine("Would you like to merge sort or bubble sort? (Press m and then enter for merge sort)");
            string mergeornot = Console.ReadLine();
            if(mergeornot == "m" || mergeornot == "M")
            {
                PerformMergeSort = true;
            }

            //--------------------------------------------------------------------------------------------------

            List<int> sortedEven; 
            List<int> sortedOdd;
            List<int> sortedCharacters;
            if (PerformMergeSort)
            {
                sortedEven = MergeSort(evenNumbers);
                sortedOdd = MergeSort(OddNumbers);
                sortedCharacters = MergeSort(Characters, true);

            }
            else
            {
                sortedEven = Bubblesort(evenNumbers);
                sortedOdd = Bubblesort(OddNumbers);
                sortedCharacters = Bubblesort(Characters, true);
            }
            
            string output = "";
            for(int i = 0; i < UserInput.Count; i++)
            {
                output += UserInput[i] + ",";
            }

            Console.WriteLine("Entered string : " + output);
            Console.WriteLine("Sorted string : " + IntListToUnicodeString(sortedCharacters) + IntListToString(sortedOdd) + IntListToString(sortedEven)); 
        }
        static List<int> Bubblesort(List<int> input, bool reverse = false)
        {       
            //Perform the sorting
            bool SwapHappened = false;
            while (true)
            {
                for(int i = 0; i < input.Count - 1; i++) //One less than length as no value comes after the final value
                {
                    
                    if(input[i] > input[i + 1] & reverse == false)
                    {
                        //swap values
                        int temp = input[i + 1];
                        input[i + 1] = input[i];
                        input[i] = temp;
                        SwapHappened = true;
                    }
                    if(input[i] < input[i + 1] & reverse == true)
                    {
                        //swap values except descending
                        int temp = input[i];
                        input[i] = input[i + 1];
                        input[i + 1] = temp;
                        SwapHappened = true;
                    }
                }
                if(SwapHappened == false)
                {
                    return input;
                }
                SwapHappened = false;
            }

        }

        static string IntListToString(List<int> input)
        {
            string output = "";
            for(int i = 0; i < input.Count; i++)
            {
                output += input[i].ToString() + ",";
            }
            
            return output;
        }

        static string IntListToUnicodeString(List<int> input)
        {
            string output = "";
            for(int i = 0; i< input.Count; i++)
            {
                output += (char)input[i] + ",";
            }
            return output;
        }


        //Merge sort stuff under here--------------------------------------------------------------------------------


        static List<int> MergeSort(List<int> input, bool reverse = false)
        {
            TotalList.Clear();
            TotalList.Add(input);
            Split(input);

            //Do inital sort of the new list
            for(int i = 0; i < TotalList.Count; i++)
            {
                TotalList[i] = Bubblesort(TotalList[i], reverse);
            }

            Merge(TotalList[0], reverse);
            return TotalList[0];
        }


        static void Split(List<int> input, int Count = 0) //recursive function
        {
            //Split list
            List<int> FirstElements = new List<int>();
            List<int> LastElements = new List<int>();
                    
            for(int i = 0; i < input.Count / 2; i++)
            {
                FirstElements.Add(input[i]);
            }

            for(int i = input.Count/ 2; i< input.Count; i++)
            {
                LastElements.Add(input[i]);
            }
            
            TotalList.Remove(input); //Remove the list passed into the function

            TotalList.Add(FirstElements); //add TO THE END of the total list the new lists
            TotalList.Add(LastElements);
            
            //this is here as eventually the list will be split into 2s which dont want to be split
            //so this finds the remaining elements which need to be split and splits them
            while(TotalList[Count].Count < 3)
            {
                Count++;
                if(Count == TotalList.Count - 1)
                {
                    return; 
                }
            }        
            Split(TotalList[Count], Count);        
        }


        static void Merge(List<int> input, bool reverse = false)// recursive, going to use similar logic as split
        {
            //Take 2 elements of the list and merge
            List<int> MergedList = new List<int>();
            for(int i = 0; i < TotalList[0].Count; i++)
            {
                MergedList.Add(TotalList[0][i]);
            }
            for (int i = 0; i < TotalList[1].Count; i++)
            {
                MergedList.Add(TotalList[1][i]);
            }

            //sort the merged list and add to the END of the total list
            MergedList = Bubblesort(MergedList, reverse);
            TotalList.Add(MergedList);

            //remove the merged elements
            TotalList.RemoveAt(0);
            TotalList.RemoveAt(0); //List shifts up one as no 0 element as it just got removed. So another remove at 0 instead of 1
            if(TotalList.Count == 1)
            {
                return;
            }
            Merge(TotalList[0], reverse);
        } 
        
    }
}
