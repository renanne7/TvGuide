using System;

namespace TvGuide
{
    class Program
    {
        static void Main(string[] args)
        {
            //Call class Guide
            var guide = new Guide();

            Console.WriteLine("A. Search Shows (single search)");
            Console.WriteLine("B. Search Shows (multi search)");
            Console.WriteLine("C. Shows playing now in the UK");

            Console.WriteLine("\nEnter option:");
            var option = Console.ReadLine();

            switch(option)
            {
                case var name when string.Equals(name, "A", StringComparison.InvariantCultureIgnoreCase):
                    guide.SearchShow();
                    break;
                case var name when string.Equals(name, "B", StringComparison.InvariantCultureIgnoreCase):
                    guide.SearchShows();
                    break;
                case var name when string.Equals(name, "C", StringComparison.InvariantCultureIgnoreCase):
                    guide.WatchNow();
                    break;
                default:
                    break;
            }  
        }
    }
}