using System.Diagnostics.Metrics;

class FreqCount{
    public static void Main(){
        Dictionary <char, int> mydict = counter(Console.ReadLine());
        foreach (KeyValuePair<char, int> kvp in mydict){
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");

        }

        }

    

    public static Dictionary<char, int>  counter(string myvar){
        Dictionary<char, int> mydict = new Dictionary<char, int>();
        for (int i = 0; i< myvar.Length; i++){
            if (!mydict.ContainsKey(myvar[i])){
                mydict.Add(myvar[i], 1);}

            else { mydict[myvar[i]] += 1 ;};
            
            }
        return mydict;
            
        
        }
    
}
