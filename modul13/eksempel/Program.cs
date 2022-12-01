User user1 = new User{Id = 42, Username = "Kristian"};
User user2 = new User{Id = 22, Username = "Mads"};
User user3 = new User{Id = 42, Username = "Kristian"};
User user4 = user1;

Console.WriteLine(user1.GetHashCode());
Console.WriteLine(user2.GetHashCode());
Console.WriteLine(user3.GetHashCode());

Console.WriteLine(user1.Equals(user3));
Console.WriteLine(user1 == user3);

public class User {
    public int Id { get; set; }
    public string Username { get; set; }

    public override bool Equals(object obj)
    {
        User input = (User) obj;
        return input.Id == this.Id;
    }

    public override int GetHashCode()
    {
        return Id + Username.GetHashCode();
    }
}