class Calculator{
    public static void Main(){
        Console.Write("Enter your name: ");
        string name = Console.ReadLine(); double total = 0;
        
        Console.Write("Enter the number of subjects: ");
        int number = int.Parse(Console.ReadLine());
        Dictionary<string, double> mydict  = new Dictionary<string, double>();

        for (int i = 0; i < number; i++){
            Console.Write("Subject Name: ");
            string subject = Console.ReadLine();
            
            Console.Write("Enter score: ");
            double score ;

            while  (!double.TryParse(Console.ReadLine(), out score) || score < 0 || score >20)  {
                Console.WriteLine("Invalid Score.Enter a valid Score");
                Console.Write("Enter score: ");
                

            }

            mydict.Add(subject, score);
            Console.WriteLine();
        }

        Console.WriteLine($"Hello, {name}");
        foreach (KeyValuePair<string, double> kvp in mydict){
            Console.WriteLine($"{kvp.Key} : {kvp.Value}");
            total += kvp.Value;
        }

        Console.WriteLine($"Your Average score: {Calculator.Calc_Average(total, number)}");

    }

    static double  Calc_Average(double total, int num){
        return (double)total/ num;
    }
}

