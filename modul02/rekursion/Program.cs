// Program.cs
//Console.WriteLine(Opgave3.Faculty(5)); // Output skal være '120'.
Console.WriteLine(Opgave4.sfd(5000, 1005000)); 
Console.WriteLine(Opgave4.potens(4, 5)); 
Console.WriteLine(Opgave4.Reverse("BANANKAGE")); 

class Opgave4 {

    public static string Reverse(string s) {
        if (s.Length <= 1) return s;
        else return Reverse(s.Substring(1)) + s[0];
    }

    public static int potens(int n, int p) { 
        if (p == 0) {
            return 1;
        } else {
            return potens(n, p - 1) * n;
        }
    }

    public static int sfd(int a, int b)
    {
        if (b <= a && a % b == 0) {
            return b;
        } else {
            if (a < b) {
                return sfd(b, a);
            } else {
                return sfd(b, a % b);
            }
        }
    }
}

class Opgave3 {
    public static int Faculty(int n)
    {
        if (n == 0) {
            return 1;
        } else {
            return n * Faculty(n - 1);
        }
    }
}