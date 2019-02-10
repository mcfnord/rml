using System;

namespace HexC
{
    string[] writtenDigit = {"NEVERZERO", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", 
                            "eleven", "twelve", "thirteen", "forteen", "fifteen"} ;
    string[] writtenTens = { "NEVERTEN", "twenty", "thirty", "forty", "fifty" } ;

static void Main(string[] args)
{
    // spell it out

    num = (int)(args[0]);
    Debug.Assert(num > 0) ;
    Debug.Assert(num < 100) ;

    if(num <= 15)
    {
        Console.WriteLine(writtenDigit[num]);
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
        return ;

    Console.Write("-" + writtenDigit[num % 10] ) ;
}
}
