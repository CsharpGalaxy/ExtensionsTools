using System;

namespace CsharpGalexy.LibraryExtention.IO;

public static class Algorithm
{
    // The Luhn algorithm or Luhn formula,
    // also known as the "modulus 10" or "mod 10" algorithm,
    // named after its creator, IBM scientist Hans Peter Luhn,
    // is a simple checksum formula used to validate a variety of
    // identification numbers, such as credit card numbers, IMEI numbers, 
    public static byte Luhn(string value)
    {
        int conSum = 0;
        bool multiply = false;
        for (int i = value.Length - 1; i >= 0; i--)
        {
            if (multiply)
            {
                int conAdd = Convert.ToByte(value.Substring(i, 1)) * 2;
                if (conAdd > 9)
                    conAdd -= 9;
                conSum += conAdd;
            }
            else
                conSum += Convert.ToInt32(value.Substring(i, 1));

            multiply = !multiply;
        }
        //Si el total mod de 10 es 0 VALIDO
        if (conSum % 10 == 0)
            return 0;
        return Convert.ToByte(10 - conSum % 10);  // NO VALIDO
    }
}