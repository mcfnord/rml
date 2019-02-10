using System;

class sayit
{
    static string[] writtenDigit = {"NEVERZERO", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", 
                            "eleven", "twelve", "thirteen", "forteen", "fifteen"} ;
    static string[] writtenTens = { "NEVERZERO", "NEVERTEN", "twenty", "thirty", "forty", "fifty" } ;

static void DoThat(int num)
{
        
    if(num <= 19)
    {
        if( num <= 15)
            Console.WriteLine(writtenDigit[num]);
        else
        {
            if(num == 18)
                Console.WriteLine(writtenDigit[num - 10] + "een");
                else
                Console.WriteLine(writtenDigit[num - 10] + "teen");
        }
        return ;
    }

    // We know it's above 15!

    int tenth = num / 10 ;
    if(tenth <= 5)
    {
        Console.Write(writtenTens[tenth]);
    }
    else
    {
        // we construct the tens by adding ty to something. Except eighty!
        string tensy = writtenDigit[tenth] ;
        Console.Write(tensy) ;
        if(tenth == 8)
            Console.Write('y') ;
        else
            Console.Write("ty") ;
    }

    if( 0 == num % 10)
    {
        Console.WriteLine() ;
        return ;
    }

    Console.WriteLine("-" + writtenDigit[num % 10] ) ;


}
static void Main(string[] args)
{
    // spell it out

    for(int num = 1; num < 100; num++)
    {
        DoThat(num);
    }
}
}
