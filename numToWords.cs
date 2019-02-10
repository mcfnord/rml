using System;

namespace HexC
{
    string writtenDigit[] = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "forteen", "fifteen"} ;
    string writtenTens[] = { "NEVERTEN", "twenty", "thirty" "forty", "fifty", "NEVERSIXTY", "NEVERSEVENTY", "eighty" } ;

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
    if((tenth <= 5) || (tenth == 8))
    {
        Console.Write(writtenTens[tenth]);
    }
    else
    {
        // we construct the tens by adding ty to something.
        Console.Write(writtenDigit[tenth] + "ty");
    }

    if( 0 == num % 10)
        return ;

    Console.Write("-" + writtenDigit[num % 10] ) ;
}









    
    // if we get here, we do end in zero, but need to construct the ten set by adding a 'ty'
    Console.Write(writtenDigit[tenth] + "ty") ;
    return ;
    

    


}
}
