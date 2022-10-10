namespace shared.Model;

public class User {
    public int Id { get; set; }
    public string Username { get; set; }
    public User(string username = "") {
        Username = username;
    }
    public User() {
        Id = 0;
        Username = "";
    }
}