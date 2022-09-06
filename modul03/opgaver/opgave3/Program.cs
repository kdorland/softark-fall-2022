var CreateWordFilterFn = (string[] words) =>
{
    return (string filterBadWords) =>
    {
        return String.Join(" ", filterBadWords.Split(" ").Except(words));
    };
};


var badWords = new string[] { "tis", "pis", "lort" };
var FilterBadWords = CreateWordFilterFn(badWords);
Console.WriteLine(FilterBadWords("Sikke en gang lort"));
// Udskriver: "Sikke en gang kage"