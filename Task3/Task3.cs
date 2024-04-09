using System.Security.Cryptography.X509Certificates;

class Palindrome{
    public static void Main(){
        string name = Console.ReadLine();
        Console.WriteLine(palindromeChecker(name));

       
        
        
        }

    static bool palindromeChecker(string mystring){
            int l = 0; int r = mystring.Length - 1; 

            while (l < r){
                if (mystring[l] != mystring[r]){
                    return false;}
                l += 1; r-=1;
            }

            return true;

            

            

            }
}



    



   

