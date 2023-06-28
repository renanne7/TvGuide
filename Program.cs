namespace TvGuide
{
    class Program
    {
        static void Main(string[] args)
        {
            var guide = new Guide();

            Console.WriteLine("A. Search Shows (single search)");
            Console.WriteLine("B. Search Shows (multi search)");
            Console.WriteLine("C. Shows playing now in the UK");

            Console.WriteLine("\nEnter option:");
            var option = Console.ReadLine();

            switch(option?.ToUpper())
            {
                case "A":
                    guide.SearchShow().Wait();
                    break;
                case "B":
                    guide.SearchShows();
                    break;
                case "C":
                    guide.WatchNow();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }  
        }
    }
}