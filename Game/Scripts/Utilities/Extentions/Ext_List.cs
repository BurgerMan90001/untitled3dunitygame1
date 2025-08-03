using System.Collections.Generic;

public static class ListExtensions
{
    public static void Swap<T>(this List<T> list, int indexA, int indexB)
    {
        (list[indexB], list[indexA]) = (list[indexA], list[indexB]);
    }
}
