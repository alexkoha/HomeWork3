using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericApp
{
    [Key]
    public struct Shelf
    {
        private int _numberOfShelf;

        public Shelf(int numberOfShelf)
        {
            _numberOfShelf = numberOfShelf;
        }
    }

    public struct NoneKeyAttribute
    {
       
    }

    public class Package
    {
        private int _numberOfPackage;
        private string _nameOgPackage;

        public Package(int number, string name)
        {
            _numberOfPackage = number;
            _nameOgPackage = name;
        }

        public Package()
        {
        }

    }

    class Program
    {
        static void Main()
        {
            var test = new MultiDictionary<Shelf, Package>();
            var test2 = new MultiDictionary<NoneKeyAttribute, Package>();
            var test3 = new MultiDictionary<int, Package>();

            Console.WriteLine("Test 1 : Goot parameters");
            var _newShelf1 = new Shelf(53);
            var _newShelf2 = new Shelf(23);
            var _newShelf3 = new Shelf(12);

            test.Add(_newShelf1, new Package(345, "Bamboo"));
            test.Add(_newShelf1, new Package(231, "Bamboo"));
            test.Add(_newShelf2, new Package(132,"Bamboo"));
            test.Add(_newShelf3, new Package(12, "Mobile"));
            test.Add(_newShelf3, new Package(567, "Mobile"));

            Console.WriteLine("We expected tp 3 keys , lets print keys.");
            test.Keys.ToList().ForEach(shelf => Console.WriteLine(shelf));
            Console.WriteLine($"We expected tp 5 values , lets print count : {test.Count}");

            Console.WriteLine("\nTest 2 : Bed parameters");
            Console.WriteLine("Now we expected Exception.");

            try
            {
                test2.Add(new NoneKeyAttribute(), new Package(567, "Mobile"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("\nTest 3 : Bed parameters");
            Console.WriteLine("Now we expected Exception.");
            try
            {
                test3.Add(123, new Package(567, "Mobile"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
