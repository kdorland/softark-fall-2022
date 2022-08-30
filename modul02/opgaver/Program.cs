Person[] people = new Person[]
{
    new Person { Name = "Jens Hansen", Age = 45, Phone = "+4512345678" },
    new Person { Name = "Jane Olsen", Age = 22, Phone = "+4543215687" },
    new Person { Name = "Tor Iversen", Age = 35, Phone = "+4587654322" },
    new Person { Name = "Sigurd Nielsen", Age = 31, Phone = "+4512345673" },
    new Person { Name = "Viggo Nielsen", Age = 28, Phone = "+4543217846" },
    new Person { Name = "Rosa Jensen", Age = 23, Phone = "+4543217846" },
};

/*
// Udregner den samlede alder for alle mennesker.
int totalAge = 0;
for (int i = 0; i < people.Length; i++)
{
    totalAge += people[i].Age;
}
*/
var sumAge = people.Sum(person => person.Age);
Console.WriteLine(sumAge);

/*
// Tæller hvor mange der hedder "Nielsen"
int countNielsen = 0;
for (int i = 0; i < people.Length; i++)
{
    if (people[i].Name.Contains("Nielsen"))
    {
        countNielsen++;
    }
}
*/
var count = people.Count(p => p.Name.Contains("Nielsen"));
Console.WriteLine(count);



class Person {
    public string Name { get; set; }
    public int Age { get; set; }
    public string Phone { get; set; }
}
