using CVAPI.Models;
using System.Text;

namespace CVAPI.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {

        private ConvandbContext _dbContext;

        private string tokenresult = string.Empty; 
        public TokenManager(ConvandbContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public bool Authenticate(string username, string password)
        {
            string IncPassword = string.Empty;

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))             
            {
                var userEmployee = _dbContext.Users.Where(m => m.Username == username && m.Status == 1);

                var verifyUser = _dbContext.Users
                         .FirstOrDefault(c => c.Username == username &&
                                         c.Password == password);

                if (verifyUser != null) {
                    var userid = verifyUser.Id;
                    string userIdString = userid.ToString();
                    NewToken(userIdString);
                    return true;
                }
            }

            return false;

        }

        //TO RETURN THE FROM THE CONTROLLER 
        //NEED TO UPDATE 
        public string tokensresult() {
            var tokentodisplay = tokenresult;
            return tokentodisplay;
        }

        public TokenList NewToken(String userId)
        {
            //var tokenExpireinHrs = Int32.Parse(tokenExpiry);
            var tokenExpireinHrs = 24;

            var token = new TokenList
            {
                validtoken = Guid.NewGuid().ToString(),
                expirydate = DateTime.Now.AddHours(tokenExpireinHrs)
            };

            byte[] byteToken = Encoding.UTF8.GetBytes(userId + ":" + token.validtoken + String.Concat("tsaf") + ":" + token.expirydate);
            string ConvertedToken = Convert.ToBase64String(byteToken).ToString();

            var toAddtoken = _dbContext.Tokens.Add(new Token
            {
                UserId = int.Parse(userId),
                AuthToken = ConvertedToken,
                ExpiresOn = token.expirydate,
                IssuedOn = DateTime.Now,
                IsExpire = 0

            });

            tokenresult = ConvertedToken;
            tokensresult();

            _dbContext.SaveChanges();

            return token;
        }

        public bool VerifyToken(string token)
        {
            try
            {
            var FromBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var credentials = FromBase64.Split(':');
            if (credentials[1].Substring(credentials[1].Length - 4).Equals("tsaf")) //if token came from fast
            {

                var tokenList = _dbContext.Tokens.FirstOrDefault(t => t.AuthToken == token);
                if (tokenList != null)
                {
                    if (tokenList.ExpiresOn > DateTime.Now)
                    {
                        return true;
                    }
                    else
                    {
                        tokenList.IsExpire = 1;
                        _dbContext.SaveChanges();
                        return false;
                    }

                }
            }
    
            return false;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
