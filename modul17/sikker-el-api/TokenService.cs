using Newtonsoft.Json.Linq;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using JWT.Exceptions;

public class TokenService {
    private const string secret = "banankage";

    public string GenerateToken(string username)
    {
        var payload = new Dictionary<string, object>
        {
            { "Username", username },
            { "Role", "User"},
            { "exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()}
        };
        
        IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        IJsonSerializer serializer = new JsonNetSerializer();
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        var token = encoder.Encode(payload, secret);
        Console.WriteLine(token);

        return token;
    }

    private string ParseToken(string token) {
        try
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
            
            var json = decoder.Decode(token, secret, verify: true);
            Console.WriteLine("Parsing JWT: " + json);
            return json;
        }
        catch (TokenExpiredException)
        {
            Console.WriteLine("Token has expired");
            return null!;
        }
        catch (SignatureVerificationException)
        {
            Console.WriteLine("Token has invalid signature");
            return null!;
        }
    }

    public bool IsTokenValid(string token) {
        try {
        return !String.IsNullOrEmpty(ParseToken(token));            
        } catch (Exception e) {
            return false;
        }
    }

    public string GetRole(string token) {
        var json = ParseToken(token);
        dynamic stuff = JObject.Parse(json);
        string role = stuff.Role;
        return role;
    }
}

