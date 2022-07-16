using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLessonAlgo04v01Part01
{
    [RankColumn]
    [MemoryDiagnoser]
    [MaxColumn, MinColumn]
    public class BenchMarkTestStingSearch
    {
        private HashSet<string> benchMarkTestHashSet;
        private string[] benchMarkTestArray;

        private Random randomString;
        private int child;

        [Params (10000, 100000)]
        public int sizeMassive {get;set;}



        public BenchMarkTestStingSearch()
        {      
            benchMarkTestHashSet = new HashSet<string>(sizeMassive);
            benchMarkTestArray = new string[sizeMassive];
            for (int i = 0; i < sizeMassive; i++)
            {
                string randomString = GetRandonString(3, i);
                benchMarkTestArray[i] = randomString;
                benchMarkTestHashSet.Add(randomString);
            }

        }

        [GlobalSetup]
        public void GlobalSetup() =>
            randomString = new Random((int)DateTime.Now.Ticks);

        private string GetRandonString(int lenghtMassiveChars, int num)
        {
            int c = 93;
            int s = 34;

            char[] massivaChars = new char[lenghtMassiveChars];

            for (int i = 0; i < lenghtMassiveChars; i++)
            {
                massivaChars[i] = (char)(s + num % c);
                num /= c;
            }

            return new string(massivaChars);
        }


        [Benchmark]
        public void SearchValueByFor()
        {
            string findStr = GetRandonString(3, randomString.Next(0, sizeMassive));

            for (int i = 0; i < benchMarkTestArray.Length; i++)
            { 
                if (benchMarkTestArray[i] == findStr)
                    break;
            }                
                    
        }

        [Benchmark]
        public void SearchIndexInMassive()
        {
            string findString = GetRandonString(3, randomString.Next(0, sizeMassive));
            Array.FindIndex(benchMarkTestArray, indexString => indexString == findString);
        }

        [Benchmark]
        public void SearchInMassive()
        {
            string findString = GetRandonString(3, randomString.Next(0, sizeMassive));
            Array.Find(benchMarkTestArray, str => str == findString);
        }

        [Benchmark]
        public void SearchValueHashset()
        {
            string findString = GetRandonString(3, randomString.Next(0, sizeMassive));
            benchMarkTestHashSet.TryGetValue(findString, out string _);
        }

        [Benchmark]
        public void SearchContainsHashset()
        {
            string findString = GetRandonString(3, randomString.Next(0, sizeMassive));
            benchMarkTestHashSet.Contains(findString);
        }
    }
}
